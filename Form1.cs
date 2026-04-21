using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace ClosedAI
{
    public partial class Form1 : Form
    {
        private List<OrderItem> allOrders;
        private List<dynamic> currentDisplayedProducts = new List<dynamic>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private DateTime ParseHotcakesDate(string hotcakesDate)
        {
            if (string.IsNullOrWhiteSpace(hotcakesDate))
                return DateTime.MinValue;

            string digits = new string(hotcakesDate.Where(char.IsDigit).ToArray());

            if (long.TryParse(digits, out long milliseconds))
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).LocalDateTime;
            }

            return DateTime.MinValue;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ApiService api = new ApiService();

            allOrders = await api.GetOrders();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = allOrders;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();

            var filtered = currentDisplayedProducts
                .Where(x =>
                    (x.ProductName != null && x.ProductName.ToLower().Contains(search)) ||
                    (x.ProductSku != null && x.ProductSku.ToLower().Contains(search)))
                .ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = filtered;
        }

        private async void btnTopProducts_Click(object sender, EventArgs e)
        {
            if (allOrders == null)
            {
                MessageBox.Show("Először töltsd be az orderöket.");
                return;
            }

            ApiService api = new ApiService();

            var validOrders = allOrders
                .Where(x => x.IsPlaced)
                .Where(x => !string.IsNullOrWhiteSpace(x.bvin))
                .ToList();

            List<OrderDetailItem> allItems = new List<OrderDetailItem>();

            foreach (var order in validOrders)
            {
                var items = await api.GetOrderDetailsItems(order.bvin);
                allItems.AddRange(items);
            }

            var topProducts = allItems
                .GroupBy(x => new { x.ProductId, x.ProductSku, x.ProductName })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductSku = g.Key.ProductSku,
                    ProductName = g.Key.ProductName,
                    TotalQuantitySold = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.LineTotal),
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .ThenByDescending(x => x.TotalRevenue)
                .Take(10)
                .ToList();

            currentDisplayedProducts = topProducts.Cast<dynamic>().ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
        }

        private async void btnWorstProducts_Click(object sender, EventArgs e)
        {
            if (allOrders == null)
            {
                MessageBox.Show("Először töltsd be az orderöket.");
                return;
            }

            ApiService api = new ApiService();

            var validOrders = allOrders
                .Where(x => x.IsPlaced)
                .Where(x => !string.IsNullOrWhiteSpace(x.bvin))
                .ToList();

            List<OrderDetailItem> allItems = new List<OrderDetailItem>();

            foreach (var order in validOrders)
            {
                var items = await api.GetOrderDetailsItems(order.bvin);
                allItems.AddRange(items);
            }

            var worstProducts = allItems
                .GroupBy(x => new { x.ProductId, x.ProductSku, x.ProductName })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductSku = g.Key.ProductSku,
                    ProductName = g.Key.ProductName,
                    TotalQuantitySold = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.LineTotal),
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.TotalQuantitySold)
                .ThenBy(x => x.TotalRevenue)
                .Take(10)
                .ToList();

            currentDisplayedProducts = worstProducts.Cast<dynamic>().ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
        }

        private async void btnPlus_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            var selected = dgvProducts.CurrentRow.DataBoundItem as dynamic;

            string productId = selected.ProductId;

            ApiService api = new ApiService();

            var inventory = await api.GetProductInventory(productId);

            if (inventory == null)
            {
                MessageBox.Show("Inventory not found!");
                return;
            }

            inventory.QuantityOnHand += 1;

            await api.UpdateProductInventory(inventory);

            MessageBox.Show("Inventory increased!");
        }

        private async void btnMinus_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            var selected = dgvProducts.CurrentRow.DataBoundItem as dynamic;

            string productId = selected.ProductId;

            ApiService api = new ApiService();

            var inventory = await api.GetProductInventory(productId);

            if (inventory == null)
            {
                MessageBox.Show("Inventory not found!");
                return;
            }

            if (inventory.QuantityOnHand <= 0)
            {
                MessageBox.Show("Inventory is already zero!");
                return;
            }

            inventory.QuantityOnHand -= 1;

            await api.UpdateProductInventory(inventory);

            MessageBox.Show("Inventory decreased!");
        }

        private async void btnAllProducts_Click(object sender, EventArgs e)
        {
            ApiService api = new ApiService();

            var products = await api.GetAllProducts();

            var result = products.Select(p => new
            {
                ProductId = p.Bvin,
                ProductSku = p.Sku,
                ProductName = p.ProductName
            }).ToList();

            currentDisplayedProducts = result.Cast<dynamic>().ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
        }

        private async void btnSearchProduct_Click_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Adj meg egy SKU-t!");
                return;
            }

            ApiService api = new ApiService();

            var product = await api.GetProductBySku(txtSearch.Text.Trim());

            if (product == null)
            {
                MessageBox.Show("Nincs ilyen termék.");
                return;
            }

            var result = new List<dynamic>
            {
                new
                {
                    ProductId = product.Bvin,
                    ProductSku = product.Sku,
                    ProductName = product.ProductName
                }
            };

            currentDisplayedProducts = result;

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
        }
    }
}