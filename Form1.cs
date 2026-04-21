using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace ClosedAI
{
    public partial class Form1 : Form
    {
        private List<OrderItem> allOrders;
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

            dgvProducts.DataSource = allOrders;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (allOrders == null) return;

            string search = txtSearch.Text.ToLower();

            var filtered = allOrders
                .Where(x =>
                    (x.UserEmail != null && x.UserEmail.ToLower().Contains(search)) ||
                    (x.OrderNumber != null && x.OrderNumber.ToLower().Contains(search))
                )
                .ToList();

            dgvProducts.DataSource = filtered;
        }

        private async void btnTopProducts_Click(object sender, EventArgs e)
        {
            if (allOrders == null) return;

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
                .GroupBy(x => new { x.ProductSku, x.ProductName })
                .Select(g => new
                {
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

            dgvProducts.DataSource = topProducts;
        }

        private async void btnWorstProducts_Click(object sender, EventArgs e)
        {
            if (allOrders == null) return;

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
                .GroupBy(x => new { x.ProductSku, x.ProductName })
                .Select(g => new
                {
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

            dgvProducts.DataSource = worstProducts;
        }
    }
}
