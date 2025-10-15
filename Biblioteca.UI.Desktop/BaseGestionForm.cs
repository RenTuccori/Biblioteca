namespace Biblioteca.UI.Desktop
{
    public partial class BaseGestionForm : Form
    {
        protected TableLayoutPanel tableLayoutPrincipal;
        protected Panel panelHeader;
        protected Label lblTitulo;
        protected DataGridView dgvDatos;

        public BaseGestionForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(800, 500);
            this.BackColor = Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected virtual void InitializeComponent()
        {
            this.tableLayoutPrincipal = new TableLayoutPanel();
            this.panelHeader = new Panel();
            this.lblTitulo = new Label();
            this.dgvDatos = new DataGridView();
            
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();

            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.BackColor = Color.WhiteSmoke;
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
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
            this.lblTitulo.Size = new Size(200, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Título del Formulario";

            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatos.BackgroundColor = Color.White;
            this.dgvDatos.BorderStyle = BorderStyle.None;
            this.dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            this.dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvDatos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            this.dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            this.dgvDatos.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.dgvDatos.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            this.dgvDatos.Dock = DockStyle.Fill;
            this.dgvDatos.EnableHeadersVisualStyles = false;
            this.dgvDatos.GridColor = Color.LightGray;
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.RowTemplate.Height = 30;
            this.dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.TabIndex = 2;

            // 
            // BaseGestionForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.tableLayoutPrincipal);
            this.Name = "BaseGestionForm";

            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
        }

        protected void ConfigurarDataGridView()
        {
            // Configuración adicional común para todos los DataGridView
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        protected Button CrearBoton(string texto, Color colorFondo, Color colorTexto, EventHandler clickHandler)
        {
            var boton = new Button
            {
                Text = texto,
                BackColor = colorFondo,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(100, 30),
                UseVisualStyleBackColor = false
            };
            
            boton.FlatAppearance.BorderSize = 0;
            boton.Click += clickHandler;
            
            return boton;
        }

        protected TextBox CrearTextBox(string placeholder = "")
        {
            var textBox = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = placeholder
            };
            
            return textBox;
        }

        protected Label CrearLabel(string texto, bool esTitulo = false)
        {
            var label = new Label
            {
                Text = texto,
                AutoSize = true,
                Font = esTitulo ? new Font("Segoe UI", 10F, FontStyle.Bold) : new Font("Segoe UI", 10F),
                ForeColor = esTitulo ? Color.DarkSlateBlue : Color.Black
            };
            
            return label;
        }
    }
}