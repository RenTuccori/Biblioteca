namespace Biblioteca.UI.Desktop
{
    partial class FormAutores
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
            dgvAutores = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            txtNombreAutor = new TextBox();
            txtApellidoAutor = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvAutores).BeginInit();
            SuspendLayout();
            // 
            // dgvAutores
            // 
            dgvAutores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAutores.Location = new Point(28, 55);
            dgvAutores.Name = "dgvAutores";
            dgvAutores.RowHeadersWidth = 51;
            dgvAutores.Size = new Size(300, 188);
            dgvAutores.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(351, 59);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 1;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(351, 118);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 2;
            label2.Text = "Apellido";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(28, 249);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 3;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(134, 249);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 4;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(234, 249);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // txtNombreAutor
            // 
            txtNombreAutor.Location = new Point(351, 82);
            txtNombreAutor.Name = "txtNombreAutor";
            txtNombreAutor.Size = new Size(125, 27);
            txtNombreAutor.TabIndex = 6;
            // 
            // txtApellidoAutor
            // 
            txtApellidoAutor.Location = new Point(351, 141);
            txtApellidoAutor.Name = "txtApellidoAutor";
            txtApellidoAutor.Size = new Size(125, 27);
            txtApellidoAutor.TabIndex = 7;
            // 
            // FormAutores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtApellidoAutor);
            Controls.Add(txtNombreAutor);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvAutores);
            Name = "FormAutores";
            Text = "FormAutores";
            ((System.ComponentModel.ISupportInitialize)dgvAutores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAutores;
        private Label label1;
        private Label label2;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private TextBox txtNombreAutor;
        private TextBox txtApellidoAutor;
    }
}