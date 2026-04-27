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
            pnlMenu = new Panel();
            btnReports = new Button();
            btnInventory = new Button();
            btnProducts = new Button();
            lblBrand = new Label();
            pnlMain = new Panel();
            pnlReports = new Panel();
            pnlReportsGridHost = new Panel();
            pnlReportsContent = new Panel();
            btnRevenueStats = new Button();
            btnCategoryStats = new Button();
            dtpTo = new DateTimePicker();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            lblFrom = new Label();
            lblReportsTitle = new Label();
            pnlInventory = new Panel();
            pnlInventoryGridHost = new Panel();
            pnlInventoryContent = new Panel();
            btnApplyDiscount = new Button();
            txtDiscount = new TextBox();
            lblDiscount = new Label();
            btnMinus = new Button();
            btnPlus = new Button();
            btnSearchProduct = new Button();
            txtSearch = new TextBox();
            lblSku = new Label();
            btnAllProducts = new Button();
            lblInventoryTitle = new Label();
            pnlProducts = new Panel();
            pnlProductsGridHost = new Panel();
            dgvProducts = new DataGridView();
            pnlProductsToolbar = new Panel();
            btnWorstProducts = new Button();
            btnTopProducts = new Button();
            button1 = new Button();
            lblProductsTitle = new Label();
            pnlMenu.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlReports.SuspendLayout();
            pnlReportsContent.SuspendLayout();
            pnlInventory.SuspendLayout();
            pnlInventoryContent.SuspendLayout();
            pnlProducts.SuspendLayout();
            pnlProductsGridHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            pnlProductsToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(22, 37, 33);
            pnlMenu.Controls.Add(btnReports);
            pnlMenu.Controls.Add(btnInventory);
            pnlMenu.Controls.Add(btnProducts);
            pnlMenu.Controls.Add(lblBrand);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(220, 720);
            pnlMenu.TabIndex = 0;
            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(60, 71, 75);
            btnReports.Cursor = Cursors.Hand;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnReports.ForeColor = Color.FromArgb(243, 255, 185);
            btnReports.Location = new Point(20, 234);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(180, 46);
            btnReports.TabIndex = 3;
            btnReports.Text = "Reports";
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;
            // 
            // btnInventory
            // 
            btnInventory.BackColor = Color.FromArgb(60, 71, 75);
            btnInventory.Cursor = Cursors.Hand;
            btnInventory.FlatAppearance.BorderSize = 0;
            btnInventory.FlatStyle = FlatStyle.Flat;
            btnInventory.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnInventory.ForeColor = Color.FromArgb(243, 255, 185);
            btnInventory.Location = new Point(20, 176);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(180, 46);
            btnInventory.TabIndex = 2;
            btnInventory.Text = "Inventory";
            btnInventory.UseVisualStyleBackColor = false;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnProducts
            // 
            btnProducts.BackColor = Color.FromArgb(255, 253, 152);
            btnProducts.Cursor = Cursors.Hand;
            btnProducts.FlatAppearance.BorderSize = 0;
            btnProducts.FlatStyle = FlatStyle.Flat;
            btnProducts.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnProducts.ForeColor = Color.FromArgb(22, 37, 33);
            btnProducts.Location = new Point(20, 118);
            btnProducts.Name = "btnProducts";
            btnProducts.Size = new Size(180, 46);
            btnProducts.TabIndex = 1;
            btnProducts.Text = "Products";
            btnProducts.UseVisualStyleBackColor = false;
            btnProducts.Click += btnProducts_Click;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(255, 253, 152);
            lblBrand.Location = new Point(20, 32);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(159, 38);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Wheelbase";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(243, 255, 185);
            pnlMain.Controls.Add(pnlReports);
            pnlMain.Controls.Add(pnlInventory);
            pnlMain.Controls.Add(pnlProducts);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(220, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1060, 720);
            pnlMain.TabIndex = 1;
            // 
            // pnlReports
            // 
            pnlReports.BackColor = Color.FromArgb(243, 255, 185);
            pnlReports.Controls.Add(pnlReportsGridHost);
            pnlReports.Controls.Add(pnlReportsContent);
            pnlReports.Controls.Add(lblReportsTitle);
            pnlReports.Dock = DockStyle.Fill;
            pnlReports.Location = new Point(0, 0);
            pnlReports.Name = "pnlReports";
            pnlReports.Padding = new Padding(32);
            pnlReports.Size = new Size(1060, 720);
            pnlReports.TabIndex = 2;
            pnlReports.Visible = false;
            // 
            // pnlReportsGridHost
            // 
            pnlReportsGridHost.Dock = DockStyle.Fill;
            pnlReportsGridHost.Location = new Point(32, 248);
            pnlReportsGridHost.Name = "pnlReportsGridHost";
            pnlReportsGridHost.Size = new Size(996, 440);
            pnlReportsGridHost.TabIndex = 2;
            // 
            // pnlReportsContent
            // 
            pnlReportsContent.BackColor = Color.FromArgb(22, 37, 33);
            pnlReportsContent.Controls.Add(btnRevenueStats);
            pnlReportsContent.Controls.Add(btnCategoryStats);
            pnlReportsContent.Controls.Add(dtpTo);
            pnlReportsContent.Controls.Add(dtpFrom);
            pnlReportsContent.Controls.Add(lblTo);
            pnlReportsContent.Controls.Add(lblFrom);
            pnlReportsContent.Dock = DockStyle.Top;
            pnlReportsContent.Location = new Point(32, 96);
            pnlReportsContent.Name = "pnlReportsContent";
            pnlReportsContent.Padding = new Padding(24);
            pnlReportsContent.Size = new Size(996, 152);
            pnlReportsContent.TabIndex = 1;
            // 
            // btnRevenueStats
            // 
            btnRevenueStats.BackColor = Color.FromArgb(136, 162, 170);
            btnRevenueStats.Cursor = Cursors.Hand;
            btnRevenueStats.FlatAppearance.BorderSize = 0;
            btnRevenueStats.FlatStyle = FlatStyle.Flat;
            btnRevenueStats.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRevenueStats.ForeColor = Color.FromArgb(22, 37, 33);
            btnRevenueStats.Location = new Point(356, 86);
            btnRevenueStats.Name = "btnRevenueStats";
            btnRevenueStats.Size = new Size(176, 46);
            btnRevenueStats.TabIndex = 5;
            btnRevenueStats.Text = "Revenue Stats";
            btnRevenueStats.UseVisualStyleBackColor = false;
            btnRevenueStats.Click += btnRevenueStats_Click;
            // 
            // btnCategoryStats
            // 
            btnCategoryStats.BackColor = Color.FromArgb(255, 253, 152);
            btnCategoryStats.Cursor = Cursors.Hand;
            btnCategoryStats.FlatAppearance.BorderSize = 0;
            btnCategoryStats.FlatStyle = FlatStyle.Flat;
            btnCategoryStats.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCategoryStats.ForeColor = Color.FromArgb(22, 37, 33);
            btnCategoryStats.Location = new Point(162, 86);
            btnCategoryStats.Name = "btnCategoryStats";
            btnCategoryStats.Size = new Size(176, 46);
            btnCategoryStats.TabIndex = 4;
            btnCategoryStats.Text = "Category Stats";
            btnCategoryStats.UseVisualStyleBackColor = false;
            btnCategoryStats.Click += btnCategoryStats_Click;
            // 
            // dtpTo
            // 
            dtpTo.CalendarForeColor = Color.FromArgb(22, 37, 33);
            dtpTo.CalendarMonthBackground = Color.White;
            dtpTo.CalendarTitleBackColor = Color.FromArgb(60, 71, 75);
            dtpTo.CalendarTitleForeColor = Color.FromArgb(255, 253, 152);
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(462, 30);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(176, 31);
            dtpTo.TabIndex = 3;
            // 
            // dtpFrom
            // 
            dtpFrom.CalendarForeColor = Color.FromArgb(22, 37, 33);
            dtpFrom.CalendarMonthBackground = Color.White;
            dtpFrom.CalendarTitleBackColor = Color.FromArgb(60, 71, 75);
            dtpFrom.CalendarTitleForeColor = Color.FromArgb(255, 253, 152);
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(162, 30);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(176, 31);
            dtpFrom.TabIndex = 2;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTo.ForeColor = Color.FromArgb(255, 253, 152);
            lblTo.Location = new Point(410, 33);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(34, 28);
            lblTo.TabIndex = 1;
            lblTo.Text = "To";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblFrom.ForeColor = Color.FromArgb(255, 253, 152);
            lblFrom.Location = new Point(90, 33);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(59, 28);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "From";
            // 
            // lblReportsTitle
            // 
            lblReportsTitle.Dock = DockStyle.Top;
            lblReportsTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblReportsTitle.ForeColor = Color.FromArgb(22, 37, 33);
            lblReportsTitle.Location = new Point(32, 32);
            lblReportsTitle.Name = "lblReportsTitle";
            lblReportsTitle.Size = new Size(996, 64);
            lblReportsTitle.TabIndex = 0;
            lblReportsTitle.Text = "Reports";
            // 
            // pnlInventory
            // 
            pnlInventory.BackColor = Color.FromArgb(243, 255, 185);
            pnlInventory.Controls.Add(pnlInventoryGridHost);
            pnlInventory.Controls.Add(pnlInventoryContent);
            pnlInventory.Controls.Add(lblInventoryTitle);
            pnlInventory.Dock = DockStyle.Fill;
            pnlInventory.Location = new Point(0, 0);
            pnlInventory.Name = "pnlInventory";
            pnlInventory.Padding = new Padding(32);
            pnlInventory.Size = new Size(1060, 720);
            pnlInventory.TabIndex = 1;
            pnlInventory.Visible = false;
            // 
            // pnlInventoryGridHost
            // 
            pnlInventoryGridHost.Dock = DockStyle.Fill;
            pnlInventoryGridHost.Location = new Point(32, 284);
            pnlInventoryGridHost.Name = "pnlInventoryGridHost";
            pnlInventoryGridHost.Size = new Size(996, 404);
            pnlInventoryGridHost.TabIndex = 2;
            // 
            // pnlInventoryContent
            // 
            pnlInventoryContent.BackColor = Color.FromArgb(22, 37, 33);
            pnlInventoryContent.Controls.Add(btnApplyDiscount);
            pnlInventoryContent.Controls.Add(txtDiscount);
            pnlInventoryContent.Controls.Add(lblDiscount);
            pnlInventoryContent.Controls.Add(btnMinus);
            pnlInventoryContent.Controls.Add(btnPlus);
            pnlInventoryContent.Controls.Add(btnSearchProduct);
            pnlInventoryContent.Controls.Add(txtSearch);
            pnlInventoryContent.Controls.Add(lblSku);
            pnlInventoryContent.Controls.Add(btnAllProducts);
            pnlInventoryContent.Dock = DockStyle.Top;
            pnlInventoryContent.Location = new Point(32, 96);
            pnlInventoryContent.Name = "pnlInventoryContent";
            pnlInventoryContent.Padding = new Padding(24);
            pnlInventoryContent.Size = new Size(996, 188);
            pnlInventoryContent.TabIndex = 1;
            // 
            // btnApplyDiscount
            // 
            btnApplyDiscount.BackColor = Color.FromArgb(255, 253, 152);
            btnApplyDiscount.Cursor = Cursors.Hand;
            btnApplyDiscount.FlatAppearance.BorderSize = 0;
            btnApplyDiscount.FlatStyle = FlatStyle.Flat;
            btnApplyDiscount.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnApplyDiscount.ForeColor = Color.FromArgb(22, 37, 33);
            btnApplyDiscount.Location = new Point(584, 110);
            btnApplyDiscount.Name = "btnApplyDiscount";
            btnApplyDiscount.Size = new Size(176, 46);
            btnApplyDiscount.TabIndex = 8;
            btnApplyDiscount.Text = "Apply Discount";
            btnApplyDiscount.UseVisualStyleBackColor = false;
            btnApplyDiscount.Click += btnApplyDiscount_Click;
            // 
            // txtDiscount
            // 
            txtDiscount.BackColor = Color.White;
            txtDiscount.BorderStyle = BorderStyle.FixedSingle;
            txtDiscount.Font = new Font("Segoe UI", 11F);
            txtDiscount.ForeColor = Color.FromArgb(22, 37, 33);
            txtDiscount.Location = new Point(350, 115);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(208, 37);
            txtDiscount.TabIndex = 7;
            txtDiscount.TextAlign = HorizontalAlignment.Center;
            // 
            // lblDiscount
            // 
            lblDiscount.AutoSize = true;
            lblDiscount.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDiscount.ForeColor = Color.FromArgb(255, 253, 152);
            lblDiscount.Location = new Point(224, 120);
            lblDiscount.Name = "lblDiscount";
            lblDiscount.Size = new Size(107, 28);
            lblDiscount.TabIndex = 6;
            lblDiscount.Text = "Discount %";
            // 
            // btnMinus
            // 
            btnMinus.BackColor = Color.FromArgb(136, 162, 170);
            btnMinus.Cursor = Cursors.Hand;
            btnMinus.FlatAppearance.BorderSize = 0;
            btnMinus.FlatStyle = FlatStyle.Flat;
            btnMinus.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            btnMinus.ForeColor = Color.FromArgb(22, 37, 33);
            btnMinus.Location = new Point(104, 110);
            btnMinus.Name = "btnMinus";
            btnMinus.Size = new Size(64, 46);
            btnMinus.TabIndex = 5;
            btnMinus.Text = "-";
            btnMinus.UseVisualStyleBackColor = false;
            btnMinus.Click += btnMinus_Click;
            // 
            // btnPlus
            // 
            btnPlus.BackColor = Color.FromArgb(136, 162, 170);
            btnPlus.Cursor = Cursors.Hand;
            btnPlus.FlatAppearance.BorderSize = 0;
            btnPlus.FlatStyle = FlatStyle.Flat;
            btnPlus.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            btnPlus.ForeColor = Color.FromArgb(22, 37, 33);
            btnPlus.Location = new Point(24, 110);
            btnPlus.Name = "btnPlus";
            btnPlus.Size = new Size(64, 46);
            btnPlus.TabIndex = 4;
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = false;
            btnPlus.Click += btnPlus_Click;
            // 
            // btnSearchProduct
            // 
            btnSearchProduct.BackColor = Color.FromArgb(255, 253, 152);
            btnSearchProduct.Cursor = Cursors.Hand;
            btnSearchProduct.FlatAppearance.BorderSize = 0;
            btnSearchProduct.FlatStyle = FlatStyle.Flat;
            btnSearchProduct.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSearchProduct.ForeColor = Color.FromArgb(22, 37, 33);
            btnSearchProduct.Location = new Point(584, 24);
            btnSearchProduct.Name = "btnSearchProduct";
            btnSearchProduct.Size = new Size(176, 46);
            btnSearchProduct.TabIndex = 3;
            btnSearchProduct.Text = "Search Product";
            btnSearchProduct.UseVisualStyleBackColor = false;
            btnSearchProduct.Click += btnSearchProduct_Click_Click;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.ForeColor = Color.FromArgb(22, 37, 33);
            txtSearch.Location = new Point(350, 29);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(208, 37);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSku
            // 
            lblSku.AutoSize = true;
            lblSku.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSku.ForeColor = Color.FromArgb(255, 253, 152);
            lblSku.Location = new Point(290, 34);
            lblSku.Name = "lblSku";
            lblSku.Size = new Size(45, 28);
            lblSku.TabIndex = 1;
            lblSku.Text = "SKU";
            // 
            // btnAllProducts
            // 
            btnAllProducts.BackColor = Color.FromArgb(255, 253, 152);
            btnAllProducts.Cursor = Cursors.Hand;
            btnAllProducts.FlatAppearance.BorderSize = 0;
            btnAllProducts.FlatStyle = FlatStyle.Flat;
            btnAllProducts.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnAllProducts.ForeColor = Color.FromArgb(22, 37, 33);
            btnAllProducts.Location = new Point(24, 24);
            btnAllProducts.Name = "btnAllProducts";
            btnAllProducts.Size = new Size(176, 46);
            btnAllProducts.TabIndex = 0;
            btnAllProducts.Text = "All Products";
            btnAllProducts.UseVisualStyleBackColor = false;
            btnAllProducts.Click += btnAllProducts_Click;
            // 
            // lblInventoryTitle
            // 
            lblInventoryTitle.Dock = DockStyle.Top;
            lblInventoryTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblInventoryTitle.ForeColor = Color.FromArgb(22, 37, 33);
            lblInventoryTitle.Location = new Point(32, 32);
            lblInventoryTitle.Name = "lblInventoryTitle";
            lblInventoryTitle.Size = new Size(996, 64);
            lblInventoryTitle.TabIndex = 0;
            lblInventoryTitle.Text = "Inventory";
            // 
            // pnlProducts
            // 
            pnlProducts.BackColor = Color.FromArgb(243, 255, 185);
            pnlProducts.Controls.Add(pnlProductsGridHost);
            pnlProducts.Controls.Add(pnlProductsToolbar);
            pnlProducts.Controls.Add(lblProductsTitle);
            pnlProducts.Dock = DockStyle.Fill;
            pnlProducts.Location = new Point(0, 0);
            pnlProducts.Name = "pnlProducts";
            pnlProducts.Padding = new Padding(32);
            pnlProducts.Size = new Size(1060, 720);
            pnlProducts.TabIndex = 0;
            // 
            // pnlProductsGridHost
            // 
            pnlProductsGridHost.Controls.Add(dgvProducts);
            pnlProductsGridHost.Dock = DockStyle.Fill;
            pnlProductsGridHost.Location = new Point(32, 166);
            pnlProductsGridHost.Name = "pnlProductsGridHost";
            pnlProductsGridHost.Size = new Size(996, 522);
            pnlProductsGridHost.TabIndex = 2;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 71, 75);
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 71, 75);
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvProducts.ColumnHeadersHeight = 38;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.DefaultCellStyle.BackColor = Color.White;
            dgvProducts.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvProducts.DefaultCellStyle.ForeColor = Color.FromArgb(22, 37, 33);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(136, 162, 170);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(22, 37, 33);
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.GridColor = Color.FromArgb(136, 162, 170);
            dgvProducts.Location = new Point(0, 0);
            dgvProducts.MultiSelect = true;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.RowTemplate.Height = 34;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(996, 522);
            dgvProducts.TabIndex = 0;
            // 
            // pnlProductsToolbar
            // 
            pnlProductsToolbar.Controls.Add(btnWorstProducts);
            pnlProductsToolbar.Controls.Add(btnTopProducts);
            pnlProductsToolbar.Controls.Add(button1);
            pnlProductsToolbar.Dock = DockStyle.Top;
            pnlProductsToolbar.Location = new Point(32, 96);
            pnlProductsToolbar.Name = "pnlProductsToolbar";
            pnlProductsToolbar.Size = new Size(996, 70);
            pnlProductsToolbar.TabIndex = 1;
            // 
            // btnWorstProducts
            // 
            btnWorstProducts.BackColor = Color.FromArgb(136, 162, 170);
            btnWorstProducts.Cursor = Cursors.Hand;
            btnWorstProducts.FlatAppearance.BorderSize = 0;
            btnWorstProducts.FlatStyle = FlatStyle.Flat;
            btnWorstProducts.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnWorstProducts.ForeColor = Color.FromArgb(22, 37, 33);
            btnWorstProducts.Location = new Point(396, 12);
            btnWorstProducts.Name = "btnWorstProducts";
            btnWorstProducts.Size = new Size(176, 46);
            btnWorstProducts.TabIndex = 2;
            btnWorstProducts.Text = "Worst Products";
            btnWorstProducts.UseVisualStyleBackColor = false;
            btnWorstProducts.Click += btnWorstProducts_Click;
            // 
            // btnTopProducts
            // 
            btnTopProducts.BackColor = Color.FromArgb(136, 162, 170);
            btnTopProducts.Cursor = Cursors.Hand;
            btnTopProducts.FlatAppearance.BorderSize = 0;
            btnTopProducts.FlatStyle = FlatStyle.Flat;
            btnTopProducts.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnTopProducts.ForeColor = Color.FromArgb(22, 37, 33);
            btnTopProducts.Location = new Point(198, 12);
            btnTopProducts.Name = "btnTopProducts";
            btnTopProducts.Size = new Size(176, 46);
            btnTopProducts.TabIndex = 1;
            btnTopProducts.Text = "Top Products";
            btnTopProducts.UseVisualStyleBackColor = false;
            btnTopProducts.Click += btnTopProducts_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 253, 152);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(22, 37, 33);
            button1.Location = new Point(0, 12);
            button1.Name = "button1";
            button1.Size = new Size(176, 46);
            button1.TabIndex = 0;
            button1.Text = "Load Orders";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // lblProductsTitle
            // 
            lblProductsTitle.Dock = DockStyle.Top;
            lblProductsTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblProductsTitle.ForeColor = Color.FromArgb(22, 37, 33);
            lblProductsTitle.Location = new Point(32, 32);
            lblProductsTitle.Name = "lblProductsTitle";
            lblProductsTitle.Size = new Size(996, 64);
            lblProductsTitle.TabIndex = 0;
            lblProductsTitle.Text = "Products";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 255, 185);
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlMain);
            Controls.Add(pnlMenu);
            MinimumSize = new Size(1040, 640);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wheelbase Admin";
            Load += Form1_Load;
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlReports.ResumeLayout(false);
            pnlReportsContent.ResumeLayout(false);
            pnlReportsContent.PerformLayout();
            pnlInventory.ResumeLayout(false);
            pnlInventoryContent.ResumeLayout(false);
            pnlInventoryContent.PerformLayout();
            pnlProducts.ResumeLayout(false);
            pnlProductsGridHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            pnlProductsToolbar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Panel pnlMain;
        private Panel pnlProducts;
        private Panel pnlInventory;
        private Panel pnlReports;
        private Panel pnlProductsGridHost;
        private Panel pnlInventoryGridHost;
        private Panel pnlReportsGridHost;
        private Button btnProducts;
        private Button btnInventory;
        private Button btnReports;
        private Label lblBrand;
        private Label lblProductsTitle;
        private Panel pnlProductsToolbar;
        private Button button1;
        private Button btnTopProducts;
        private Button btnWorstProducts;
        private DataGridView dgvProducts;
        private Label lblInventoryTitle;
        private Panel pnlInventoryContent;
        private Button btnAllProducts;
        private Label lblSku;
        private TextBox txtSearch;
        private Button btnSearchProduct;
        private Button btnPlus;
        private Button btnMinus;
        private Label lblDiscount;
        private TextBox txtDiscount;
        private Button btnApplyDiscount;
        private Label lblReportsTitle;
        private Panel pnlReportsContent;
        private Label lblFrom;
        private Label lblTo;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnCategoryStats;
        private Button btnRevenueStats;
    }
}
