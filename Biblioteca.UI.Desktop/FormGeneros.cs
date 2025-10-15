using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormGeneros : Form
    {
        private readonly GeneroApiClient _generoApiClient;

        public FormGeneros(GeneroApiClient generoApiClient)
        {
            InitializeComponent();
            _generoApiClient = generoApiClient;
        }

        private async void FormGenero_Load(object sender, EventArgs e)
        {
            await CargarGeneros();
        }

        private void dgvGeneros_SelectionChanged(object sender, EventArgs e)
        {
            // Verificamos si hay alguna fila seleccionada.
            if (dgvGeneros.SelectedRows.Count > 0)
            {
                // Obtenemos el objeto DTO de la fila seleccionada.
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;

                // Ponemos el nombre del género en el TextBox para que el usuario pueda editarlo.
                txtNombreGenero.Text = generoSeleccionado.Nombre;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreGenero.Text))
                {
                    MessageBox.Show("El nombre del género es requerido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoGeneroDto = new CrearGeneroDto { Nombre = txtNombreGenero.Text.Trim() };
                await _generoApiClient.CreateAsync(nuevoGeneroDto);
                MessageBox.Show("Género agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarGeneros();
                txtNombreGenero.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar género: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un género para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreGenero.Text))
                {
                    MessageBox.Show("El nombre del género es requerido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Obtenemos el DTO del género seleccionado.
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;

                // 2. Creamos un DTO con el ID original y el nuevo nombre del TextBox.
                var generoModificadoDto = new GeneroDto 
                { 
                    Id = generoSeleccionado.Id, 
                    Nombre = txtNombreGenero.Text.Trim()
                };

                // 3. Llamamos al cliente API de actualización.
                bool resultado = await _generoApiClient.UpdateAsync(generoModificadoDto);

                if (resultado)
                {
                    MessageBox.Show("Género modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarGeneros(); // Refrescamos la grilla.
                    txtNombreGenero.Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el género.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar género: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un género para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pedimos confirmación antes de una acción destructiva.
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este género?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;
                    bool resultado = await _generoApiClient.DeleteAsync(generoSeleccionado.Id);

                    if (resultado)
                    {
                        MessageBox.Show("Género eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarGeneros();
                        txtNombreGenero.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el género.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar género: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                
                // Limpiar la selección para que no se cargue automáticamente el primer elemento
                dgvGeneros.ClearSelection();

                // Dejar campo de entrada vacío al iniciar
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar géneros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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