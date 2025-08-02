// EN: Biblioteca.UI.Desktop/FormPrincipal.cs

namespace Biblioteca.UI.Desktop
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        // Este es el método para el botón de Géneros (que ya te funciona)
        private void btnGestionarGeneros_Click(object sender, EventArgs e)
        {
            FormGeneros form = new FormGeneros();
            form.Show();
        }

        // ESTE ES EL MÉTODO QUE PROBABLEMENTE TE FALTA O ESTÁ VACÍO
        private void btnGestionarAutores_Click(object sender, EventArgs e)
        {
            // Esta línea es la que abre la ventana de autores
            FormAutores form = new FormAutores();
            form.Show();
        }
    }
}