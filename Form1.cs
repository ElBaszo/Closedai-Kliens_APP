using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Globalization;

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

        private void btnTopProducts_Click(object sender, EventArgs e)
        {
            if (allOrders == null) return;

            var topSales = allOrders
                .Where(x => x.IsPlaced == true)
                .Where(x => !string.IsNullOrWhiteSpace(x.OrderNumber))
                .Where(x => x.StatusName == "Complete")
                .OrderByDescending(x => x.TotalGrand)
                .Take(10)
                .Select((x, index) => new
                {
                    Rank = index + 1,
                    OrderNumber = x.OrderNumber,
                    CustomerEmail = x.UserEmail,
                    TotalValue = x.TotalGrand,
                    Status = x.StatusName,
                    OrderDate = ParseHotcakesDate(x.TimeOfOrderUtc).ToString("yyyy-MM-dd HH:mm")
                })
                .ToList();

            dgvProducts.DataSource = topSales;
        }
    }
}
