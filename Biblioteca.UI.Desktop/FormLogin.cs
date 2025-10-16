using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormLogin : Form
    {
        private readonly IAuthService _auth;

        public FormLogin(IAuthService auth)
        {
            InitializeComponent();
            _auth = auth;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;
                var ok = await _auth.LoginAsync(new LoginRequest
                {
                    Username = txtUsuario.Text.Trim(),
                    Password = txtPassword.Text
                });
                if (!ok)
                {
                    MessageBox.Show("Usuario o contraseña inválidos", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnLogin.Enabled = true;
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de autenticación: {ex.Message}", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
