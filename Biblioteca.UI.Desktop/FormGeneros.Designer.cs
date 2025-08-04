// REEMPLAZA TODO EL CONTENIDO DE FormGeneros.Designer.cs CON ESTO

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
            this.tableLayoutPanelPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.dgvGeneros = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelInputs = new System.Windows.Forms.TableLayoutPanel();
            this.labelNombre = new System.Windows.Forms.Label();
            this.txtNombreGenero = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.tableLayoutPanelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneros)).BeginInit();
            this.tableLayoutPanelInputs.SuspendLayout();
            this.flowLayoutPanelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelPrincipal
            // 
            this.tableLayoutPanelPrincipal.ColumnCount = 1;
            this.tableLayoutPanelPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPrincipal.Controls.Add(this.dgvGeneros, 0, 1);
            this.tableLayoutPanelPrincipal.Controls.Add(this.tableLayoutPanelInputs, 0, 0);
            this.tableLayoutPanelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelPrincipal.Name = "tableLayoutPanelPrincipal";
            this.tableLayoutPanelPrincipal.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelPrincipal.RowCount = 2;
            this.tableLayoutPanelPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanelPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPrincipal.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanelPrincipal.TabIndex = 0;
            // 
            // dgvGeneros
            // 
            this.dgvGeneros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGeneros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGeneros.Location = new System.Drawing.Point(13, 89);
            this.dgvGeneros.Name = "dgvGeneros";
            this.dgvGeneros.RowTemplate.Height = 25;
            this.dgvGeneros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGeneros.Size = new System.Drawing.Size(774, 348);
            this.dgvGeneros.TabIndex = 0;
            this.dgvGeneros.SelectionChanged += new System.EventHandler(this.dgvGeneros_SelectionChanged);
            // 
            // tableLayoutPanelInputs
            // 
            this.tableLayoutPanelInputs.AutoSize = true;
            this.tableLayoutPanelInputs.ColumnCount = 2;
            this.tableLayoutPanelInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInputs.Controls.Add(this.labelNombre, 0, 0);
            this.tableLayoutPanelInputs.Controls.Add(this.txtNombreGenero, 1, 0);
            this.tableLayoutPanelInputs.Controls.Add(this.flowLayoutPanelBotones, 1, 1);
            this.tableLayoutPanelInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInputs.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanelInputs.Name = "tableLayoutPanelInputs";
            this.tableLayoutPanelInputs.RowCount = 2;
            this.tableLayoutPanelInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelInputs.Size = new System.Drawing.Size(774, 70);
            this.tableLayoutPanelInputs.TabIndex = 1;
            // 
            // labelNombre
            // 
            this.labelNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(3, 7);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(54, 15);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre:";
            // 
            // txtNombreGenero
            // 
            this.txtNombreGenero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreGenero.Location = new System.Drawing.Point(63, 3);
            this.txtNombreGenero.Name = "txtNombreGenero";
            this.txtNombreGenero.Size = new System.Drawing.Size(708, 23);
            this.txtNombreGenero.TabIndex = 1;
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.Controls.Add(this.btnAgregar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnModificar);
            this.flowLayoutPanelBotones.Controls.Add(this.btnEliminar);
            this.flowLayoutPanelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelBotones.Location = new System.Drawing.Point(63, 33);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new System.Drawing.Size(708, 34);
            this.flowLayoutPanelBotones.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(3, 3);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(84, 3);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 1;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(165, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // FormGeneros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelPrincipal);
            this.Name = "FormGeneros";
            this.Text = "Gestión de Géneros";
            this.Load += new System.EventHandler(this.FormGenero_Load);
            this.tableLayoutPanelPrincipal.ResumeLayout(false);
            this.tableLayoutPanelPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneros)).EndInit();
            this.tableLayoutPanelInputs.ResumeLayout(false);
            this.tableLayoutPanelInputs.PerformLayout();
            this.flowLayoutPanelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPrincipal;
        private System.Windows.Forms.DataGridView dgvGeneros;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInputs;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox txtNombreGenero;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBotones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
    }
}