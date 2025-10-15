namespace Biblioteca.UI.Desktop
{
    partial class FormAutores
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
            this.lblBuscar = new Label();
            this.txtBuscar = new TextBox();
            this.btnBuscar = new Button();
            this.lblNombre = new Label();
            this.txtNombreAutor = new TextBox();
            this.lblApellido = new Label();
            this.txtApellidoAutor = new TextBox();
            this.flowLayoutPanelBotones = new FlowLayoutPanel();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.dgvAutores = new DataGridView();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutInputs.SuspendLayout();
            this.flowLayoutPanelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAutores)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.BackColor = Color.WhiteSmoke;
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.tableLayoutInputs, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.dgvAutores, 0, 2);
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
            this.lblTitulo.Size = new Size(229, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "✍️ Gestión de Autores";
            // 
            // tableLayoutInputs
            // 
            this.tableLayoutInputs.BackColor = Color.White;
            this.tableLayoutInputs.ColumnCount = 3;
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.Controls.Add(this.lblBuscar, 0, 0);
            this.tableLayoutInputs.Controls.Add(this.txtBuscar, 1, 0);
            this.tableLayoutInputs.Controls.Add(this.btnBuscar, 2, 0);
            this.tableLayoutInputs.Controls.Add(this.lblNombre, 0, 1);
            this.tableLayoutInputs.Controls.Add(this.txtNombreAutor, 1, 1);
            this.tableLayoutInputs.Controls.Add(this.lblApellido, 0, 2);
            this.tableLayoutInputs.Controls.Add(this.txtApellidoAutor, 1, 2);
            this.tableLayoutInputs.Controls.Add(this.flowLayoutPanelBotones, 1, 3);
            this.tableLayoutInputs.Dock = DockStyle.Fill;
            this.tableLayoutInputs.Location = new Point(18, 78);
            this.tableLayoutInputs.Name = "tableLayoutInputs";
            this.tableLayoutInputs.Padding = new Padding(20);
            this.tableLayoutInputs.RowCount = 4;
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.Size = new Size(964, 180);
            this.tableLayoutInputs.TabIndex = 1;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Anchor = AnchorStyles.Left;
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblBuscar.ForeColor = Color.DarkSlateBlue;
            this.lblBuscar.Location = new Point(23, 28);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new Size(55, 19);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "🔍 Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtBuscar.Font = new Font("Segoe UI", 10F);
            this.txtBuscar.Location = new Point(110, 25);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "Buscar por nombre o apellido...";
            this.txtBuscar.Size = new Size(750, 25);
            this.txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = AnchorStyles.Left;
            this.btnBuscar.BackColor = Color.LightSkyBlue;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = FlatStyle.Flat;
            this.btnBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnBuscar.ForeColor = Color.DarkBlue;
            this.btnBuscar.Location = new Point(866, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new Size(75, 29);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new EventHandler(this.btnBuscar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = AnchorStyles.Left;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new Font("Segoe UI", 10F);
            this.lblNombre.Location = new Point(23, 83);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new Size(67, 19);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombreAutor
            // 
            this.txtNombreAutor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtNombreAutor.Font = new Font("Segoe UI", 10F);
            this.txtNombreAutor.Location = new Point(110, 80);
            this.txtNombreAutor.Name = "txtNombreAutor";
            this.txtNombreAutor.Size = new Size(750, 25);
            this.txtNombreAutor.TabIndex = 4;
            // 
            // lblApellido
            // 
            this.lblApellido.Anchor = AnchorStyles.Left;
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new Font("Segoe UI", 10F);
            this.lblApellido.Location = new Point(23, 118);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new Size(67, 19);
            this.lblApellido.TabIndex = 5;
            this.lblApellido.Text = "Apellido:";
            // 
            // txtApellidoAutor
            // 
            this.txtApellidoAutor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtApellidoAutor.Font = new Font("Segoe UI", 10F);
            this.txtApellidoAutor.Location = new Point(110, 115);
            this.txtApellidoAutor.Name = "txtApellidoAutor";
            this.txtApellidoAutor.Size = new Size(750, 25);
            this.txtApellidoAutor.TabIndex = 6;
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.AutoSize = true;
            this.flowLayoutPanelBotones.Controls.Add(this.btnAgregar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnModificar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnEliminar);
            this.flowLayoutPanelBotones.Dock = DockStyle.Fill;
            this.flowLayoutPanelBotones.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanelBotones.Location = new Point(110, 148);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new Size(750, 29);
            this.flowLayoutPanelBotones.TabIndex = 7;
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
            this.btnAgregar.Text = "➕ Agregar";
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
            this.btnModificar.Text = "✏️ Modificar";
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
            this.btnEliminar.Text = "🗑️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);
            // 
            // dgvAutores
            // 
            this.dgvAutores.AllowUserToAddRows = false;
            this.dgvAutores.AllowUserToDeleteRows = false;
            this.dgvAutores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAutores.BackgroundColor = Color.White;
            this.dgvAutores.BorderStyle = BorderStyle.None;
            this.dgvAutores.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            this.dgvAutores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvAutores.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvAutores.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            this.dgvAutores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAutores.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            this.dgvAutores.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.dgvAutores.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            this.dgvAutores.Dock = DockStyle.Fill;
            this.dgvAutores.EnableHeadersVisualStyles = false;
            this.dgvAutores.GridColor = Color.LightGray;
            this.dgvAutores.Location = new Point(18, 264);
            this.dgvAutores.MultiSelect = false;
            this.dgvAutores.Name = "dgvAutores";
            this.dgvAutores.ReadOnly = true;
            this.dgvAutores.RowHeadersVisible = false;
            this.dgvAutores.RowTemplate.Height = 30;
            this.dgvAutores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAutores.Size = new Size(964, 318);
            this.dgvAutores.TabIndex = 2;
            this.dgvAutores.SelectionChanged += new EventHandler(this.dgvAutores_SelectionChanged);
            // 
            // FormAutores
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.tableLayoutPrincipal);
            this.MinimumSize = new Size(800, 500);
            this.Name = "FormAutores";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "✍️ Gestión de Autores - Biblioteca";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormAutores_Load);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableLayoutInputs.ResumeLayout(false);
            this.tableLayoutInputs.PerformLayout();
            this.flowLayoutPanelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAutores)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPrincipal;
        private Panel panelHeader;
        private Label lblTitulo;
        private TableLayoutPanel tableLayoutInputs;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label lblNombre;
        private TextBox txtNombreAutor;
        private Label lblApellido;
        private TextBox txtApellidoAutor;
        private FlowLayoutPanel flowLayoutPanelBotones;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvAutores;
    }
}