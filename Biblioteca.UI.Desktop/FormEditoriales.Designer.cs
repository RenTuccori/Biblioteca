namespace Biblioteca.UI.Desktop
{
    partial class FormEditoriales
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
            dgvEditoriales = new DataGridView();
            label1 = new Label();
            txtNombreEditorial = new TextBox();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEditoriales).BeginInit();
            SuspendLayout();
            // 
            // dgvEditoriales
            // 
            dgvEditoriales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEditoriales.Location = new Point(50, 150);
            dgvEditoriales.Name = "dgvEditoriales";
            dgvEditoriales.RowHeadersWidth = 51;
            dgvEditoriales.Size = new Size(500, 300);
            dgvEditoriales.TabIndex = 0;
            dgvEditoriales.SelectionChanged += dgvEditoriales_SelectionChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 30);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 1;
            label1.Text = "Nombre";
            // 
            // txtNombreEditorial
            // 
            txtNombreEditorial.Location = new Point(150, 27);
            txtNombreEditorial.Name = "txtNombreEditorial";
            txtNombreEditorial.Size = new Size(300, 27);
            txtNombreEditorial.TabIndex = 2;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(470, 25);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(90, 30);
            btnAgregar.TabIndex = 3;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(470, 65);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(90, 30);
            btnModificar.TabIndex = 4;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(470, 105);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(90, 30);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // FormEditoriales
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 500);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(txtNombreEditorial);
            Controls.Add(label1);
            Controls.Add(dgvEditoriales);
            Name = "FormEditoriales";
            Text = "Gestión de Editoriales";
            Load += FormEditoriales_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEditoriales).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvEditoriales;
        private Label label1;
        private TextBox txtNombreEditorial;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
    }
}
