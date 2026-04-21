namespace ClosedAI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnRevenueStats = new Button();
            btnCategoryStats = new Button();
            btnWorstProducts = new Button();
            btnTopProducts = new Button();
            dgvProducts = new DataGridView();
            txtSearch = new TextBox();
            btnPlus = new Button();
            btnMinus = new Button();
            btnApplyDiscount = new Button();
            btnResetPrices = new Button();
            label1 = new Label();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRevenueStats);
            panel1.Controls.Add(btnCategoryStats);
            panel1.Controls.Add(btnWorstProducts);
            panel1.Controls.Add(btnTopProducts);
            panel1.Location = new Point(-2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(341, 653);
            panel1.TabIndex = 0;
            // 
            // btnRevenueStats
            // 
            btnRevenueStats.Location = new Point(69, 531);
            btnRevenueStats.Name = "btnRevenueStats";
            btnRevenueStats.Size = new Size(153, 48);
            btnRevenueStats.TabIndex = 4;
            btnRevenueStats.Text = "Revenue Stats";
            btnRevenueStats.UseVisualStyleBackColor = true;
            // 
            // btnCategoryStats
            // 
            btnCategoryStats.Location = new Point(69, 371);
            btnCategoryStats.Name = "btnCategoryStats";
            btnCategoryStats.Size = new Size(153, 48);
            btnCategoryStats.TabIndex = 3;
            btnCategoryStats.Text = "Category Stats";
            btnCategoryStats.UseVisualStyleBackColor = true;
            // 
            // btnWorstProducts
            // 
            btnWorstProducts.AutoEllipsis = true;
            btnWorstProducts.Location = new Point(69, 213);
            btnWorstProducts.Name = "btnWorstProducts";
            btnWorstProducts.Size = new Size(153, 48);
            btnWorstProducts.TabIndex = 2;
            btnWorstProducts.Text = "Worst Products";
            btnWorstProducts.UseVisualStyleBackColor = true;
            // 
            // btnTopProducts
            // 
            btnTopProducts.Location = new Point(69, 56);
            btnTopProducts.Name = "btnTopProducts";
            btnTopProducts.Size = new Size(153, 48);
            btnTopProducts.TabIndex = 1;
            btnTopProducts.Text = "Top Products";
            btnTopProducts.UseVisualStyleBackColor = true;
            btnTopProducts.Click += btnTopProducts_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(382, 1);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.Size = new Size(615, 653);
            dgvProducts.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(1003, 74);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(314, 31);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnPlus
            // 
            btnPlus.Location = new Point(1015, 134);
            btnPlus.Name = "btnPlus";
            btnPlus.Size = new Size(74, 42);
            btnPlus.TabIndex = 3;
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = true;
            // 
            // btnMinus
            // 
            btnMinus.Location = new Point(1217, 134);
            btnMinus.Name = "btnMinus";
            btnMinus.Size = new Size(74, 42);
            btnMinus.TabIndex = 4;
            btnMinus.Text = "-";
            btnMinus.UseVisualStyleBackColor = true;
            // 
            // btnApplyDiscount
            // 
            btnApplyDiscount.Location = new Point(1034, 224);
            btnApplyDiscount.Name = "btnApplyDiscount";
            btnApplyDiscount.Size = new Size(239, 47);
            btnApplyDiscount.TabIndex = 5;
            btnApplyDiscount.Text = "Apply Discount";
            btnApplyDiscount.UseVisualStyleBackColor = true;
            // 
            // btnResetPrices
            // 
            btnResetPrices.Location = new Point(1034, 337);
            btnResetPrices.Name = "btnResetPrices";
            btnResetPrices.Size = new Size(239, 47);
            btnResetPrices.TabIndex = 6;
            btnResetPrices.Text = "Revert Discount";
            btnResetPrices.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1064, 26);
            label1.Name = "label1";
            label1.Size = new Size(166, 25);
            label1.TabIndex = 7;
            label1.Text = "Search For Product:";
            // 
            // button1
            // 
            button1.Location = new Point(1107, 539);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 8;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1318, 654);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(btnResetPrices);
            Controls.Add(btnApplyDiscount);
            Controls.Add(btnMinus);
            Controls.Add(btnPlus);
            Controls.Add(txtSearch);
            Controls.Add(dgvProducts);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnRevenueStats;
        private Button btnCategoryStats;
        private Button btnWorstProducts;
        private Button btnTopProducts;
        private DataGridView dgvProducts;
        private TextBox txtSearch;
        private Button btnPlus;
        private Button btnMinus;
        private Button btnApplyDiscount;
        private Button btnResetPrices;
        private Label label1;
        private Button button1;
    }
}
