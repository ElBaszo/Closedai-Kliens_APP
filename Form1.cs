using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;

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
    }
}
