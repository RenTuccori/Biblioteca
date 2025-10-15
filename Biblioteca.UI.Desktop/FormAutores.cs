// EN: Biblioteca.UI.Desktop/FormAutores.cs

using Biblioteca.API.Clients;
using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormAutores : Form
    {
        private readonly AutorApiClient _autorApiClient;
        private List<AutorDto> _autores = new();

        public FormAutores(AutorApiClient autorApiClient)
        {
            InitializeComponent();
            _autorApiClient = autorApiClient;
        }

        private async void FormAutores_Load(object sender, EventArgs e)
        {
            await CargarAutores();
        }

        private void dgvAutores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count > 0)
            {
                var autorSeleccionado = (AutorDto)dgvAutores.SelectedRows[0].DataBoundItem;
                txtNombreAutor.Text = autorSeleccionado.Nombre;
                txtApellidoAutor.Text = autorSeleccionado.Apellido;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreAutor.Text) || string.IsNullOrWhiteSpace(txtApellidoAutor.Text))
                {
                    MessageBox.Show("Nombre y Apellido son requeridos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoAutorDto = new CrearAutorDto 
                { 
                    Nombre = txtNombreAutor.Text.Trim(), 
                    Apellido = txtApellidoAutor.Text.Trim() 
                };
                
                await _autorApiClient.CreateAsync(nuevoAutorDto);
                MessageBox.Show("Autor agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarAutores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar autor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un autor para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreAutor.Text) || string.IsNullOrWhiteSpace(txtApellidoAutor.Text))
                {
                    MessageBox.Show("Nombre y Apellido son requeridos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var autorSeleccionado = (AutorDto)dgvAutores.SelectedRows[0].DataBoundItem;
                var autorModificadoDto = new AutorDto 
                { 
                    Id = autorSeleccionado.Id, 
                    Nombre = txtNombreAutor.Text.Trim(), 
                    Apellido = txtApellidoAutor.Text.Trim() 
                };
                
                var resultado = await _autorApiClient.UpdateAsync(autorModificadoDto);
                if (resultado)
                {
                    MessageBox.Show("Autor modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarAutores();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el autor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar autor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un autor para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este autor?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var autorSeleccionado = (AutorDto)dgvAutores.SelectedRows[0].DataBoundItem;
                    var resultado = await _autorApiClient.DeleteAsync(autorSeleccionado.Id);
                    if (resultado)
                    {
                        MessageBox.Show("Autor eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarAutores();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el autor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar autor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    await CargarAutores();
                    LimpiarCampos();
                }
                else
                {
                    _autores = (await _autorApiClient.GetByCriteriaAsync(txtBuscar.Text.Trim())).ToList();
                    dgvAutores.DataSource = null;
                    dgvAutores.DataSource = _autores;
                    dgvAutores.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar autores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarAutores()
        {
            try
            {
                _autores = (await _autorApiClient.GetAllAsync()).ToList();
                dgvAutores.DataSource = null;
                dgvAutores.DataSource = _autores;
                
                if (dgvAutores.Columns.Count > 0)
                {
                    dgvAutores.Columns["Id"].HeaderText = "ID";
                    dgvAutores.Columns["Nombre"].HeaderText = "Nombre";
                    dgvAutores.Columns["Apellido"].HeaderText = "Apellido";
                }
                
                // Limpiar la selección para que no se cargue automáticamente el primer elemento
                dgvAutores.ClearSelection();

                // Dejar campos vacíos al iniciar/cargar
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar autores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreAutor.Clear();
            txtApellidoAutor.Clear();
            txtBuscar.Clear();
            dgvAutores.ClearSelection();
        }
    }
}