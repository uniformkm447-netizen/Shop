namespace ComputerStoreDesk;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;
    private ToolTip toolTip = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        toolTip = new ToolTip(components);
        helpProvider = new HelpProvider();
        newProductGroupBox = new GroupBox();
        miniToolStrip = new MenuStrip();
        справкаToolStripMenuItem = new ToolStripMenuItem();
        руководствоПользователяToolStripMenuItem = new ToolStripMenuItem();
        оПрограммеToolStripMenuItem = new ToolStripMenuItem();
        addButton = new Button();
        stockNumeric = new NumericUpDown();
        stockLabel = new Label();
        priceNumeric = new NumericUpDown();
        priceLabel = new Label();
        categoryTextBox = new TextBox();
        categoryLabel = new Label();
        manufacturerTextBox = new TextBox();
        manufacturerLabel = new Label();
        nameTextBox = new TextBox();
        nameLabel = new Label();
        productsGrid = new DataGridView();
        searchPanel = new Panel();
        searchTextBox = new TextBox();
        searchLabel = new Label();
        topPanel = new FlowLayoutPanel();
        statusLabel = new Label();
        progressBar = new ProgressBar();
        syncLoadButton = new Button();
        cancelButton = new Button();
        loadButton = new Button();
        rootLayout = new TableLayoutPanel();
        menuStrip1 = new MenuStrip();
        errorProvider = new ErrorProvider(components);
        newProductGroupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)stockNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)priceNumeric).BeginInit();
        ((System.ComponentModel.ISupportInitialize)productsGrid).BeginInit();
        searchPanel.SuspendLayout();
        topPanel.SuspendLayout();
        rootLayout.SuspendLayout();
        menuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
        SuspendLayout();
        // 
        // newProductGroupBox
        // 
        newProductGroupBox.Controls.Add(nameLabel);
        newProductGroupBox.Controls.Add(nameTextBox);
        newProductGroupBox.Controls.Add(manufacturerLabel);
        newProductGroupBox.Controls.Add(manufacturerTextBox);
        newProductGroupBox.Controls.Add(categoryLabel);
        newProductGroupBox.Controls.Add(categoryTextBox);
        newProductGroupBox.Controls.Add(priceLabel);
        newProductGroupBox.Controls.Add(priceNumeric);
        newProductGroupBox.Controls.Add(stockLabel);
        newProductGroupBox.Controls.Add(stockNumeric);
        newProductGroupBox.Controls.Add(addButton);
        newProductGroupBox.Controls.Add(menuStrip1);
        newProductGroupBox.Dock = DockStyle.Fill;
        newProductGroupBox.Location = new Point(15, 700);
        newProductGroupBox.Margin = new Padding(4, 5, 4, 5);
        newProductGroupBox.Name = "newProductGroupBox";
        newProductGroupBox.Padding = new Padding(4, 5, 4, 5);
        newProductGroupBox.Size = new Size(1656, 332);
        newProductGroupBox.TabIndex = 3;
        newProductGroupBox.TabStop = false;
        newProductGroupBox.Text = "Новый товар";
        // 
        // miniToolStrip
        // 
        miniToolStrip.AccessibleName = "Выбор нового элемента";
        miniToolStrip.AccessibleRole = AccessibleRole.ComboBox;
        miniToolStrip.AutoSize = false;
        miniToolStrip.Dock = DockStyle.None;
        miniToolStrip.ImageScalingSize = new Size(24, 24);
        miniToolStrip.Location = new Point(103, 2);
        miniToolStrip.Name = "miniToolStrip";
        miniToolStrip.Size = new Size(1648, 33);
        miniToolStrip.TabIndex = 11;
        miniToolStrip.ItemClicked += menuStrip1_ItemClicked;
        // 
        // справкаToolStripMenuItem
        // 
        справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { руководствоПользователяToolStripMenuItem, оПрограммеToolStripMenuItem });
        справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
        справкаToolStripMenuItem.Size = new Size(97, 29);
        справкаToolStripMenuItem.Text = "Справка";
        // 
        // руководствоПользователяToolStripMenuItem
        // 
        руководствоПользователяToolStripMenuItem.Name = "руководствоПользователяToolStripMenuItem";
        руководствоПользователяToolStripMenuItem.Size = new Size(336, 34);
        руководствоПользователяToolStripMenuItem.Text = "Руководство пользователя";
        руководствоПользователяToolStripMenuItem.Click += руководствоПользователяToolStripMenuItem_Click;
        // 
        // оПрограммеToolStripMenuItem
        // 
        оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
        оПрограммеToolStripMenuItem.Size = new Size(336, 34);
        оПрограммеToolStripMenuItem.Text = "О программе";
        оПрограммеToolStripMenuItem.Click += оПрограммеToolStripMenuItem_Click;
        // 
        // addButton
        // 
        addButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        addButton.Location = new Point(611, 210);
        addButton.Margin = new Padding(4, 5, 4, 5);
        addButton.Name = "addButton";
        addButton.Size = new Size(160, 45);
        addButton.TabIndex = 10;
        addButton.Text = "Добавить товар";
        addButton.UseVisualStyleBackColor = true;
        // 
        // stockNumeric
        // 
        stockNumeric.Location = new Point(179, 203);
        stockNumeric.Margin = new Padding(4, 5, 4, 5);
        stockNumeric.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        stockNumeric.Name = "stockNumeric";
        stockNumeric.Size = new Size(214, 31);
        stockNumeric.TabIndex = 9;
        toolTip.SetToolTip(stockNumeric, "Количество товара на складе от 0 до 10 000 штук.");
        stockNumeric.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // stockLabel
        // 
        stockLabel.AutoSize = true;
        stockLabel.Location = new Point(21, 210);
        stockLabel.Margin = new Padding(4, 0, 4, 0);
        stockLabel.Name = "stockLabel";
        stockLabel.Size = new Size(115, 25);
        stockLabel.TabIndex = 8;
        stockLabel.Text = "Остаток, шт.:";
        // 
        // priceNumeric
        // 
        priceNumeric.DecimalPlaces = 2;
        priceNumeric.Location = new Point(886, 130);
        priceNumeric.Margin = new Padding(4, 5, 4, 5);
        priceNumeric.Maximum = new decimal(new int[] { 2000000, 0, 0, 0 });
        priceNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        priceNumeric.Name = "priceNumeric";
        priceNumeric.Size = new Size(257, 31);
        priceNumeric.TabIndex = 7;
        priceNumeric.ThousandsSeparator = true;
        toolTip.SetToolTip(priceNumeric, "Цена от 1 до 2 000 000 рублей. Дробная часть — до двух знаков.");
        priceNumeric.Value = new decimal(new int[] { 5290, 0, 0, 0 });
        // 
        // priceLabel
        // 
        priceLabel.AutoSize = true;
        priceLabel.Location = new Point(714, 137);
        priceLabel.Margin = new Padding(4, 0, 4, 0);
        priceLabel.Name = "priceLabel";
        priceLabel.Size = new Size(100, 25);
        priceLabel.TabIndex = 6;
        priceLabel.Text = "Цена, руб.:";
        // 
        // categoryTextBox
        // 
        categoryTextBox.Location = new Point(179, 130);
        categoryTextBox.Margin = new Padding(4, 5, 4, 5);
        categoryTextBox.Name = "categoryTextBox";
        categoryTextBox.Size = new Size(498, 31);
        categoryTextBox.TabIndex = 5;
        toolTip.SetToolTip(categoryTextBox, "Например, Процессоры, Периферия или Накопители.");
        // 
        // categoryLabel
        // 
        categoryLabel.AutoSize = true;
        categoryLabel.Location = new Point(21, 137);
        categoryLabel.Margin = new Padding(4, 0, 4, 0);
        categoryLabel.Name = "categoryLabel";
        categoryLabel.Size = new Size(99, 25);
        categoryLabel.TabIndex = 4;
        categoryLabel.Text = "Категория:";
        // 
        // manufacturerTextBox
        // 
        manufacturerTextBox.Location = new Point(886, 57);
        manufacturerTextBox.Margin = new Padding(4, 5, 4, 5);
        manufacturerTextBox.Name = "manufacturerTextBox";
        manufacturerTextBox.Size = new Size(498, 31);
        manufacturerTextBox.TabIndex = 3;
        toolTip.SetToolTip(manufacturerTextBox, "Например, AMD, Logitech или Samsung.");
        // 
        // manufacturerLabel
        // 
        manufacturerLabel.AutoSize = true;
        manufacturerLabel.Location = new Point(714, 63);
        manufacturerLabel.Margin = new Padding(4, 0, 4, 0);
        manufacturerLabel.Name = "manufacturerLabel";
        manufacturerLabel.Size = new Size(144, 25);
        manufacturerLabel.TabIndex = 2;
        manufacturerLabel.Text = "Производитель:";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(179, 57);
        nameTextBox.Margin = new Padding(4, 5, 4, 5);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(498, 31);
        nameTextBox.TabIndex = 1;
        toolTip.SetToolTip(nameTextBox, "Укажите название товара от 3 до 150 символов.");
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(21, 63);
        nameLabel.Margin = new Padding(4, 0, 4, 0);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(94, 25);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Название:";
        // 
        // productsGrid
        // 
        productsGrid.AllowUserToAddRows = false;
        productsGrid.AllowUserToDeleteRows = false;
        productsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        productsGrid.BackgroundColor = SystemColors.Window;
        productsGrid.ColumnHeadersHeight = 34;
        productsGrid.Dock = DockStyle.Fill;
        productsGrid.Location = new Point(15, 188);
        productsGrid.Margin = new Padding(4, 5, 4, 5);
        productsGrid.MultiSelect = false;
        productsGrid.Name = "productsGrid";
        productsGrid.ReadOnly = true;
        productsGrid.RowHeadersVisible = false;
        productsGrid.RowHeadersWidth = 62;
        productsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        productsGrid.Size = new Size(1656, 502);
        productsGrid.TabIndex = 2;
        // 
        // searchPanel
        // 
        searchPanel.Controls.Add(searchLabel);
        searchPanel.Controls.Add(searchTextBox);
        searchPanel.Dock = DockStyle.Fill;
        searchPanel.Location = new Point(15, 108);
        searchPanel.Margin = new Padding(4, 5, 4, 5);
        searchPanel.Name = "searchPanel";
        searchPanel.Size = new Size(1656, 70);
        searchPanel.TabIndex = 1;
        // 
        // searchTextBox
        // 
        searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        searchTextBox.Location = new Point(214, 15);
        searchTextBox.Margin = new Padding(4, 5, 4, 5);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.Size = new Size(1925, 31);
        searchTextBox.TabIndex = 1;
        toolTip.SetToolTip(searchTextBox, "Введите часть названия товара для фильтрации уже загруженного списка.");
        // 
        // searchLabel
        // 
        searchLabel.AutoSize = true;
        searchLabel.Location = new Point(11, 22);
        searchLabel.Margin = new Padding(4, 0, 4, 0);
        searchLabel.Name = "searchLabel";
        searchLabel.Size = new Size(179, 25);
        searchLabel.TabIndex = 0;
        searchLabel.Text = "Поиск по названию:";
        // 
        // topPanel
        // 
        topPanel.Controls.Add(loadButton);
        topPanel.Controls.Add(cancelButton);
        topPanel.Controls.Add(syncLoadButton);
        topPanel.Controls.Add(progressBar);
        topPanel.Controls.Add(statusLabel);
        topPanel.Dock = DockStyle.Fill;
        topPanel.Location = new Point(15, 18);
        topPanel.Margin = new Padding(4, 5, 4, 5);
        topPanel.Name = "topPanel";
        topPanel.Padding = new Padding(3, 12, 3, 3);
        topPanel.Size = new Size(1656, 80);
        topPanel.TabIndex = 0;
        topPanel.WrapContents = false;
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(783, 29);
        statusLabel.Margin = new Padding(14, 17, 0, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(70, 25);
        statusLabel.TabIndex = 4;
        statusLabel.Text = "Готово";
        // 
        // progressBar
        // 
        progressBar.Location = new Point(583, 27);
        progressBar.Margin = new Padding(17, 15, 7, 0);
        progressBar.MarqueeAnimationSpeed = 25;
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(179, 30);
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.TabIndex = 3;
        progressBar.Visible = false;
        // 
        // syncLoadButton
        // 
        syncLoadButton.ForeColor = Color.DarkRed;
        syncLoadButton.Location = new Point(326, 17);
        syncLoadButton.Margin = new Padding(4, 5, 4, 5);
        syncLoadButton.Name = "syncLoadButton";
        syncLoadButton.Size = new Size(236, 57);
        syncLoadButton.TabIndex = 2;
        syncLoadButton.Text = "Эксперимент";
        // 
        // cancelButton
        // 
        cancelButton.Enabled = false;
        cancelButton.Location = new Point(175, 17);
        cancelButton.Margin = new Padding(4, 5, 4, 5);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(143, 57);
        cancelButton.TabIndex = 1;
        cancelButton.Text = "Отмена";
        // 
        // loadButton
        // 
        loadButton.Location = new Point(7, 17);
        loadButton.Margin = new Padding(4, 5, 4, 5);
        loadButton.Name = "loadButton";
        loadButton.Size = new Size(160, 57);
        loadButton.TabIndex = 0;
        loadButton.Text = "Загрузить";
        // 
        // rootLayout
        // 
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(topPanel, 0, 0);
        rootLayout.Controls.Add(searchPanel, 0, 1);
        rootLayout.Controls.Add(productsGrid, 0, 2);
        rootLayout.Controls.Add(newProductGroupBox, 0, 3);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Margin = new Padding(4, 5, 4, 5);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(11, 13, 11, 13);
        rootLayout.RowCount = 4;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 342F));
        rootLayout.Size = new Size(1686, 1050);
        rootLayout.TabIndex = 0;
        // 
        // menuStrip1
        // 
        menuStrip1.ImageScalingSize = new Size(24, 24);
        menuStrip1.Items.AddRange(new ToolStripItem[] { справкаToolStripMenuItem });
        menuStrip1.Location = new Point(4, 29);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1648, 33);
        menuStrip1.TabIndex = 11;
        menuStrip1.Text = "menuStrip1";
        menuStrip1.ItemClicked += menuStrip1_ItemClicked;
        // 
        // errorProvider1
        // 
        errorProvider.ContainerControl = this;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1686, 1050);
        Controls.Add(rootLayout);
        MainMenuStrip = miniToolStrip;
        Margin = new Padding(4, 5, 4, 5);
        MinimumSize = new Size(1333, 1006);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Магазин компьютерной техники — каталог товаров";
        newProductGroupBox.ResumeLayout(false);
        newProductGroupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)stockNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)priceNumeric).EndInit();
        ((System.ComponentModel.ISupportInitialize)productsGrid).EndInit();
        searchPanel.ResumeLayout(false);
        searchPanel.PerformLayout();
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        rootLayout.ResumeLayout(false);
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
        ResumeLayout(false);
    }
    private HelpProvider helpProvider=null!;
    private GroupBox newProductGroupBox;
    private Label nameLabel;
    private TextBox nameTextBox;
    private Label manufacturerLabel;
    private TextBox manufacturerTextBox;
    private Label categoryLabel;
    private TextBox categoryTextBox;
    private Label priceLabel;
    private NumericUpDown priceNumeric;
    private Label stockLabel;
    private NumericUpDown stockNumeric;
    private Button addButton;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem справкаToolStripMenuItem;
    private ToolStripMenuItem руководствоПользователяToolStripMenuItem;
    private ToolStripMenuItem оПрограммеToolStripMenuItem;
    private MenuStrip miniToolStrip;
    private DataGridView productsGrid;
    private Panel searchPanel;
    private Label searchLabel;
    private TextBox searchTextBox;
    private FlowLayoutPanel topPanel;
    private Button loadButton;
    private Button cancelButton;
    private Button syncLoadButton;
    private ProgressBar progressBar;
    private Label statusLabel;
    private TableLayoutPanel rootLayout;
    private ErrorProvider errorProvider = null!;
}
