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
            var form = ServiceProvider.GetService<FormGeneros>();
            form.Show();
        }

        private void btnGestionarAutores_Click(object sender, EventArgs e)
        {
            var form = ServiceProvider.GetService<FormAutores>();
            form.Show();
        }

        private void btnGestionarLibros_Click(object sender, EventArgs e)
        {
            var form = ServiceProvider.GetService<FormLibros>();
            form.Show();
        }

        private void btnGestionarEditoriales_Click(object sender, EventArgs e)
        {
            var form = ServiceProvider.GetService<FormEditoriales>();
            form.Show();
        }

        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
            var form = ServiceProvider.GetService<FormUsuarios>();
            form.Show();
        }

        private void btnGestionarPrestamos_Click(object sender, EventArgs e)
        {
            var form = ServiceProvider.GetService<FormPrestamos>();
            form.Show();
        }
    }
}