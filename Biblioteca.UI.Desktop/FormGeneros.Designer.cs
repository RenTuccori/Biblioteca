namespace Biblioteca.UI.Desktop
{
    partial class FormGeneros
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
            this.labelNombre = new Label();
            this.txtNombreGenero = new TextBox();
            this.flowLayoutPanelBotones = new FlowLayoutPanel();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.dgvGeneros = new DataGridView();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutInputs.SuspendLayout();
            this.flowLayoutPanelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneros)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.BackColor = Color.WhiteSmoke;
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.tableLayoutInputs, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.dgvGeneros, 0, 2);
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
            this.lblTitulo.Text = "📚 Gestión de Géneros";
            // 
            // tableLayoutInputs
            // 
            this.tableLayoutInputs.BackColor = Color.White;
            this.tableLayoutInputs.ColumnCount = 2;
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutInputs.Controls.Add(this.labelNombre, 0, 0);
            this.tableLayoutInputs.Controls.Add(this.txtNombreGenero, 1, 0);
            this.tableLayoutInputs.Controls.Add(this.flowLayoutPanelBotones, 1, 1);
            this.tableLayoutInputs.Dock = DockStyle.Fill;
            this.tableLayoutInputs.Location = new Point(18, 78);
            this.tableLayoutInputs.Name = "tableLayoutInputs";
            this.tableLayoutInputs.Padding = new Padding(20);
            this.tableLayoutInputs.RowCount = 2;
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.Size = new Size(964, 100);
            this.tableLayoutInputs.TabIndex = 1;
            // 
            // labelNombre
            // 
            this.labelNombre.Anchor = AnchorStyles.Left;
            this.labelNombre.AutoSize = true;
            this.labelNombre.Font = new Font("Segoe UI", 10F);
            this.labelNombre.Location = new Point(23, 28);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new Size(67, 19);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre:";
            // 
            // txtNombreGenero
            // 
            this.txtNombreGenero.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtNombreGenero.Font = new Font("Segoe UI", 10F);
            this.txtNombreGenero.Location = new Point(96, 25);
            this.txtNombreGenero.Name = "txtNombreGenero";
            this.txtNombreGenero.PlaceholderText = "Ingrese el nombre del género...";
            this.txtNombreGenero.Size = new Size(845, 25);
            this.txtNombreGenero.TabIndex = 1;
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.AutoSize = true;
            this.flowLayoutPanelBotones.Controls.Add(this.btnAgregar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnModificar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnEliminar);
            this.flowLayoutPanelBotones.Dock = DockStyle.Fill;
            this.flowLayoutPanelBotones.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanelBotones.Location = new Point(96, 58);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new Size(845, 39);
            this.flowLayoutPanelBotones.TabIndex = 2;
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
            // dgvGeneros
            // 
            this.dgvGeneros.AllowUserToAddRows = false;
            this.dgvGeneros.AllowUserToDeleteRows = false;
            this.dgvGeneros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGeneros.BackgroundColor = Color.White;
            this.dgvGeneros.BorderStyle = BorderStyle.None;
            this.dgvGeneros.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            this.dgvGeneros.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvGeneros.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvGeneros.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            this.dgvGeneros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGeneros.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            this.dgvGeneros.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.dgvGeneros.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            this.dgvGeneros.Dock = DockStyle.Fill;
            this.dgvGeneros.EnableHeadersVisualStyles = false;
            this.dgvGeneros.GridColor = Color.LightGray;
            this.dgvGeneros.Location = new Point(18, 184);
            this.dgvGeneros.MultiSelect = false;
            this.dgvGeneros.Name = "dgvGeneros";
            this.dgvGeneros.ReadOnly = true;
            this.dgvGeneros.RowHeadersVisible = false;
            this.dgvGeneros.RowTemplate.Height = 30;
            this.dgvGeneros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvGeneros.Size = new Size(964, 398);
            this.dgvGeneros.TabIndex = 2;
            this.dgvGeneros.SelectionChanged += new EventHandler(this.dgvGeneros_SelectionChanged);
            // 
            // FormGeneros
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.tableLayoutPrincipal);
            this.MinimumSize = new Size(800, 500);
            this.Name = "FormGeneros";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "📚 Gestión de Géneros - Biblioteca";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormGenero_Load);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableLayoutInputs.ResumeLayout(false);
            this.tableLayoutInputs.PerformLayout();
            this.flowLayoutPanelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPrincipal;
        private Panel panelHeader;
        private Label lblTitulo;
        private TableLayoutPanel tableLayoutInputs;
        private Label labelNombre;
        private TextBox txtNombreGenero;
        private FlowLayoutPanel flowLayoutPanelBotones;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvGeneros;
    }
}