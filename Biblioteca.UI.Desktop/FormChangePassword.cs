using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormChangePassword : Form
    {
        private readonly IAuthService _auth;
        private readonly AuthApiClient _authApiClient;

        public FormChangePassword(IAuthService auth, AuthApiClient authApiClient)
        {
            InitializeComponent();
            _auth = auth;
            _authApiClient = authApiClient;
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtActual.Text) || string.IsNullOrWhiteSpace(txtNueva.Text))
            {
                MessageBox.Show("Complete todos los campos", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var token = await _auth.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Sesión no válida", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var ok = await _authApiClient.ChangePasswordAsync(new ChangePasswordDto
            {
                CurrentPassword = txtActual.Text,
                NewPassword = txtNueva.Text
            }, token);
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo cambiar la contraseña", "Cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
