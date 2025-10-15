namespace Biblioteca.UI.Desktop
{
    partial class FormPrestamos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPrincipal = new TableLayoutPanel();
            this.panelHeader = new Panel();
            this.lblTitulo = new Label();
            this.tableLayoutInputs = new TableLayoutPanel();
            this.label1 = new Label();
            this.cmbLibro = new ComboBox();
            this.label2 = new Label();
            this.cmbSocio = new ComboBox();
            this.label3 = new Label();
            this.dtpFechaPrestamo = new DateTimePicker();
            this.label4 = new Label();
            this.dtpFechaDevolucionPrevista = new DateTimePicker();
            this.groupBox1 = new GroupBox();
            this.btnVerTodos = new Button();
            this.btnVerVencidos = new Button();
            this.btnVerActivos = new Button();
            this.flowLayoutPanelBotones = new FlowLayoutPanel();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.btnDevolver = new Button();
            this.panelStatus = new Panel();
            this.lblEstado = new Label();
            this.dgvPrestamos = new DataGridView();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutInputs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanelBotones.SuspendLayout();
            this.panelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamos)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.BackColor = Color.WhiteSmoke;
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.tableLayoutInputs, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.panelStatus, 0, 2);
            this.tableLayoutPrincipal.Controls.Add(this.dgvPrestamos, 0, 3);
            this.tableLayoutPrincipal.Dock = DockStyle.Fill;
            this.tableLayoutPrincipal.Location = new Point(0, 0);
            this.tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            this.tableLayoutPrincipal.Padding = new Padding(15);
            this.tableLayoutPrincipal.RowCount = 4;
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
            this.lblTitulo.Size = new Size(265, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Préstamos";
            // 
            // tableLayoutInputs
            // 
            this.tableLayoutInputs.BackColor = Color.White;
            this.tableLayoutInputs.ColumnCount = 5;
            this.tableLayoutInputs.ColumnStyles.Clear();
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            this.tableLayoutInputs.Controls.Add(this.label1, 0, 0);
            this.tableLayoutInputs.Controls.Add(this.cmbLibro, 1, 0);
            this.tableLayoutInputs.Controls.Add(this.label2, 2, 0);
            this.tableLayoutInputs.Controls.Add(this.cmbSocio, 3, 0);
            this.tableLayoutInputs.Controls.Add(this.label3, 0, 1);
            this.tableLayoutInputs.Controls.Add(this.dtpFechaPrestamo, 1, 1);
            this.tableLayoutInputs.Controls.Add(this.label4, 2, 1);
            this.tableLayoutInputs.Controls.Add(this.dtpFechaDevolucionPrevista, 3, 1);
            this.tableLayoutInputs.Controls.Add(this.groupBox1, 4, 0);
            this.tableLayoutInputs.Controls.Add(this.flowLayoutPanelBotones, 0, 2);
            this.tableLayoutInputs.Dock = DockStyle.Fill;
            this.tableLayoutInputs.Location = new Point(18, 78);
            this.tableLayoutInputs.Name = "tableLayoutInputs";
            this.tableLayoutInputs.Padding = new Padding(20);
            this.tableLayoutInputs.RowCount = 3;
            this.tableLayoutInputs.RowStyles.Clear();
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.Size = new Size(964, 160);
            this.tableLayoutInputs.TabIndex = 1;
            // RowSpan para el GroupBox (filtros)
            this.tableLayoutInputs.SetRowSpan(this.groupBox1, 2);
            // ColumnSpan para los botones (que ocupen todo el ancho)
            this.tableLayoutInputs.SetColumnSpan(this.flowLayoutPanelBotones, 5);
            // 
            // label1
            // 
            this.label1.Anchor = AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 10F);
            this.label1.Location = new Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new Size(44, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Libro:";
            // 
            // cmbLibro
            // 
            this.cmbLibro.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbLibro.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLibro.Font = new Font("Segoe UI", 9F);
            this.cmbLibro.FormattingEnabled = true;
            this.cmbLibro.Location = new Point(73, 25);
            this.cmbLibro.Name = "cmbLibro";
            this.cmbLibro.Size = new Size(288, 23);
            this.cmbLibro.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Segoe UI", 10F);
            this.label2.Location = new Point(367, 28);
            this.label2.Name = "label2";
            this.label2.Size = new Size(47, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Socio:";
            // 
            // cmbSocio
            // 
            this.cmbSocio.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbSocio.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSocio.Font = new Font("Segoe UI", 9F);
            this.cmbSocio.FormattingEnabled = true;
            this.cmbSocio.Location = new Point(420, 25);
            this.cmbSocio.Name = "cmbSocio";
            this.cmbSocio.Size = new Size(288, 23);
            this.cmbSocio.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Segoe UI", 10F);
            this.label3.Location = new Point(23, 68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(116, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha Préstamo:";
            // 
            // dtpFechaPrestamo
            // 
            this.dtpFechaPrestamo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.dtpFechaPrestamo.Font = new Font("Segoe UI", 9F);
            this.dtpFechaPrestamo.Format = DateTimePickerFormat.Short;
            this.dtpFechaPrestamo.Location = new Point(138, 65);
            this.dtpFechaPrestamo.Name = "dtpFechaPrestamo";
            this.dtpFechaPrestamo.Size = new Size(223, 23);
            this.dtpFechaPrestamo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Segoe UI", 10F);
            this.label4.Location = new Point(367, 68);
            this.label4.Name = "label4";
            this.label4.Size = new Size(134, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Devolución Prevista:";
            // 
            // dtpFechaDevolucionPrevista
            // 
            this.dtpFechaDevolucionPrevista.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.dtpFechaDevolucionPrevista.Font = new Font("Segoe UI", 9F);
            this.dtpFechaDevolucionPrevista.Format = DateTimePickerFormat.Short;
            this.dtpFechaDevolucionPrevista.Location = new Point(519, 65);
            this.dtpFechaDevolucionPrevista.Name = "dtpFechaDevolucionPrevista";
            this.dtpFechaDevolucionPrevista.Size = new Size(189, 23);
            this.dtpFechaDevolucionPrevista.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Controls.Add(this.btnVerTodos);
            this.groupBox1.Controls.Add(this.btnVerVencidos);
            this.groupBox1.Controls.Add(this.btnVerActivos);
            this.groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.groupBox1.ForeColor = Color.DarkSlateBlue;
            this.groupBox1.Location = new Point(714, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(227, 105);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // btnVerTodos
            // 
            this.btnVerTodos.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.btnVerTodos.BackColor = Color.LightBlue;
            this.btnVerTodos.FlatAppearance.BorderSize = 0;
            this.btnVerTodos.FlatStyle = FlatStyle.Flat;
            this.btnVerTodos.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.btnVerTodos.ForeColor = Color.DarkBlue;
            this.btnVerTodos.Location = new Point(10, 70);
            this.btnVerTodos.Name = "btnVerTodos";
            this.btnVerTodos.Size = new Size(207, 25);
            this.btnVerTodos.TabIndex = 2;
            this.btnVerTodos.Text = "Ver Todos";
            this.btnVerTodos.UseVisualStyleBackColor = false;
            this.btnVerTodos.Click += new EventHandler(this.btnVerTodos_Click);
            // 
            // btnVerVencidos
            // 
            this.btnVerVencidos.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.btnVerVencidos.BackColor = Color.LightCoral;
            this.btnVerVencidos.FlatAppearance.BorderSize = 0;
            this.btnVerVencidos.FlatStyle = FlatStyle.Flat;
            this.btnVerVencidos.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.btnVerVencidos.ForeColor = Color.DarkRed;
            this.btnVerVencidos.Location = new Point(10, 45);
            this.btnVerVencidos.Name = "btnVerVencidos";
            this.btnVerVencidos.Size = new Size(207, 25);
            this.btnVerVencidos.TabIndex = 1;
            this.btnVerVencidos.Text = "Ver Vencidos";
            this.btnVerVencidos.UseVisualStyleBackColor = false;
            this.btnVerVencidos.Click += new EventHandler(this.btnVerVencidos_Click);
            // 
            // btnVerActivos
            // 
            this.btnVerActivos.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.btnVerActivos.BackColor = Color.LightGreen;
            this.btnVerActivos.FlatAppearance.BorderSize = 0;
            this.btnVerActivos.FlatStyle = FlatStyle.Flat;
            this.btnVerActivos.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.btnVerActivos.ForeColor = Color.DarkGreen;
            this.btnVerActivos.Location = new Point(10, 20);
            this.btnVerActivos.Name = "btnVerActivos";
            this.btnVerActivos.Size = new Size(207, 25);
            this.btnVerActivos.TabIndex = 0;
            this.btnVerActivos.Text = "Ver Activos";
            this.btnVerActivos.UseVisualStyleBackColor = false;
            this.btnVerActivos.Click += new EventHandler(this.btnVerActivos_Click);
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.AutoSize = true;
            this.flowLayoutPanelBotones.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelBotones.WrapContents = true;
            this.flowLayoutPanelBotones.Dock = DockStyle.Fill;
            this.flowLayoutPanelBotones.Controls.Add(this.btnAgregar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnModificar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnEliminar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnDevolver);
            this.flowLayoutPanelBotones.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanelBotones.Location = new Point(23, 134);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new Size(918, 36);
            this.flowLayoutPanelBotones.TabIndex = 9;
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
            // btnEliminar
            // 
            this.btnEliminar.BackColor = Color.LightCoral;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = FlatStyle.Flat;
            this.btnEliminar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEliminar.ForeColor = Color.DarkRed;
            this.btnEliminar.Location = new Point(215, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new Size(100, 30);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "- Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);
            // 
            // btnDevolver
            // 
            this.btnDevolver.BackColor = Color.PaleGreen;
            this.btnDevolver.Enabled = false;
            this.btnDevolver.FlatAppearance.BorderSize = 0;
            this.btnDevolver.FlatStyle = FlatStyle.Flat;
            this.btnDevolver.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnDevolver.ForeColor = Color.DarkGreen;
            this.btnDevolver.Location = new Point(321, 3);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new Size(100, 30);
            this.btnDevolver.TabIndex = 3;
            this.btnDevolver.Text = "Devolver";
            this.btnDevolver.UseVisualStyleBackColor = false;
            this.btnDevolver.Click += new EventHandler(this.btnDevolver_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = Color.AliceBlue;
            this.panelStatus.Controls.Add(this.lblEstado);
            this.panelStatus.Dock = DockStyle.Fill;
            this.panelStatus.Location = new Point(18, 244);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Padding = new Padding(20, 10, 20, 10);
            this.panelStatus.Size = new Size(964, 40);
            this.panelStatus.TabIndex = 2;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblEstado.ForeColor = Color.DarkSlateBlue;
            this.lblEstado.Location = new Point(20, 10);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new Size(228, 19);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Mostrando todos los préstamos";
            // 
            // dgvPrestamos
            // 
            this.dgvPrestamos.AllowUserToAddRows = false;
            this.dgvPrestamos.AllowUserToDeleteRows = false;
            this.dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrestamos.BackgroundColor = Color.White;
            this.dgvPrestamos.BorderStyle = BorderStyle.None;
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvPrestamos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            this.dgvPrestamos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrestamos.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            this.dgvPrestamos.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.dgvPrestamos.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            this.dgvPrestamos.Dock = DockStyle.Fill;
            this.dgvPrestamos.EnableHeadersVisualStyles = false;
            this.dgvPrestamos.GridColor = Color.LightGray;
            this.dgvPrestamos.Location = new Point(18, 290);
            this.dgvPrestamos.MultiSelect = false;
            this.dgvPrestamos.Name = "dgvPrestamos";
            this.dgvPrestamos.ReadOnly = true;
            this.dgvPrestamos.RowHeadersVisible = false;
            this.dgvPrestamos.RowTemplate.Height = 30;
            this.dgvPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrestamos.Size = new Size(964, 292);
            this.dgvPrestamos.TabIndex = 3;
            this.dgvPrestamos.SelectionChanged += new EventHandler(this.dgvPrestamos_SelectionChanged);
            // 
            // FormPrestamos
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.tableLayoutPrincipal);
            this.MinimumSize = new Size(800, 500);
            this.Name = "FormPrestamos";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Préstamos - Biblioteca";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormPrestamos_Load);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableLayoutInputs.ResumeLayout(false);
            this.tableLayoutInputs.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanelBotones.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPrincipal;
        private Panel panelHeader;
        private Label lblTitulo;
        private TableLayoutPanel tableLayoutInputs;
        private Label label1;
        private ComboBox cmbLibro;
        private Label label2;
        private ComboBox cmbSocio;
        private Label label3;
        private DateTimePicker dtpFechaPrestamo;
        private Label label4;
        private DateTimePicker dtpFechaDevolucionPrevista;
        private GroupBox groupBox1;
        private Button btnVerTodos;
        private Button btnVerVencidos;
        private Button btnVerActivos;
        private FlowLayoutPanel flowLayoutPanelBotones;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnDevolver;
        private Panel panelStatus;
        private Label lblEstado;
        private DataGridView dgvPrestamos;
    }
}
