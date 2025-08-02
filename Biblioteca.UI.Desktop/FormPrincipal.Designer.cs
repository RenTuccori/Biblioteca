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
            btnGestionarGeneros = new Button();
            btnGestionarAutores = new Button();
            SuspendLayout();
            // 
            // btnGestionarGeneros
            // 
            btnGestionarGeneros.Location = new Point(95, 47);
            btnGestionarGeneros.Name = "btnGestionarGeneros";
            btnGestionarGeneros.Size = new Size(160, 29);
            btnGestionarGeneros.TabIndex = 0;
            btnGestionarGeneros.Text = "Gestionar Géneros";
            btnGestionarGeneros.UseVisualStyleBackColor = true;
            btnGestionarGeneros.Click += btnGestionarGeneros_Click;
            // 
            // btnGestionarAutores
            // 
            btnGestionarAutores.Location = new Point(95, 82);
            btnGestionarAutores.Name = "btnGestionarAutores";
            btnGestionarAutores.Size = new Size(160, 29);
            btnGestionarAutores.TabIndex = 1;
            btnGestionarAutores.Text = "Gestionar Autores";
            btnGestionarAutores.UseVisualStyleBackColor = true;
            btnGestionarAutores.Click += btnGestionarAutores_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGestionarAutores);
            Controls.Add(btnGestionarGeneros);
            Name = "FormPrincipal";
            Text = "FormPrincipal";
            ResumeLayout(false);
        }

        #endregion

        private Button btnGestionarGeneros;
        private Button btnGestionarAutores;
    }
}