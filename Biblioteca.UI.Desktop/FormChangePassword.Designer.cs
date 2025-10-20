namespace Biblioteca.UI.Desktop
{
    partial class FormChangePassword
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtActual;
        private TextBox txtNueva;
        private Label lblActual;
        private Label lblNueva;
        private Button btnAceptar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtActual = new TextBox();
            this.txtNueva = new TextBox();
            this.lblActual = new Label();
            this.lblNueva = new Label();
            this.btnAceptar = new Button();
            this.btnCancelar = new Button();
            this.SuspendLayout();
            // 
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.Location = new Point(12, 18);
            this.lblActual.Text = "Contraseña actual";
            // 
            // txtActual
            // 
            this.txtActual.Location = new Point(140, 15);
            this.txtActual.PasswordChar = '•';
            this.txtActual.Width = 220;
            // 
            // lblNueva
            // 
            this.lblNueva.AutoSize = true;
            this.lblNueva.Location = new Point(12, 52);
            this.lblNueva.Text = "Nueva contraseña";
            // 
            // txtNueva
            // 
            this.txtNueva.Location = new Point(140, 49);
            this.txtNueva.PasswordChar = '•';
            this.txtNueva.Width = 220;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new Point(140, 90);
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new Point(240, 90);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(380, 135);
            this.Controls.Add(this.lblActual);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.lblNueva);
            this.Controls.Add(this.txtNueva);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Cambiar contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
