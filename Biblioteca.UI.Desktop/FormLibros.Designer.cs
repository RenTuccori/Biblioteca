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
            dgvLibros = new DataGridView();
            label1 = new Label();
            txtISBN = new TextBox();
            txtTitulo = new TextBox();
            label2 = new Label();
            label3 = new Label();
            cmbAutor = new ComboBox();
            label4 = new Label();
            cmbGenero = new ComboBox();
            label5 = new Label();
            cmbEditorial = new ComboBox();
            btnAgregar = new Button();
            btnModificar = new Button();
            BtnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLibros).BeginInit();
            SuspendLayout();
            // 
            // dgvLibros
            // 
            dgvLibros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLibros.Location = new Point(119, 228);
            dgvLibros.Margin = new Padding(3, 4, 3, 4);
            dgvLibros.Name = "dgvLibros";
            dgvLibros.RowHeadersWidth = 51;
            dgvLibros.Size = new Size(368, 236);
            dgvLibros.TabIndex = 0;
            dgvLibros.SelectionChanged += dgvLibros_SelectionChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(119, 16);
            label1.Name = "label1";
            label1.Size = new Size(47, 20);
            label1.TabIndex = 1;
            label1.Text = "Título";
            // 
            // txtISBN
            // 
            txtISBN.Location = new Point(206, 51);
            txtISBN.Margin = new Padding(3, 4, 3, 4);
            txtISBN.Name = "txtISBN";
            txtISBN.Size = new Size(188, 27);
            txtISBN.TabIndex = 2;
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(206, 12);
            txtTitulo.Margin = new Padding(3, 4, 3, 4);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(188, 27);
            txtTitulo.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(119, 55);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 4;
            label2.Text = "ISBN";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(119, 93);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 5;
            label3.Text = "Autor";
            // 
            // cmbAutor
            // 
            cmbAutor.FormattingEnabled = true;
            cmbAutor.Location = new Point(206, 89);
            cmbAutor.Margin = new Padding(3, 4, 3, 4);
            cmbAutor.Name = "cmbAutor";
            cmbAutor.Size = new Size(188, 28);
            cmbAutor.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(119, 132);
            label4.Name = "label4";
            label4.Size = new Size(57, 20);
            label4.TabIndex = 7;
            label4.Text = "Género";
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(206, 128);
            cmbGenero.Margin = new Padding(3, 4, 3, 4);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(188, 28);
            cmbGenero.TabIndex = 8;
            cmbGenero.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(119, 171);
            label5.Name = "label5";
            label5.Size = new Size(64, 20);
            label5.TabIndex = 12;
            label5.Text = "Editorial";
            // 
            // cmbEditorial
            // 
            cmbEditorial.FormattingEnabled = true;
            cmbEditorial.Location = new Point(206, 167);
            cmbEditorial.Margin = new Padding(3, 4, 3, 4);
            cmbEditorial.Name = "cmbEditorial";
            cmbEditorial.Size = new Size(188, 28);
            cmbEditorial.TabIndex = 13;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(401, 11);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(86, 31);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(401, 53);
            btnModificar.Margin = new Padding(3, 4, 3, 4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(86, 31);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // BtnEliminar
            // 
            BtnEliminar.Location = new Point(401, 92);
            BtnEliminar.Margin = new Padding(3, 4, 3, 4);
            BtnEliminar.Name = "BtnEliminar";
            BtnEliminar.Size = new Size(86, 31);
            BtnEliminar.TabIndex = 11;
            BtnEliminar.Text = "Eliminar";
            BtnEliminar.UseVisualStyleBackColor = true;
            BtnEliminar.Click += btnEliminar_Click;
            // 
            // FormLibros
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(cmbEditorial);
            Controls.Add(label5);
            Controls.Add(BtnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(cmbGenero);
            Controls.Add(label4);
            Controls.Add(cmbAutor);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtTitulo);
            Controls.Add(txtISBN);
            Controls.Add(label1);
            Controls.Add(dgvLibros);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormLibros";
            Text = "Form1";
            Load += FormLibros_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLibros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLibros;
        private Label label1;
        private TextBox txtISBN;
        private TextBox txtTitulo;
        private Label label2;
        private Label label3;
        private ComboBox cmbAutor;
        private Label label4;
        private ComboBox cmbGenero;
        private Label label5;
        private ComboBox cmbEditorial;
        private Button btnAgregar;
        private Button btnModificar;
        private Button BtnEliminar;
    }
}