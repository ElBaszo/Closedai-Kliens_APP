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
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
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

            if (selected == null)
            {
                MessageBox.Show("Nincs kiválasztott termék.");
                return;
            }

            string productBvin = selected.ProductId;

            ApiService api = new ApiService();

            var inventory = await api.GetInventoryByProductBvin(productBvin);

            if (inventory == null)
            {
                MessageBox.Show("Ehhez a termékhez nincs inventory rekord.");
                return;
            }

            int newQuantity = inventory.QuantityOnHand + 1;

            bool success = await api.SaveInventory(
                inventory.Bvin,
                inventory.ProductBvin,
                inventory.VariantId,
                newQuantity
            );

            if (success)
            {
                MessageBox.Show("Inventory növelve.");
            }
        }

        private async void btnMinus_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            var selected = dgvProducts.CurrentRow.DataBoundItem as dynamic;

            if (selected == null)
            {
                MessageBox.Show("Nincs kiválasztott termék.");
                return;
            }

            string productBvin = selected.ProductId;

            ApiService api = new ApiService();

            var inventory = await api.GetInventoryByProductBvin(productBvin);

            if (inventory == null)
            {
                MessageBox.Show("Ehhez a termékhez nincs inventory rekord.");
                return;
            }

            if (inventory.QuantityOnHand <= 0)
            {
                MessageBox.Show("Nem lehet nulla alá menni.");
                return;
            }

            int newQuantity = inventory.QuantityOnHand - 1;

            bool success = await api.SaveInventory(
                inventory.Bvin,
                inventory.ProductBvin,
                inventory.VariantId,
                newQuantity
            );

            if (success)
            {
                MessageBox.Show("Inventory csökkentve.");
            }
        }

        private async void btnAllProducts_Click(object sender, EventArgs e)
        {
            ApiService api = new ApiService();

            var products = await api.GetAllProducts();

            if (products == null || products.Count == 0)
            {
                MessageBox.Show("Nincs termék vagy hiba történt.");
                return;
            }

            var result = products.Select(p => new
            {
                ProductId = p.Bvin,
                ProductSku = p.Sku,
                ProductName = p.ProductName,
                Price = p.SitePrice
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

            string searchSku = txtSearch.Text.Trim().ToLower();

            ApiService api = new ApiService();

            var products = await api.GetAllProducts();

            var product = products
                .FirstOrDefault(p => p.Sku != null &&
                                     p.Sku.ToLower() == searchSku);

            if (product == null)
            {
                MessageBox.Show("Nincs ilyen SKU-jú termék.");
                return;
            }

            var result = new List<dynamic>
    {
        new
        {
            ProductId = product.Bvin,
            ProductSku = product.Sku,
            ProductName = product.ProductName,
            Price = product.SitePrice
        }
    };

            currentDisplayedProducts = result;

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
        }

        private async void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            if (!decimal.TryParse(txtDiscount.Text, out decimal discountPercent))
            {
                MessageBox.Show("Adj meg érvényes százalékot.");
                return;
            }

            if (discountPercent <= 0 || discountPercent >= 100)
            {
                MessageBox.Show("0 és 100 közötti érték kell.");
                return;
            }

            var selected = dgvProducts.CurrentRow.DataBoundItem as dynamic;

            decimal oldPrice = selected.Price;

            decimal newPrice =
                oldPrice * (1 - discountPercent / 100);

            ApiService api = new ApiService();
            bool success = await api.UpdateProductPrice(
                selected.ProductId,
                newPrice
            );

            if (success)
            {
                MessageBox.Show("Discount alkalmazva.");
            }
        }

        private async void btnCategoryStats_Click(object sender, EventArgs e)
        {
            ApiService api = new ApiService();

            if (allOrders == null)
                allOrders = await api.GetOrders();

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            var filteredOrders =
                allOrders
                .Where(o => o.IsPlaced)
                .Where(o =>
                {
                    DateTime d =
                        ParseHotcakesDate(
                           o.TimeOfOrderUtc);

                    return d >= fromDate &&
                           d < toDate;
                })
                .ToList();

            List<dynamic> categoryRows =
                new List<dynamic>();

            List<OrderDetailItem> allItems =
                new List<OrderDetailItem>();

            foreach (var order in filteredOrders)
            {
                var items =
                 await api.GetOrderDetailsItems(order.bvin);

                allItems.AddRange(items);
            }

            var grouped =
                allItems
                .GroupBy(x => x.ProductId);

            var stats =
                new List<dynamic>();

            foreach (var g in grouped)
            {
                string productName = g.First().ProductName.ToLower();

                string category = "Other";

                if (productName.Contains("ferrari"))
                    category = "Ferrari";
                else if (productName.Contains("porsche"))
                    category = "Porsche";
                else if (productName.Contains("mercedes"))
                    category = "Mercedes";
                else if (productName.Contains("lamborghini"))
                    category = "Lamborghini";
                else if (productName.Contains("bmw"))
                    category = "BMW";
                else if (productName.Contains("mclaren"))
                    category = "McLaren";

                stats.Add(new
                {
                    Category = category,
                    Quantity = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.LineTotal)
                });
            }

            var finalStats =
                stats
                .GroupBy(x => x.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalUnits =
                        g.Sum(x => (int)x.Quantity),

                    TotalRevenue =
                        g.Sum(x => (decimal)x.Revenue),

                    AvgRevenuePerUnit =
                        g.Sum(x => (decimal)x.Revenue) /
                        g.Sum(x => (int)x.Quantity)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = finalStats;
            dgvProducts.Columns["TotalRevenue"].DefaultCellStyle.Format = "N0";
            dgvProducts.Columns["AvgRevenuePerUnit"].DefaultCellStyle.Format = "N0";
        }

        private async void btnRevenueStats_Click(object sender, EventArgs e)
        {
            ApiService api = new ApiService();

            if (allOrders == null)
            {
                allOrders = await api.GetOrders();
            }

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            var filteredOrders = allOrders
                .Where(o => o.IsPlaced)
                .Where(o =>
                {
                    DateTime orderDate = ParseHotcakesDate(o.TimeOfOrderUtc);
                    return orderDate >= fromDate && orderDate < toDate;
                })
                .ToList();

            if (filteredOrders.Count == 0)
            {
                MessageBox.Show("Nincs rendelés ebben az intervallumban.");
                return;
            }

            List<OrderDetailItem> allItems = new List<OrderDetailItem>();

            foreach (var order in filteredOrders)
            {
                var items = await api.GetOrderDetailsItems(order.bvin);
                allItems.AddRange(items);
            }

            if (allItems.Count == 0)
            {
                MessageBox.Show("Nincs termékadat a rendelésekben.");
                return;
            }

            decimal totalRevenue = allItems.Sum(x => x.LineTotal);
            int totalOrders = filteredOrders.Count;
            int totalUnitsSold = allItems.Sum(x => x.Quantity);

            decimal averageOrderValue = totalRevenue / totalOrders;
            decimal averageRevenuePerUnit = totalRevenue / totalUnitsSold;

            var bestSellingProduct = allItems
                .GroupBy(x => x.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .First();

            var topRevenueProduct = allItems
                .GroupBy(x => x.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    Revenue = g.Sum(x => x.LineTotal)
                })
                .OrderByDescending(x => x.Revenue)
                .First();

            var result = new List<dynamic>
    {
        new
        {
            Metric = "Total Revenue",
            Value = totalRevenue.ToString("N0") + " HUF"
        },
        new
        {
            Metric = "Total Orders",
            Value = totalOrders.ToString()
        },
        new
        {
            Metric = "Total Units Sold",
            Value = totalUnitsSold.ToString()
        },
        new
        {
            Metric = "Average Order Value",
            Value = averageOrderValue.ToString("N0") + " HUF"
        },
        new
        {
            Metric = "Average Revenue Per Unit",
            Value = averageRevenuePerUnit.ToString("N0") + " HUF"
        },
        new
        {
            Metric = "Best Selling Product",
            Value = bestSellingProduct.ProductName + " (" + bestSellingProduct.Quantity + " pcs)"
        },
        new
        {
            Metric = "Top Revenue Product",
            Value = topRevenueProduct.ProductName + " (" + topRevenueProduct.Revenue.ToString("N0") + " HUF)"
        }
    };

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = result;
        }
    }
}