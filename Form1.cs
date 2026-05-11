using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace ClosedAI
{
    public partial class Form1 : Form
    {
        private List<OrderItem> allOrders;
        private List<dynamic> currentDisplayedProducts = new List<dynamic>();

        private class ProductGridRow
        {
            public string ProductId { get; set; } = string.Empty;
            public string ProductSku { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int? QuantityOnHand { get; set; }
        }

        private class OrderGridRow
        {
            public string Bvin { get; set; } = string.Empty;
            public string OrderNumber { get; set; } = string.Empty;
            public string OrderDate { get; set; } = string.Empty;
            public string Customer { get; set; } = string.Empty;
            public decimal TotalGrand { get; set; }
            public string StatusName { get; set; } = string.Empty;
            public int ItemCount { get; set; }
            public string Products { get; set; } = string.Empty;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
            dgvProducts.MultiSelect = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.DataBindingComplete += dgvProducts_DataBindingComplete;
            InitializeButtonVisualState();
            ShowView(pnlProducts);
        }

        private void ShowView(Panel selectedPanel)
        {
            Panel gridHost = pnlProductsGridHost;
            Button activeButton = btnProducts;

            if (selectedPanel == pnlInventory)
            {
                gridHost = pnlInventoryGridHost;
                activeButton = btnInventory;
            }
            else if (selectedPanel == pnlReports)
            {
                gridHost = pnlReportsGridHost;
                activeButton = btnReports;
            }

            MoveGridTo(gridHost);

            pnlProducts.Visible = false;
            pnlInventory.Visible = false;
            pnlReports.Visible = false;

            selectedPanel.Visible = true;
            selectedPanel.BringToFront();
            SetActiveMenuButton(activeButton);
            ApplyGridColumnVisibility();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ShowView(pnlProducts);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            ShowView(pnlInventory);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ShowView(pnlReports);
        }

        private void MoveGridTo(Panel gridHost)
        {
            if (dgvProducts.Parent != gridHost)
            {
                gridHost.Controls.Add(dgvProducts);
            }

            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.BringToFront();
        }

        private void SetActiveMenuButton(Button activeButton)
        {
            Button[] menuButtons = { btnProducts, btnInventory, btnReports };

            foreach (Button button in menuButtons)
            {
                button.BackColor = Color.FromArgb(60, 71, 75);
                button.ForeColor = Color.FromArgb(243, 255, 185);
            }

            activeButton.BackColor = Color.FromArgb(255, 253, 152);
            activeButton.ForeColor = Color.FromArgb(22, 37, 33);
        }

        private void InitializeButtonVisualState()
        {
            SetNormalButtonStyle(button1);
            SetNormalButtonStyle(btnTopProducts);
            SetNormalButtonStyle(btnWorstProducts);
            SetNormalButtonStyle(btnAllProducts);
            SetNormalButtonStyle(btnSearchProduct);
            SetNormalButtonStyle(btnCategoryStats);
            SetNormalButtonStyle(btnRevenueStats);
            SetNormalButtonStyle(btnPlus);
            SetNormalButtonStyle(btnMinus);
            SetNormalButtonStyle(btnApplyDiscount);
        }

        private void SetActiveButton(Button activeButton, params Button[] groupButtons)
        {
            foreach (Button button in groupButtons)
            {
                SetNormalButtonStyle(button);
            }

            activeButton.BackColor = Color.FromArgb(255, 253, 152);
            activeButton.ForeColor = Color.FromArgb(22, 37, 33);
            activeButton.UseVisualStyleBackColor = false;
        }

        private void SetNormalButtonStyle(Button button)
        {
            button.BackColor = Color.FromArgb(60, 71, 75);
            button.ForeColor = Color.FromArgb(243, 255, 185);
            button.UseVisualStyleBackColor = false;
        }

        private void dgvProducts_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyGridColumnVisibility();
        }

        private void ApplyGridColumnVisibility()
        {
            HideGridColumn("Id");
            HideGridColumn("bvin");
            HideGridColumn("Bvin");
            HideGridColumn("ProductId");
            HideGridColumn("TimeOfOrderUtc");
            HideGridColumn("CreationDateUtc");
            HideGridColumn("ListPrice");
            HideGridColumn("IsPlaced");

            SetGridHeader("OrderNumber", "Order");
            SetGridHeader("OrderDate", "Date");
            SetGridHeader("Customer", "Customer");
            SetGridHeader("UserEmail", "Customer Email");
            SetGridHeader("TotalGrand", "Total");
            SetGridHeader("StatusName", "Status");
            SetGridHeader("ItemCount", "Items");
            SetGridHeader("ProductName", "Product");
            SetGridHeader("ProductSku", "SKU");
            SetGridHeader("Sku", "SKU");
            SetGridHeader("TotalQuantitySold", "Units Sold");
            SetGridHeader("TotalRevenue", "Revenue");
            SetGridHeader("AvgRevenuePerUnit", "Avg / Unit");
            SetGridHeader("OrderCount", "Orders");
            SetGridHeader("TotalUnits", "Units");
            SetGridHeader("Price", "Price");
            SetGridHeader("QuantityOnHand", "Raktáron");
            SetGridHeader("Products", "Order Products");

            FormatGridColumn("TotalGrand", "N0");
            FormatGridColumn("TotalRevenue", "N0");
            FormatGridColumn("AvgRevenuePerUnit", "N0");
            FormatGridColumn("Price", "N0");

            ConfigureOrderProductsColumn();
        }

        private void HideGridColumn(string columnName)
        {
            DataGridViewColumn? column = GetGridColumn(columnName);

            if (column != null)
            {
                column.Visible = false;
            }
        }

        private void SetGridHeader(string columnName, string headerText)
        {
            DataGridViewColumn? column = GetGridColumn(columnName);

            if (column != null)
            {
                column.HeaderText = headerText;
            }
        }

        private void FormatGridColumn(string columnName, string format)
        {
            DataGridViewColumn? column = GetGridColumn(columnName);

            if (column != null)
            {
                column.DefaultCellStyle.Format = format;
            }
        }

        private DataGridViewColumn? GetGridColumn(string columnName)
        {
            if (!dgvProducts.Columns.Contains(columnName))
            {
                return null;
            }

            return dgvProducts.Columns[columnName];
        }

        private void ConfigureOrderProductsColumn()
        {
            DataGridViewColumn? column = GetGridColumn("Products");

            if (column == null)
            {
                dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                return;
            }

            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            SetActiveButton(button1, button1, btnTopProducts, btnWorstProducts);

            ApiService api = new ApiService();

            allOrders = await api.GetOrders();
            var orderRows = await BuildOrderGridRows(api, allOrders);

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = orderRows;
        }

        private async Task<List<OrderGridRow>> BuildOrderGridRows(ApiService api, List<OrderItem> orders)
        {
            var rows = new List<OrderGridRow>();

            foreach (var orderGroup in GetOrderGroupsForDisplay(orders))
            {
                OrderItem order = GetPrimaryOrderForGroup(orderGroup);
                List<OrderDetailItem> items = await GetOrderItemsForGroup(api, orderGroup);

                DateTime orderDate = orderGroup
                    .Select(x => ParseHotcakesDate(x.TimeOfOrderUtc))
                    .Where(x => x != DateTime.MinValue)
                    .DefaultIfEmpty(DateTime.MinValue)
                    .Max();

                rows.Add(new OrderGridRow
                {
                    Bvin = order.bvin,
                    OrderNumber = order.OrderNumber,
                    OrderDate = orderDate == DateTime.MinValue ? string.Empty : orderDate.ToString("yyyy-MM-dd HH:mm"),
                    Customer = order.UserEmail,
                    TotalGrand = order.TotalGrand,
                    StatusName = order.StatusName,
                    ItemCount = items.Sum(x => x.Quantity),
                    Products = FormatOrderProducts(items)
                });
            }

            return rows
                .OrderByDescending(x => ParseOrderNumberForSort(x.OrderNumber))
                .ThenByDescending(x => x.OrderDate)
                .ToList();
        }

        private List<IGrouping<string, OrderItem>> GetOrderGroupsForDisplay(List<OrderItem> orders)
        {
            return orders
                .Where(x => x.IsPlaced)
                .Where(x => !string.IsNullOrWhiteSpace(x.OrderNumber) ||
                            !string.IsNullOrWhiteSpace(x.bvin))
                .GroupBy(GetOrderGroupKey)
                .ToList();
        }

        private string GetOrderGroupKey(OrderItem order)
        {
            if (!string.IsNullOrWhiteSpace(order.OrderNumber))
            {
                return "order:" + order.OrderNumber.Trim();
            }

            return "bvin:" + order.bvin;
        }

        private OrderItem GetPrimaryOrderForGroup(IGrouping<string, OrderItem> orderGroup)
        {
            return orderGroup
                .OrderByDescending(x => ParseHotcakesDate(x.TimeOfOrderUtc))
                .First();
        }

        private async Task<List<OrderDetailItem>> GetOrderItemsForGroup(ApiService api, IGrouping<string, OrderItem> orderGroup)
        {
            foreach (string bvin in orderGroup
                .Select(x => x.bvin)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct())
            {
                List<OrderDetailItem> items = await api.GetOrderDetailsItems(bvin);

                if (items.Count > 0)
                {
                    return items;
                }
            }

            return new List<OrderDetailItem>();
        }

        private int ParseOrderNumberForSort(string orderNumber)
        {
            return int.TryParse(orderNumber, out int value) ? value : 0;
        }

        private List<OrderItem> GetPlacedOrdersWithBvin(List<OrderItem> orders)
        {
            return orders
                .Where(x => x.IsPlaced)
                .Where(x => !string.IsNullOrWhiteSpace(x.bvin))
                .Where(x => !string.IsNullOrWhiteSpace(x.OrderNumber))
                .GroupBy(GetOrderGroupKey)
                .Select(g => g.First())
                .ToList();
        }

        private string FormatOrderProducts(List<OrderDetailItem> items)
        {
            var groupedItems = items
                .GroupBy(item => new
                {
                    item.ProductId,
                    item.ProductSku,
                    item.ProductName
                })
                .Select(group => new
                {
                    group.Key.ProductSku,
                    group.Key.ProductName,
                    Quantity = group.Sum(item => item.Quantity)
                });

            return string.Join(", ", groupedItems.Select(item =>
            {
                string productName = item.ProductName;

                if (string.IsNullOrWhiteSpace(productName))
                {
                    productName = item.ProductSku;
                }

                if (string.IsNullOrWhiteSpace(productName))
                {
                    productName = "Unknown product";
                }

                return productName + " x" + item.Quantity;
            }));
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

            SetActiveButton(btnTopProducts, button1, btnTopProducts, btnWorstProducts);

            ApiService api = new ApiService();

            var validOrders = GetPlacedOrdersWithBvin(allOrders);

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

            SetActiveButton(btnWorstProducts, button1, btnTopProducts, btnWorstProducts);

            ApiService api = new ApiService();

            var validOrders = GetPlacedOrdersWithBvin(allOrders);

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
            SetNormalButtonStyle(btnPlus);

            var productIds = new List<string>();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                bool rowIsSelected = row.Selected;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Selected)
                    {
                        rowIsSelected = true;
                        break;
                    }
                }

                if (rowIsSelected && row.Cells["ProductId"].Value != null)
                {
                    string productId = row.Cells["ProductId"].Value.ToString();

                    if (!productIds.Contains(productId))
                    {
                        productIds.Add(productId);
                    }
                }
            }

            if (productIds.Count == 0)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            ApiService api = new ApiService();
            int successCount = 0;
            int missingInventoryCount = 0;

            foreach (string productBvin in productIds)
            {
                var inventory = await api.GetInventoryByProductBvin(productBvin);

                if (inventory == null)
                {
                    missingInventoryCount++;
                    continue;
                }

                int newQuantity = inventory.QuantityOnHand + 1;

                bool success = await api.SaveInventory(
                    inventory.Bvin,
                    productBvin,
                    inventory.VariantId,
                    newQuantity
                );

                if (success)
                {
                    successCount++;
                    UpdateDisplayedInventoryQuantity(productBvin, newQuantity);
                }
            }

            string message = successCount + " különböző termék inventoryja növelve.";

            if (missingInventoryCount > 0)
            {
                message += "\n" + missingInventoryCount + " kijelölt termékhez nem található meglévő inventory rekord.";
            }

            MessageBox.Show(message);
        }

        private async void btnMinus_Click(object sender, EventArgs e)
        {
            SetNormalButtonStyle(btnMinus);

            var productIds = new List<string>();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                bool rowIsSelected = row.Selected;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Selected)
                    {
                        rowIsSelected = true;
                        break;
                    }
                }

                if (rowIsSelected && row.Cells["ProductId"].Value != null)
                {
                    string productId = row.Cells["ProductId"].Value.ToString();

                    if (!productIds.Contains(productId))
                    {
                        productIds.Add(productId);
                    }
                }
            }

            if (productIds.Count == 0)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            ApiService api = new ApiService();
            int successCount = 0;
            int missingInventoryCount = 0;

            foreach (string productBvin in productIds)
            {
                var inventory = await api.GetInventoryByProductBvin(productBvin);

                if (inventory == null)
                {
                    missingInventoryCount++;
                    continue;
                }

                if (inventory.QuantityOnHand <= 0)
                    continue;

                int newQuantity = inventory.QuantityOnHand - 1;

                bool success = await api.SaveInventory(
                    inventory.Bvin,
                    inventory.ProductBvin,
                    inventory.VariantId,
                    newQuantity
                );

                if (success)
                {
                    successCount++;
                    UpdateDisplayedInventoryQuantity(productBvin, newQuantity);
                }
            }

            string message = successCount + " különböző termék inventoryja csökkentve.";

            if (missingInventoryCount > 0)
            {
                message += "\n" + missingInventoryCount + " kijelölt termékhez nem található meglévő inventory rekord.";
            }

            MessageBox.Show(message);
        }

        private void UpdateDisplayedInventoryQuantity(string productBvin, int newQuantity)
        {
            if (!dgvProducts.Columns.Contains("QuantityOnHand"))
            {
                return;
            }

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["ProductId"].Value != null &&
                    row.Cells["ProductId"].Value.ToString() == productBvin)
                {
                    row.Cells["QuantityOnHand"].Value = newQuantity;
                }
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

            var result = await BuildProductGridRows(api, products);

            currentDisplayedProducts = result.Cast<dynamic>().ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
            SetActiveButton(btnAllProducts, btnAllProducts, btnSearchProduct);
        }

        private async Task<List<ProductGridRow>> BuildProductGridRows(ApiService api, List<ProductResponse> products)
        {
            var result = new List<ProductGridRow>();
            var inventories = await api.GetAllInventory();

            foreach (ProductResponse product in products)
            {
                ProductInventoryResponse inventory = inventories
                    .FirstOrDefault(x => string.Equals(x.ProductBvin, product.Bvin, StringComparison.OrdinalIgnoreCase));

                result.Add(new ProductGridRow
                {
                    ProductId = product.Bvin,
                    ProductSku = product.Sku,
                    ProductName = product.ProductName,
                    Price = product.SitePrice,
                    QuantityOnHand = inventory?.QuantityOnHand
                });
            }

            return result;
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

            var inventory = await api.GetInventoryByProductBvin(product.Bvin);

            var result = new List<ProductGridRow>
            {
                new ProductGridRow
                {
                    ProductId = product.Bvin,
                    ProductSku = product.Sku,
                    ProductName = product.ProductName,
                    Price = product.SitePrice,
                    QuantityOnHand = inventory?.QuantityOnHand
                }
            };

            currentDisplayedProducts = result.Cast<dynamic>().ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = currentDisplayedProducts;
            SetActiveButton(btnSearchProduct, btnAllProducts, btnSearchProduct);
        }

        private async void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            SetNormalButtonStyle(btnApplyDiscount);

            if (!decimal.TryParse(txtDiscount.Text, out decimal discountPercent))
            {
                MessageBox.Show("Adj meg érvényes kedvezmény %-ot.");
                return;
            }

            var productIds = new List<string>();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                bool rowIsSelected = row.Selected;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Selected)
                    {
                        rowIsSelected = true;
                        break;
                    }
                }

                if (rowIsSelected &&
                    row.Cells["ProductId"].Value != null)
                {
                    string productId =
                        row.Cells["ProductId"].Value.ToString();

                    if (!productIds.Contains(productId))
                    {
                        productIds.Add(productId);
                    }
                }
            }

            if (productIds.Count == 0)
            {
                MessageBox.Show("Nincs kijelölt termék.");
                return;
            }

            ApiService api = new ApiService();

            int successCount = 0;

            foreach (string productId in productIds)
            {
                DataGridViewRow row = dgvProducts.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r =>
                        r.Cells["ProductId"].Value != null &&
                        r.Cells["ProductId"].Value.ToString() == productId);

                if (row == null)
                    continue;

                decimal oldPrice =
                    Convert.ToDecimal(
                        row.Cells["Price"].Value
                    );

                decimal newPrice =
                    oldPrice *
                    (1 - discountPercent / 100);

                bool success =
                    await api.UpdateProductPrice(
                        productId,
                        newPrice
                    );

                if (success)
                    successCount++;
            }

            MessageBox.Show(
                successCount +
                " termékre kedvezmény alkalmazva."
            );
        }

        private async void btnCategoryStats_Click(object sender, EventArgs e)
        {
            ApiService api = new ApiService();

            if (allOrders == null)
                allOrders = await api.GetOrders();

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            var filteredOrders =
                GetPlacedOrdersWithBvin(allOrders)
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
            SetActiveButton(btnCategoryStats, btnCategoryStats, btnRevenueStats);
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

            var filteredOrders = GetPlacedOrdersWithBvin(allOrders)
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
            SetActiveButton(btnRevenueStats, btnCategoryStats, btnRevenueStats);
        }
    }
}
