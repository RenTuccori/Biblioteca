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
            btnAgregar = new Button();
            txtNombreGenero = new TextBox();
            dgvGeneros = new DataGridView();
            btnModificar = new Button();
            btnEliminar = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvGeneros).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(209, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(101, 31);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtNombreGenero
            // 
            txtNombreGenero.Location = new Point(3, 3);
            txtNombreGenero.Name = "txtNombreGenero";
            txtNombreGenero.Size = new Size(200, 27);
            txtNombreGenero.TabIndex = 1;
            txtNombreGenero.TextChanged += textBox1_TextChanged;
            // 
            // dgvGeneros
            // 
            dgvGeneros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGeneros.Location = new Point(3, 40);
            dgvGeneros.Name = "dgvGeneros";
            dgvGeneros.RowHeadersWidth = 51;
            dgvGeneros.Size = new Size(521, 188);
            dgvGeneros.TabIndex = 2;
            dgvGeneros.SelectionChanged += dgvGeneros_SelectionChanged;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(423, 3);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(101, 31);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(316, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(101, 31);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(txtNombreGenero);
            flowLayoutPanel1.Controls.Add(btnAgregar);
            flowLayoutPanel1.Controls.Add(btnEliminar);
            flowLayoutPanel1.Controls.Add(btnModificar);
            flowLayoutPanel1.Controls.Add(dgvGeneros);
            flowLayoutPanel1.Location = new Point(32, 100);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(529, 238);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 450);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGeneros).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnAgregar;
        private TextBox txtNombreGenero;
        private DataGridView dgvGeneros;
        private Button btnModificar;
        private Button btnEliminar;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
