namespace Biblioteca.UI.Desktop
{
    partial class FormLibros
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPrincipal = new TableLayoutPanel();
            this.panelHeader = new Panel();
            this.lblTitulo = new Label();
            this.tableLayoutInputs = new TableLayoutPanel();
            this.label1 = new Label();
            this.txtTitulo = new TextBox();
            this.label2 = new Label();
            this.txtISBN = new TextBox();
            this.label3 = new Label();
            this.cmbAutor = new ComboBox();
            this.label4 = new Label();
            this.cmbGenero = new ComboBox();
            this.label5 = new Label();
            this.cmbEditorial = new ComboBox();
            this.flowLayoutPanelBotones = new FlowLayoutPanel();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.BtnEliminar = new Button();
            this.dgvLibros = new DataGridView();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutInputs.SuspendLayout();
            this.flowLayoutPanelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibros)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.BackColor = Color.WhiteSmoke;
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.tableLayoutInputs, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.dgvLibros, 0, 2);
            this.tableLayoutPrincipal.Dock = DockStyle.Fill;
            this.tableLayoutPrincipal.Location = new Point(0, 0);
            this.tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            this.tableLayoutPrincipal.Padding = new Padding(15);
            this.tableLayoutPrincipal.RowCount = 3;
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Size = new Size(1000, 600);
            this.tableLayoutPrincipal.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = Color.DarkSlateBlue;
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = DockStyle.Fill;
            this.panelHeader.Location = new Point(18, 18);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new Padding(20, 15, 20, 15);
            this.panelHeader.Size = new Size(964, 54);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(197, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Libros";
            // 
            // tableLayoutInputs
            // 
            this.tableLayoutInputs.BackColor = Color.White;
            this.tableLayoutInputs.ColumnCount = 4;
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutInputs.Controls.Add(this.label1, 0, 0);
            this.tableLayoutInputs.Controls.Add(this.txtTitulo, 1, 0);
            this.tableLayoutInputs.Controls.Add(this.label2, 2, 0);
            this.tableLayoutInputs.Controls.Add(this.txtISBN, 3, 0);
            this.tableLayoutInputs.Controls.Add(this.label3, 0, 1);
            this.tableLayoutInputs.Controls.Add(this.cmbAutor, 1, 1);
            this.tableLayoutInputs.Controls.Add(this.label4, 2, 1);
            this.tableLayoutInputs.Controls.Add(this.cmbGenero, 3, 1);
            this.tableLayoutInputs.Controls.Add(this.label5, 0, 2);
            this.tableLayoutInputs.Controls.Add(this.cmbEditorial, 1, 2);
            this.tableLayoutInputs.Controls.Add(this.flowLayoutPanelBotones, 3, 2);
            this.tableLayoutInputs.Dock = DockStyle.Fill;
            this.tableLayoutInputs.Location = new Point(18, 78);
            this.tableLayoutInputs.Name = "tableLayoutInputs";
            this.tableLayoutInputs.Padding = new Padding(20);
            this.tableLayoutInputs.RowCount = 3;
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.Size = new Size(964, 150);
            this.tableLayoutInputs.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 10F);
            this.label1.Location = new Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Título:";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtTitulo.Font = new Font("Segoe UI", 10F);
            this.txtTitulo.Location = new Point(110, 25);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new Size(380, 25);
            this.txtTitulo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Segoe UI", 10F);
            this.label2.Location = new Point(496, 28);
            this.label2.Name = "label2";
            this.label2.Size = new Size(42, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "ISBN:";
            // 
            // txtISBN
            // 
            this.txtISBN.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtISBN.Font = new Font("Segoe UI", 10F);
            this.txtISBN.Location = new Point(572, 25);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new Size(369, 25);
            this.txtISBN.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Segoe UI", 10F);
            this.label3.Location = new Point(23, 68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(49, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Autor:";
            // 
            // cmbAutor
            // 
            this.cmbAutor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbAutor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAutor.Font = new Font("Segoe UI", 10F);
            this.cmbAutor.FormattingEnabled = true;
            this.cmbAutor.Location = new Point(110, 64);
            this.cmbAutor.Name = "cmbAutor";
            this.cmbAutor.Size = new Size(380, 25);
            this.cmbAutor.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Segoe UI", 10F);
            this.label4.Location = new Point(496, 68);
            this.label4.Name = "label4";
            this.label4.Size = new Size(58, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Género:";
            // 
            // cmbGenero
            // 
            this.cmbGenero.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGenero.Font = new Font("Segoe UI", 10F);
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Location = new Point(572, 64);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new Size(369, 25);
            this.cmbGenero.TabIndex = 7;
            this.cmbGenero.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label5
            // 
            this.label5.Anchor = AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Segoe UI", 10F);
            this.label5.Location = new Point(23, 108);
            this.label5.Name = "label5";
            this.label5.Size = new Size(68, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Editorial:";
            // 
            // cmbEditorial
            // 
            this.cmbEditorial.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbEditorial.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbEditorial.Font = new Font("Segoe UI", 10F);
            this.cmbEditorial.FormattingEnabled = true;
            this.cmbEditorial.Location = new Point(110, 104);
            this.cmbEditorial.Name = "cmbEditorial";
            this.cmbEditorial.Size = new Size(380, 25);
            this.cmbEditorial.TabIndex = 9;
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.Anchor = AnchorStyles.Right;
            this.flowLayoutPanelBotones.AutoSize = true;
            this.flowLayoutPanelBotones.Controls.Add(this.btnAgregar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnModificar);
            this.flowLayoutPanelBotones.Controls.Add(this.BtnEliminar);
            this.flowLayoutPanelBotones.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanelBotones.Location = new Point(572, 102);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new Size(330, 30);
            this.flowLayoutPanelBotones.TabIndex = 10;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = Color.LightGreen;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = FlatStyle.Flat;
            this.btnAgregar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAgregar.ForeColor = Color.DarkGreen;
            this.btnAgregar.Location = new Point(3, 3);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new Size(100, 30);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "+ Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = Color.LightGoldenrodYellow;
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatStyle = FlatStyle.Flat;
            this.btnModificar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnModificar.ForeColor = Color.DarkGoldenrod;
            this.btnModificar.Location = new Point(109, 3);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new Size(100, 30);
            this.btnModificar.TabIndex = 1;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new EventHandler(this.btnModificar_Click);
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.BackColor = Color.LightCoral;
            this.BtnEliminar.FlatAppearance.BorderSize = 0;
            this.BtnEliminar.FlatStyle = FlatStyle.Flat;
            this.BtnEliminar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.BtnEliminar.ForeColor = Color.DarkRed;
            this.BtnEliminar.Location = new Point(215, 3);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.Size = new Size(100, 30);
            this.BtnEliminar.TabIndex = 2;
            this.BtnEliminar.Text = "- Eliminar";
            this.BtnEliminar.UseVisualStyleBackColor = false;
            this.BtnEliminar.Click += new EventHandler(this.btnEliminar_Click);
            // 
            // dgvLibros
            // 
            this.dgvLibros.AllowUserToAddRows = false;
            this.dgvLibros.AllowUserToDeleteRows = false;
            this.dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLibros.BackgroundColor = Color.White;
            this.dgvLibros.BorderStyle = BorderStyle.None;
            this.dgvLibros.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            this.dgvLibros.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvLibros.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvLibros.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            this.dgvLibros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLibros.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            this.dgvLibros.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.dgvLibros.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            this.dgvLibros.Dock = DockStyle.Fill;
            this.dgvLibros.EnableHeadersVisualStyles = false;
            this.dgvLibros.GridColor = Color.LightGray;
            this.dgvLibros.Location = new Point(18, 234);
            this.dgvLibros.MultiSelect = false;
            this.dgvLibros.Name = "dgvLibros";
            this.dgvLibros.ReadOnly = true;
            this.dgvLibros.RowHeadersVisible = false;
            this.dgvLibros.RowTemplate.Height = 30;
            this.dgvLibros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvLibros.Size = new Size(964, 348);
            this.dgvLibros.TabIndex = 2;
            this.dgvLibros.SelectionChanged += new EventHandler(this.dgvLibros_SelectionChanged);
            // 
            // FormLibros
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.tableLayoutPrincipal);
            this.MinimumSize = new Size(800, 500);
            this.Name = "FormLibros";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Libros - Biblioteca";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormLibros_Load);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableLayoutInputs.ResumeLayout(false);
            this.tableLayoutInputs.PerformLayout();
            this.flowLayoutPanelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibros)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPrincipal;
        private Panel panelHeader;
        private Label lblTitulo;
        private TableLayoutPanel tableLayoutInputs;
        private Label label1;
        private TextBox txtTitulo;
        private Label label2;
        private TextBox txtISBN;
        private Label label3;
        private ComboBox cmbAutor;
        private Label label4;
        private ComboBox cmbGenero;
        private Label label5;
        private ComboBox cmbEditorial;
        private FlowLayoutPanel flowLayoutPanelBotones;
        private Button btnAgregar;
        private Button btnModificar;
        private Button BtnEliminar;
        private DataGridView dgvLibros;
    }
}