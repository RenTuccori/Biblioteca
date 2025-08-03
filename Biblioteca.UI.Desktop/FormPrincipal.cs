
namespace Biblioteca.UI.Desktop
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnGestionarGeneros_Click(object sender, EventArgs e)
        {
            FormGeneros form = new FormGeneros();
            form.Show();
        }

        private void btnGestionarAutores_Click(object sender, EventArgs e)
        {
            FormAutores form = new FormAutores();
            form.Show();
        }

        private void btnGestionarLibros_Click(object sender, EventArgs e)
        {
            FormLibros form = new FormLibros();
            form.Show();
        }
    }
}