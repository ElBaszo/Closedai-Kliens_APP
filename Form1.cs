using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ClosedAI
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=DNN;Trusted_Connection=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestConnection();
        }

        private void TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Sikeres kapcsolódás!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }
    }
}