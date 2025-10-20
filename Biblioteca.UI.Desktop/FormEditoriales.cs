using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormEditoriales : Form
    {
        private readonly EditorialApiClient _editorialApiClient;
        private readonly IAuthService _auth;

        public FormEditoriales(EditorialApiClient editorialApiClient, IAuthService auth)
        {
            InitializeComponent();
            _editorialApiClient = editorialApiClient;
            _auth = auth;
        }

        private async void FormEditoriales_Load(object sender, EventArgs e)
        {
            await AplicarPermisosAsync();
            await CargarEditoriales();
        }

        private async Task AplicarPermisosAsync()
        {
            var puedeLeer = await _auth.HasPermissionAsync("editoriales.leer");
            var puedeAgregar = await _auth.HasPermissionAsync("editoriales.agregar");
            var puedeActualizar = await _auth.HasPermissionAsync("editoriales.actualizar");
            var puedeEliminar = await _auth.HasPermissionAsync("editoriales.eliminar");

            if (!puedeLeer)
            {
                MessageBox.Show("No tiene permiso para ver editoriales.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            btnAgregar.Enabled = puedeAgregar;
            btnModificar.Enabled = puedeActualizar;
            btnEliminar.Enabled = puedeEliminar;
        }

        private void dgvEditoriales_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEditoriales.SelectedRows.Count > 0)
            {
                var editorialSeleccionada = (EditorialDto)dgvEditoriales.SelectedRows[0].DataBoundItem;
                txtNombreEditorial.Text = editorialSeleccionada.Nombre;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreEditorial.Text))
                {
                    MessageBox.Show("El nombre de la editorial es requerido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevaEditorialDto = new CrearEditorialDto { Nombre = txtNombreEditorial.Text.Trim() };
                await _editorialApiClient.CreateAsync(nuevaEditorialDto);
                MessageBox.Show("Editorial agregada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarEditoriales();
                txtNombreEditorial.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar editorial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEditoriales.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una editorial para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreEditorial.Text))
                {
                    MessageBox.Show("El nombre de la editorial es requerido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var editorialSeleccionada = (EditorialDto)dgvEditoriales.SelectedRows[0].DataBoundItem;
                var editorialModificadaDto = new EditorialDto { Id = editorialSeleccionada.Id, Nombre = txtNombreEditorial.Text.Trim() };
                bool resultado = await _editorialApiClient.UpdateAsync(editorialModificadaDto);
                if (resultado)
                {
                    MessageBox.Show("Editorial modificada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarEditoriales();
                    txtNombreEditorial.Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la editorial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar editorial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEditoriales.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una editorial para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar esta editorial?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var editorialSeleccionada = (EditorialDto)dgvEditoriales.SelectedRows[0].DataBoundItem;
                    bool resultado = await _editorialApiClient.DeleteAsync(editorialSeleccionada.Id);
                    if (resultado)
                    {
                        MessageBox.Show("Editorial eliminada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarEditoriales();
                        txtNombreEditorial.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la editorial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar editorial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task CargarEditoriales()
        {
            try
            {
                var editorialesDto = (await _editorialApiClient.GetAllAsync()).ToList();

                dgvEditoriales.DataSource = null;
                dgvEditoriales.DataSource = editorialesDto;
                dgvEditoriales.ClearSelection();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar editoriales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreEditorial.Clear();
            dgvEditoriales.ClearSelection();
        }
    }
}
