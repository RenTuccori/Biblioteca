namespace Biblioteca.UI.Desktop
{
    partial class FormPrestamos
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
            dgvPrestamos = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbLibro = new ComboBox();
            cmbSocio = new ComboBox();
            dtpFechaPrestamo = new DateTimePicker();
            dtpFechaDevolucionPrevista = new DateTimePicker();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnDevolver = new Button();
            groupBox1 = new GroupBox();
            btnVerTodos = new Button();
            btnVerVencidos = new Button();
            btnVerActivos = new Button();
            lblEstado = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPrestamos).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPrestamos
            // 
            dgvPrestamos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrestamos.Location = new Point(30, 280);
            dgvPrestamos.Name = "dgvPrestamos";
            dgvPrestamos.RowHeadersWidth = 51;
            dgvPrestamos.Size = new Size(940, 280);
            dgvPrestamos.TabIndex = 0;
            dgvPrestamos.SelectionChanged += dgvPrestamos_SelectionChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 30);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 1;
            label1.Text = "Libro";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 75);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 2;
            label2.Text = "Socio";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 120);
            label3.Name = "label3";
            label3.Size = new Size(118, 20);
            label3.TabIndex = 3;
            label3.Text = "Fecha Préstamo";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 165);
            label4.Name = "label4";
            label4.Size = new Size(182, 20);
            label4.TabIndex = 4;
            label4.Text = "Fecha Devolución Prevista";
            // 
            // cmbLibro
            // 
            cmbLibro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLibro.FormattingEnabled = true;
            cmbLibro.Location = new Point(220, 27);
            cmbLibro.Name = "cmbLibro";
            cmbLibro.Size = new Size(350, 28);
            cmbLibro.TabIndex = 5;
            // 
            // cmbSocio
            // 
            cmbSocio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSocio.FormattingEnabled = true;
            cmbSocio.Location = new Point(220, 72);
            cmbSocio.Name = "cmbSocio";
            cmbSocio.Size = new Size(350, 28);
            cmbSocio.TabIndex = 6;
            // 
            // dtpFechaPrestamo
            // 
            dtpFechaPrestamo.Format = DateTimePickerFormat.Short;
            dtpFechaPrestamo.Location = new Point(220, 117);
            dtpFechaPrestamo.Name = "dtpFechaPrestamo";
            dtpFechaPrestamo.Size = new Size(200, 27);
            dtpFechaPrestamo.TabIndex = 7;
            // 
            // dtpFechaDevolucionPrevista
            // 
            dtpFechaDevolucionPrevista.Format = DateTimePickerFormat.Short;
            dtpFechaDevolucionPrevista.Location = new Point(220, 162);
            dtpFechaDevolucionPrevista.Name = "dtpFechaDevolucionPrevista";
            dtpFechaDevolucionPrevista.Size = new Size(200, 27);
            dtpFechaDevolucionPrevista.TabIndex = 8;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(590, 25);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(110, 35);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(590, 70);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(110, 35);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(590, 115);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 35);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnDevolver
            // 
            btnDevolver.BackColor = System.Drawing.Color.LightGreen;
            btnDevolver.Enabled = false;
            btnDevolver.Location = new Point(590, 160);
            btnDevolver.Name = "btnDevolver";
            btnDevolver.Size = new Size(110, 35);
            btnDevolver.TabIndex = 12;
            btnDevolver.Text = "Devolver";
            btnDevolver.UseVisualStyleBackColor = false;
            btnDevolver.Click += btnDevolver_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnVerTodos);
            groupBox1.Controls.Add(btnVerVencidos);
            groupBox1.Controls.Add(btnVerActivos);
            groupBox1.Location = new Point(720, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(250, 180);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtros";
            // 
            // btnVerTodos
            // 
            btnVerTodos.Location = new Point(20, 130);
            btnVerTodos.Name = "btnVerTodos";
            btnVerTodos.Size = new Size(210, 35);
            btnVerTodos.TabIndex = 2;
            btnVerTodos.Text = "Ver Todos";
            btnVerTodos.UseVisualStyleBackColor = true;
            btnVerTodos.Click += btnVerTodos_Click;
            // 
            // btnVerVencidos
            // 
            btnVerVencidos.Location = new Point(20, 80);
            btnVerVencidos.Name = "btnVerVencidos";
            btnVerVencidos.Size = new Size(210, 35);
            btnVerVencidos.TabIndex = 1;
            btnVerVencidos.Text = "Ver Vencidos";
            btnVerVencidos.UseVisualStyleBackColor = true;
            btnVerVencidos.Click += btnVerVencidos_Click;
            // 
            // btnVerActivos
            // 
            btnVerActivos.Location = new Point(20, 30);
            btnVerActivos.Name = "btnVerActivos";
            btnVerActivos.Size = new Size(210, 35);
            btnVerActivos.TabIndex = 0;
            btnVerActivos.Text = "Ver Activos";
            btnVerActivos.UseVisualStyleBackColor = true;
            btnVerActivos.Click += btnVerActivos_Click;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblEstado.Location = new Point(30, 250);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(176, 20);
            lblEstado.TabIndex = 14;
            lblEstado.Text = "Mostrando todos los préstamos";
            // 
            // FormPrestamos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(lblEstado);
            Controls.Add(groupBox1);
            Controls.Add(btnDevolver);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dtpFechaDevolucionPrevista);
            Controls.Add(dtpFechaPrestamo);
            Controls.Add(cmbSocio);
            Controls.Add(cmbLibro);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPrestamos);
            Name = "FormPrestamos";
            Text = "Gestión de Préstamos";
            Load += FormPrestamos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPrestamos).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPrestamos;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox cmbLibro;
        private ComboBox cmbSocio;
        private DateTimePicker dtpFechaPrestamo;
        private DateTimePicker dtpFechaDevolucionPrevista;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnDevolver;
        private GroupBox groupBox1;
        private Button btnVerTodos;
        private Button btnVerVencidos;
        private Button btnVerActivos;
        private Label lblEstado;
    }
}
