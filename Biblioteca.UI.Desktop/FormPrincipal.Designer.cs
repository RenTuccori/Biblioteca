namespace Biblioteca.UI.Desktop
{
    partial class FormPrincipal
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
            this.tableLayoutMain = new TableLayoutPanel();
            this.lblTitulo = new Label();
            this.tableLayoutButtons = new TableLayoutPanel();
            this.btnGestionarGeneros = new Button();
            this.btnGestionarAutores = new Button();
            this.btnGestionarLibros = new Button();
            this.btnGestionarEditoriales = new Button();
            this.btnGestionarUsuarios = new Button();
            this.btnGestionarPrestamos = new Button();
            this.tableLayoutMain.SuspendLayout();
            this.tableLayoutButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.BackColor = Color.WhiteSmoke;
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.lblTitulo, 0, 0);
            this.tableLayoutMain.Controls.Add(this.tableLayoutButtons, 0, 1);
            this.tableLayoutMain.Dock = DockStyle.Fill;
            this.tableLayoutMain.Location = new Point(0, 0);
            this.tableLayoutMain.Margin = new Padding(4);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.Padding = new Padding(20);
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new Size(600, 400);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = AnchorStyles.None;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.DarkSlateBlue;
            this.lblTitulo.Location = new Point(115, 20);
            this.lblTitulo.Margin = new Padding(4, 0, 4, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(370, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📚 Sistema de Gestión de Biblioteca";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutButtons
            // 
            this.tableLayoutButtons.ColumnCount = 2;
            this.tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutButtons.Controls.Add(this.btnGestionarGeneros, 0, 0);
            this.tableLayoutButtons.Controls.Add(this.btnGestionarAutores, 1, 0);
            this.tableLayoutButtons.Controls.Add(this.btnGestionarLibros, 0, 1);
            this.tableLayoutButtons.Controls.Add(this.btnGestionarEditoriales, 1, 1);
            this.tableLayoutButtons.Controls.Add(this.btnGestionarUsuarios, 0, 2);
            this.tableLayoutButtons.Controls.Add(this.btnGestionarPrestamos, 1, 2);
            this.tableLayoutButtons.Dock = DockStyle.Fill;
            this.tableLayoutButtons.Location = new Point(24, 76);
            this.tableLayoutButtons.Margin = new Padding(4);
            this.tableLayoutButtons.Name = "tableLayoutButtons";
            this.tableLayoutButtons.RowCount = 3;
            this.tableLayoutButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            this.tableLayoutButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            this.tableLayoutButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            this.tableLayoutButtons.Size = new Size(552, 300);
            this.tableLayoutButtons.TabIndex = 1;
            // 
            // btnGestionarGeneros
            // 
            this.btnGestionarGeneros.Anchor = AnchorStyles.None;
            this.btnGestionarGeneros.BackColor = Color.LightSkyBlue;
            this.btnGestionarGeneros.FlatAppearance.BorderSize = 0;
            this.btnGestionarGeneros.FlatAppearance.MouseDownBackColor = Color.DeepSkyBlue;
            this.btnGestionarGeneros.FlatAppearance.MouseOverBackColor = Color.SkyBlue;
            this.btnGestionarGeneros.FlatStyle = FlatStyle.Flat;
            this.btnGestionarGeneros.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnGestionarGeneros.ForeColor = Color.DarkBlue;
            this.btnGestionarGeneros.Location = new Point(38, 25);
            this.btnGestionarGeneros.Margin = new Padding(10);
            this.btnGestionarGeneros.Name = "btnGestionarGeneros";
            this.btnGestionarGeneros.Size = new Size(200, 50);
            this.btnGestionarGeneros.TabIndex = 0;
            this.btnGestionarGeneros.Text = "📚 Gestionar Géneros";
            this.btnGestionarGeneros.UseVisualStyleBackColor = false;
            this.btnGestionarGeneros.Click += btnGestionarGeneros_Click;
            // 
            // btnGestionarAutores
            // 
            this.btnGestionarAutores.Anchor = AnchorStyles.None;
            this.btnGestionarAutores.BackColor = Color.LightGreen;
            this.btnGestionarAutores.FlatAppearance.BorderSize = 0;
            this.btnGestionarAutores.FlatAppearance.MouseDownBackColor = Color.ForestGreen;
            this.btnGestionarAutores.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen;
            this.btnGestionarAutores.FlatStyle = FlatStyle.Flat;
            this.btnGestionarAutores.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnGestionarAutores.ForeColor = Color.DarkGreen;
            this.btnGestionarAutores.Location = new Point(314, 25);
            this.btnGestionarAutores.Margin = new Padding(10);
            this.btnGestionarAutores.Name = "btnGestionarAutores";
            this.btnGestionarAutores.Size = new Size(200, 50);
            this.btnGestionarAutores.TabIndex = 1;
            this.btnGestionarAutores.Text = "✍️ Gestionar Autores";
            this.btnGestionarAutores.UseVisualStyleBackColor = false;
            this.btnGestionarAutores.Click += btnGestionarAutores_Click;
            // 
            // btnGestionarLibros
            // 
            this.btnGestionarLibros.Anchor = AnchorStyles.None;
            this.btnGestionarLibros.BackColor = Color.LightCoral;
            this.btnGestionarLibros.FlatAppearance.BorderSize = 0;
            this.btnGestionarLibros.FlatAppearance.MouseDownBackColor = Color.IndianRed;
            this.btnGestionarLibros.FlatAppearance.MouseOverBackColor = Color.Salmon;
            this.btnGestionarLibros.FlatStyle = FlatStyle.Flat;
            this.btnGestionarLibros.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnGestionarLibros.ForeColor = Color.DarkRed;
            this.btnGestionarLibros.Location = new Point(38, 125);
            this.btnGestionarLibros.Margin = new Padding(10);
            this.btnGestionarLibros.Name = "btnGestionarLibros";
            this.btnGestionarLibros.Size = new Size(200, 50);
            this.btnGestionarLibros.TabIndex = 2;
            this.btnGestionarLibros.Text = "📖 Gestionar Libros";
            this.btnGestionarLibros.UseVisualStyleBackColor = false;
            this.btnGestionarLibros.Click += btnGestionarLibros_Click;
            // 
            // btnGestionarEditoriales
            // 
            this.btnGestionarEditoriales.Anchor = AnchorStyles.None;
            this.btnGestionarEditoriales.BackColor = Color.LightGoldenrodYellow;
            this.btnGestionarEditoriales.FlatAppearance.BorderSize = 0;
            this.btnGestionarEditoriales.FlatAppearance.MouseDownBackColor = Color.Gold;
            this.btnGestionarEditoriales.FlatAppearance.MouseOverBackColor = Color.Khaki;
            this.btnGestionarEditoriales.FlatStyle = FlatStyle.Flat;
            this.btnGestionarEditoriales.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnGestionarEditoriales.ForeColor = Color.DarkGoldenrod;
            this.btnGestionarEditoriales.Location = new Point(314, 125);
            this.btnGestionarEditoriales.Margin = new Padding(10);
            this.btnGestionarEditoriales.Name = "btnGestionarEditoriales";
            this.btnGestionarEditoriales.Size = new Size(200, 50);
            this.btnGestionarEditoriales.TabIndex = 3;
            this.btnGestionarEditoriales.Text = "🏢 Gestionar Editoriales";
            this.btnGestionarEditoriales.UseVisualStyleBackColor = false;
            this.btnGestionarEditoriales.Click += btnGestionarEditoriales_Click;
            // 
            // btnGestionarUsuarios
            // 
            this.btnGestionarUsuarios.Anchor = AnchorStyles.None;
            this.btnGestionarUsuarios.BackColor = Color.Plum;
            this.btnGestionarUsuarios.FlatAppearance.BorderSize = 0;
            this.btnGestionarUsuarios.FlatAppearance.MouseDownBackColor = Color.MediumOrchid;
            this.btnGestionarUsuarios.FlatAppearance.MouseOverBackColor = Color.Violet;
            this.btnGestionarUsuarios.FlatStyle = FlatStyle.Flat;
            this.btnGestionarUsuarios.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnGestionarUsuarios.ForeColor = Color.Purple;
            this.btnGestionarUsuarios.Location = new Point(38, 225);
            this.btnGestionarUsuarios.Margin = new Padding(10);
            this.btnGestionarUsuarios.Name = "btnGestionarUsuarios";
            this.btnGestionarUsuarios.Size = new Size(200, 50);
            this.btnGestionarUsuarios.TabIndex = 4;
            this.btnGestionarUsuarios.Text = "👥 Gestionar Usuarios";
            this.btnGestionarUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionarUsuarios.Click += btnGestionarUsuarios_Click;
            // 
            // btnGestionarPrestamos
            // 
            this.btnGestionarPrestamos.Anchor = AnchorStyles.None;
            this.btnGestionarPrestamos.BackColor = Color.PaleGreen;
            this.btnGestionarPrestamos.FlatAppearance.BorderSize = 0;
            this.btnGestionarPrestamos.FlatAppearance.MouseDownBackColor = Color.MediumSeaGreen;
            this.btnGestionarPrestamos.FlatAppearance.MouseOverBackColor = Color.LightGreen;
            this.btnGestionarPrestamos.FlatStyle = FlatStyle.Flat;
            this.btnGestionarPrestamos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnGestionarPrestamos.ForeColor = Color.DarkGreen;
            this.btnGestionarPrestamos.Location = new Point(314, 225);
            this.btnGestionarPrestamos.Margin = new Padding(10);
            this.btnGestionarPrestamos.Name = "btnGestionarPrestamos";
            this.btnGestionarPrestamos.Size = new Size(200, 50);
            this.btnGestionarPrestamos.TabIndex = 5;
            this.btnGestionarPrestamos.Text = "🤝 Gestionar Préstamos";
            this.btnGestionarPrestamos.UseVisualStyleBackColor = false;
            this.btnGestionarPrestamos.Click += btnGestionarPrestamos_Click;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(600, 400);
            this.Controls.Add(this.tableLayoutMain);
            this.MinimumSize = new Size(500, 350);
            this.Name = "FormPrincipal";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "📚 Biblioteca - Sistema de Gestión";
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.tableLayoutButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutMain;
        private Label lblTitulo;
        private TableLayoutPanel tableLayoutButtons;
        private Button btnGestionarGeneros;
        private Button btnGestionarAutores;
        private Button btnGestionarLibros;
        private Button btnGestionarEditoriales;
        private Button btnGestionarUsuarios;
        private Button btnGestionarPrestamos;
    }
}