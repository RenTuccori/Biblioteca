namespace Biblioteca.UI.Desktop
{
    partial class FormUsuarios
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
            this.groupBox1 = new GroupBox();
            this.txtEmail = new TextBox();
            this.txtDni = new TextBox();
            this.txtApellido = new TextBox();
            this.txtNombre = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.cmbRol = new ComboBox();
            this.txtClave = new TextBox();
            this.txtNombreUsuario = new TextBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.flowLayoutPanelBotones = new FlowLayoutPanel();
            this.btnAgregar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.dgvUsuarios = new DataGridView();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutInputs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.BackColor = Color.WhiteSmoke;
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.tableLayoutInputs, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.dgvUsuarios, 0, 2);
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
            this.lblTitulo.Size = new Size(241, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Usuarios";
            // 
            // tableLayoutInputs
            // 
            this.tableLayoutInputs.BackColor = Color.White;
            this.tableLayoutInputs.ColumnCount = 2;
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutInputs.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutInputs.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutInputs.Controls.Add(this.flowLayoutPanelBotones, 1, 1);
            this.tableLayoutInputs.Dock = DockStyle.Fill;
            this.tableLayoutInputs.Location = new Point(18, 78);
            this.tableLayoutInputs.Name = "tableLayoutInputs";
            this.tableLayoutInputs.Padding = new Padding(20);
            this.tableLayoutInputs.RowCount = 2;
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutInputs.Size = new Size(964, 260);
            this.tableLayoutInputs.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtDni);
            this.groupBox1.Controls.Add(this.txtApellido);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.groupBox1.ForeColor = Color.DarkSlateBlue;
            this.groupBox1.Location = new Point(23, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(15);
            this.groupBox1.Size = new Size(456, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Personales";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtEmail.Font = new Font("Segoe UI", 9F);
            this.txtEmail.Location = new Point(120, 150);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "usuario@ejemplo.com";
            this.txtEmail.Size = new Size(316, 23);
            this.txtEmail.TabIndex = 7;
            // 
            // txtDni
            // 
            this.txtDni.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtDni.Font = new Font("Segoe UI", 9F);
            this.txtDni.Location = new Point(120, 115);
            this.txtDni.Name = "txtDni";
            this.txtDni.PlaceholderText = "12345678";
            this.txtDni.Size = new Size(316, 23);
            this.txtDni.TabIndex = 6;
            // 
            // txtApellido
            // 
            this.txtApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtApellido.Font = new Font("Segoe UI", 9F);
            this.txtApellido.Location = new Point(120, 80);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new Size(316, 23);
            this.txtApellido.TabIndex = 5;
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtNombre.Font = new Font("Segoe UI", 9F);
            this.txtNombre.Location = new Point(120, 45);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new Size(316, 23);
            this.txtNombre.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Segoe UI", 9F);
            this.label4.ForeColor = Color.Black;
            this.label4.Location = new Point(20, 153);
            this.label4.Name = "label4";
            this.label4.Size = new Size(39, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Segoe UI", 9F);
            this.label3.ForeColor = Color.Black;
            this.label3.Location = new Point(20, 118);
            this.label3.Name = "label3";
            this.label3.Size = new Size(30, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "DNI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Segoe UI", 9F);
            this.label2.ForeColor = Color.Black;
            this.label2.Location = new Point(20, 83);
            this.label2.Name = "label2";
            this.label2.Size = new Size(54, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Apellido:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 9F);
            this.label1.ForeColor = Color.Black;
            this.label1.Location = new Point(20, 48);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox2.Controls.Add(this.cmbRol);
            this.groupBox2.Controls.Add(this.txtClave);
            this.groupBox2.Controls.Add(this.txtNombreUsuario);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.groupBox2.ForeColor = Color.DarkSlateBlue;
            this.groupBox2.Location = new Point(485, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(15);
            this.groupBox2.Size = new Size(456, 150);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Usuario";
            // 
            // cmbRol
            // 
            this.cmbRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRol.Font = new Font("Segoe UI", 9F);
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new Point(150, 110);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new Size(286, 23);
            this.cmbRol.TabIndex = 5;
            // 
            // txtClave
            // 
            this.txtClave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtClave.Font = new Font("Segoe UI", 9F);
            this.txtClave.Location = new Point(150, 75);
            this.txtClave.Name = "txtClave";
            this.txtClave.PlaceholderText = "Contraseña";
            this.txtClave.Size = new Size(286, 23);
            this.txtClave.TabIndex = 4;
            this.txtClave.UseSystemPasswordChar = true;
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtNombreUsuario.Font = new Font("Segoe UI", 9F);
            this.txtNombreUsuario.Location = new Point(150, 40);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.PlaceholderText = "usuario123";
            this.txtNombreUsuario.Size = new Size(286, 23);
            this.txtNombreUsuario.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Segoe UI", 9F);
            this.label7.ForeColor = Color.Black;
            this.label7.Location = new Point(20, 113);
            this.label7.Name = "label7";
            this.label7.Size = new Size(27, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Rol:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Segoe UI", 9F);
            this.label6.ForeColor = Color.Black;
            this.label6.Location = new Point(20, 78);
            this.label6.Name = "label6";
            this.label6.Size = new Size(39, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Clave:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Segoe UI", 9F);
            this.label5.ForeColor = Color.Black;
            this.label5.Location = new Point(20, 43);
            this.label5.Name = "label5";
            this.label5.Size = new Size(110, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nombre de Usuario:";
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.flowLayoutPanelBotones.AutoSize = true;
            this.flowLayoutPanelBotones.Controls.Add(this.btnAgregar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnModificar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnEliminar);
            this.flowLayoutPanelBotones.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanelBotones.Location = new Point(626, 229);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new Size(315, 36);
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
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = Color.White;
            this.dgvUsuarios.BorderStyle = BorderStyle.None;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            this.dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            this.dgvUsuarios.Dock = DockStyle.Fill;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.GridColor = Color.LightGray;
            this.dgvUsuarios.Location = new Point(18, 344);
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowTemplate.Height = 30;
            this.dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new Size(964, 238);
            this.dgvUsuarios.TabIndex = 2;
            this.dgvUsuarios.SelectionChanged += new EventHandler(this.dgvUsuarios_SelectionChanged);
            // 
            // FormUsuarios
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.tableLayoutPrincipal);
            this.MinimumSize = new Size(800, 500);
            this.Name = "FormUsuarios";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios - Biblioteca";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormUsuarios_Load);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableLayoutInputs.ResumeLayout(false);
            this.tableLayoutInputs.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPrincipal;
        private Panel panelHeader;
        private Label lblTitulo;
        private TableLayoutPanel tableLayoutInputs;
        private GroupBox groupBox1;
        private TextBox txtEmail;
        private TextBox txtDni;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private ComboBox cmbRol;
        private TextBox txtClave;
        private TextBox txtNombreUsuario;
        private Label label7;
        private Label label6;
        private Label label5;
        private FlowLayoutPanel flowLayoutPanelBotones;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private DataGridView dgvUsuarios;
    }
}
