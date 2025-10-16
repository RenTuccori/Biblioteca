namespace Biblioteca.UI.Desktop
{
    public partial class FormPrincipal : Form
    {
        private readonly Biblioteca.API.Clients.IAuthService _auth;
        private readonly Biblioteca.API.Clients.AuthApiClient _authApiClient;

        public FormPrincipal(Biblioteca.API.Clients.IAuthService auth, Biblioteca.API.Clients.AuthApiClient authApiClient)
        {
            InitializeComponent();
            _auth = auth;
            _authApiClient = authApiClient;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await AplicarPermisosAsync();
            await ActualizarBienvenidaAsync();
        }

        private async Task ActualizarBienvenidaAsync()
        {
            var user = await _auth.GetUsernameAsync() ?? "usuario";
            lblBienvenido.Text = $"Bienvenido {user}";
        }

        private async Task AplicarPermisosAsync()
        {
            // Ejemplos de mapeo de permisos a botones
            btnGestionarAutores.Enabled = await _auth.HasPermissionAsync("autores.leer");
            btnGestionarGeneros.Enabled = await _auth.HasPermissionAsync("generos.leer");
            btnGestionarLibros.Enabled = await _auth.HasPermissionAsync("libros.leer");
            btnGestionarEditoriales.Enabled = await _auth.HasPermissionAsync("editoriales.leer");
            btnGestionarUsuarios.Enabled = await _auth.HasPermissionAsync("usuarios.leer");
            btnGestionarPrestamos.Enabled = await _auth.HasPermissionAsync("prestamos.leer");
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

        private async void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            await _auth.LogoutAsync();
            using var login = ServiceProvider.GetService<FormLogin>();
            var result = login.ShowDialog();
            if (result == DialogResult.OK)
            {
                await AplicarPermisosAsync();
                await ActualizarBienvenidaAsync();
            }
            else
            {
                Close();
            }
        }

        private async void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            using var dlg = ServiceProvider.GetService<FormChangePassword>();
            var dr = dlg.ShowDialog();
            if (dr == DialogResult.OK)
            {
                // FormChangePassword ya realizará la llamada; solo informar
                MessageBox.Show("Contraseña actualizada", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}