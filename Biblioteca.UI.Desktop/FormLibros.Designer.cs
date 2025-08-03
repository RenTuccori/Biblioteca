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
            btnAgregar = new Button();
            btnModificar = new Button();
            BtnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLibros).BeginInit();
            SuspendLayout();
            // 
            // dgvLibros
            // 
            dgvLibros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLibros.Location = new Point(104, 141);
            dgvLibros.Name = "dgvLibros";
            dgvLibros.Size = new Size(322, 177);
            dgvLibros.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(104, 12);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 1;
            label1.Text = "Título";
            // 
            // txtISBN
            // 
            txtISBN.Location = new Point(180, 38);
            txtISBN.Name = "txtISBN";
            txtISBN.Size = new Size(165, 23);
            txtISBN.TabIndex = 2;
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(180, 9);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(165, 23);
            txtTitulo.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(104, 41);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 4;
            label2.Text = "ISBN";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(104, 70);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 5;
            label3.Text = "Autor";
            // 
            // cmbAutor
            // 
            cmbAutor.FormattingEnabled = true;
            cmbAutor.Location = new Point(180, 67);
            cmbAutor.Name = "cmbAutor";
            cmbAutor.Size = new Size(165, 23);
            cmbAutor.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(104, 99);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 7;
            label4.Text = "Género";
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(180, 96);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(165, 23);
            cmbGenero.TabIndex = 8;
            cmbGenero.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(351, 8);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(75, 23);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(351, 40);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // BtnEliminar
            // 
            BtnEliminar.Location = new Point(351, 69);
            BtnEliminar.Name = "BtnEliminar";
            BtnEliminar.Size = new Size(75, 23);
            BtnEliminar.TabIndex = 11;
            BtnEliminar.Text = "Eliminar";
            BtnEliminar.UseVisualStyleBackColor = true;
            BtnEliminar.Click += btnEliminar_Click;
            // 
            // FormLibros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
        private Button btnAgregar;
        private Button btnModificar;
        private Button BtnEliminar;
    }
}