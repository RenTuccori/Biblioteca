using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormGeneros : Form
    {
        private readonly GeneroApiClient _generoApiClient;
        private readonly IAuthService _auth;

        public FormGeneros(GeneroApiClient generoApiClient, IAuthService auth)
        {
            InitializeComponent();
            _generoApiClient = generoApiClient;
            _auth = auth;
        }

        private async void FormGenero_Load(object sender, EventArgs e)
        {
            await AplicarPermisosAsync();
            await CargarGeneros();
        }

        private async Task AplicarPermisosAsync()
        {
            var puedeLeer = await _auth.HasPermissionAsync("generos.leer");
            var puedeAgregar = await _auth.HasPermissionAsync("generos.agregar");
            var puedeActualizar = await _auth.HasPermissionAsync("generos.actualizar");
            var puedeEliminar = await _auth.HasPermissionAsync("generos.eliminar");

            // Si no puede leer, cerrar
            if (!puedeLeer)
            {
                MessageBox.Show("No tiene permiso para ver g�neros.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            btnAgregar.Enabled = puedeAgregar;
            btnModificar.Enabled = puedeActualizar;
            btnEliminar.Enabled = puedeEliminar;
        }

        private void dgvGeneros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count > 0)
            {
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;
                txtNombreGenero.Text = generoSeleccionado.Nombre;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreGenero.Text))
                {
                    MessageBox.Show("El nombre del g�nero es requerido.", "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoGeneroDto = new CrearGeneroDto { Nombre = txtNombreGenero.Text.Trim() };
                await _generoApiClient.CreateAsync(nuevoGeneroDto);
                MessageBox.Show("G�nero agregado con �xito.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarGeneros();
                txtNombreGenero.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar g�nero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un g�nero para modificar.", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreGenero.Text))
                {
                    MessageBox.Show("El nombre del g�nero es requerido.", "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;
                var generoModificadoDto = new GeneroDto
                {
                    Id = generoSeleccionado.Id,
                    Nombre = txtNombreGenero.Text.Trim()
                };

                bool resultado = await _generoApiClient.UpdateAsync(generoModificadoDto);

                if (resultado)
                {
                    MessageBox.Show("G�nero modificado con �xito.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarGeneros();
                    txtNombreGenero.Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el g�nero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar g�nero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un g�nero para eliminar.", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("�Est� seguro de que desea eliminar este g�nero?", "Confirmar eliminaci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;
                    bool resultado = await _generoApiClient.DeleteAsync(generoSeleccionado.Id);

                    if (resultado)
                    {
                        MessageBox.Show("G�nero eliminado con �xito.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarGeneros();
                        txtNombreGenero.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el g�nero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar g�nero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task CargarGeneros()
        {
            try
            {
                var generosDto = (await _generoApiClient.GetAllAsync()).ToList();

                dgvGeneros.DataSource = null;
                dgvGeneros.DataSource = generosDto;
                dgvGeneros.ClearSelection();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar g�neros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreGenero.Clear();
            dgvGeneros.ClearSelection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}