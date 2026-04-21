using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ClosedAI
{
    public partial class Form1 : Form
    {

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

            string result = await api.GetOrders();

            MessageBox.Show(result);
        }
    }
    }
