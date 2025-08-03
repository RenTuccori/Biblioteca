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
            btnGestionarLibros = new Button();
            SuspendLayout();
            // 
            // btnGestionarGeneros
            // 
            btnGestionarGeneros.Location = new Point(83, 35);
            btnGestionarGeneros.Margin = new Padding(3, 2, 3, 2);
            btnGestionarGeneros.Name = "btnGestionarGeneros";
            btnGestionarGeneros.Size = new Size(140, 22);
            btnGestionarGeneros.TabIndex = 0;
            btnGestionarGeneros.Text = "Gestionar Géneros";
            btnGestionarGeneros.UseVisualStyleBackColor = true;
            btnGestionarGeneros.Click += btnGestionarGeneros_Click;
            // 
            // btnGestionarAutores
            // 
            btnGestionarAutores.Location = new Point(83, 62);
            btnGestionarAutores.Margin = new Padding(3, 2, 3, 2);
            btnGestionarAutores.Name = "btnGestionarAutores";
            btnGestionarAutores.Size = new Size(140, 22);
            btnGestionarAutores.TabIndex = 1;
            btnGestionarAutores.Text = "Gestionar Autores";
            btnGestionarAutores.UseVisualStyleBackColor = true;
            btnGestionarAutores.Click += btnGestionarAutores_Click;
            // 
            // btnGestionarLibros
            // 
            btnGestionarLibros.Location = new Point(83, 89);
            btnGestionarLibros.Name = "btnGestionarLibros";
            btnGestionarLibros.Size = new Size(140, 23);
            btnGestionarLibros.TabIndex = 2;
            btnGestionarLibros.Text = "Gestionar Libros";
            btnGestionarLibros.UseVisualStyleBackColor = true;
            btnGestionarLibros.Click += btnGestionarLibros_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(btnGestionarLibros);
            Controls.Add(btnGestionarAutores);
            Controls.Add(btnGestionarGeneros);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormPrincipal";
            Text = "FormPrincipal";
            ResumeLayout(false);
        }

        #endregion

        private Button btnGestionarGeneros;
        private Button btnGestionarAutores;
        private Button btnGestionarLibros;
    }
}