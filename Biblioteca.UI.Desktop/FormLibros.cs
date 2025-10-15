using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormLibros : Form
    {
        private readonly LibroApiClient _libroApiClient;
        private readonly AutorApiClient _autorApiClient;
        private readonly GeneroApiClient _generoApiClient;
        private readonly EditorialApiClient _editorialApiClient;
        private List<LibroDto> _libros = new();
        private List<AutorDto> _autores = new();
        private List<GeneroDto> _generos = new();
        private List<EditorialDto> _editoriales = new();

        public FormLibros(LibroApiClient libroApiClient, AutorApiClient autorApiClient, GeneroApiClient generoApiClient, EditorialApiClient editorialApiClient)
        {
            InitializeComponent();
            _libroApiClient = libroApiClient;
            _autorApiClient = autorApiClient;
            _generoApiClient = generoApiClient;
            _editorialApiClient = editorialApiClient;
        }

        private async void FormLibros_Load(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void dgvLibros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count > 0)
            {
                var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                txtTitulo.Text = libroSeleccionado.Titulo;
                txtISBN.Text = libroSeleccionado.ISBN;
                cmbAutor.SelectedValue = libroSeleccionado.AutorId;
                cmbGenero.SelectedValue = libroSeleccionado.GeneroId;
                cmbEditorial.SelectedValue = libroSeleccionado.EditorialId;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitulo.Text) || 
                    string.IsNullOrWhiteSpace(txtISBN.Text) ||
                    cmbAutor.SelectedValue == null || 
                    cmbGenero.SelectedValue == null || 
                    cmbEditorial.SelectedValue == null)
                {
                    MessageBox.Show("Todos los campos son requeridos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoLibroDto = new CrearLibroDto
                {
                    Titulo = txtTitulo.Text.Trim(),
                    ISBN = txtISBN.Text.Trim(),
                    AutorId = (int)cmbAutor.SelectedValue,
                    GeneroId = (int)cmbGenero.SelectedValue,
                    EditorialId = (int)cmbEditorial.SelectedValue,
                    Estado = "disponible"
                };

                await _libroApiClient.CreateAsync(nuevoLibroDto);
                MessageBox.Show("Libro agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarLibros();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un libro para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtTitulo.Text) || 
                    string.IsNullOrWhiteSpace(txtISBN.Text) ||
                    cmbAutor.SelectedValue == null || 
                    cmbGenero.SelectedValue == null || 
                    cmbEditorial.SelectedValue == null)
                {
                    MessageBox.Show("Todos los campos son requeridos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                var libroModificadoDto = new LibroDto
                {
                    Id = libroSeleccionado.Id,
                    Titulo = txtTitulo.Text.Trim(),
                    ISBN = txtISBN.Text.Trim(),
                    AutorId = (int)cmbAutor.SelectedValue,
                    GeneroId = (int)cmbGenero.SelectedValue,
                    EditorialId = (int)cmbEditorial.SelectedValue,
                    Estado = libroSeleccionado.Estado
                };

                var resultado = await _libroApiClient.UpdateAsync(libroModificadoDto);
                if (resultado)
                {
                    MessageBox.Show("Libro modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarLibros();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el libro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un libro para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este libro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                    var resultado = await _libroApiClient.DeleteAsync(libroSeleccionado.Id);
                    if (resultado)
                    {
                        MessageBox.Show("Libro eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarLibros();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el libro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task CargarDatos()
        {
            try
            {
                await CargarAutores();
                await CargarGeneros();
                await CargarEditoriales();
                await CargarLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarLibros()
        {
            try
            {
                _libros = (await _libroApiClient.GetAllAsync()).ToList();
                dgvLibros.DataSource = null;
                dgvLibros.DataSource = _libros;
                
                if (dgvLibros.Columns.Count > 0)
                {
                    dgvLibros.Columns["Id"].HeaderText = "ID";
                    dgvLibros.Columns["Titulo"].HeaderText = "Título";
                    dgvLibros.Columns["ISBN"].HeaderText = "ISBN";
                    dgvLibros.Columns["AutorNombreCompleto"].HeaderText = "Autor";
                    dgvLibros.Columns["GeneroNombre"].HeaderText = "Género";
                    dgvLibros.Columns["EditorialNombre"].HeaderText = "Editorial";
                    dgvLibros.Columns["Estado"].HeaderText = "Estado";
                    
                    // Ocultar IDs de relaciones
                    dgvLibros.Columns["AutorId"].Visible = false;
                    dgvLibros.Columns["GeneroId"].Visible = false;
                    dgvLibros.Columns["EditorialId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar libros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarAutores()
        {
            try
            {
                _autores = (await _autorApiClient.GetAllAsync()).ToList();
                var autoresCombo = _autores.Select(a => new
                {
                    Id = a.Id,
                    NombreCompleto = $"{a.Nombre} {a.Apellido}"
                }).ToList();
                
                cmbAutor.DataSource = autoresCombo;
                cmbAutor.DisplayMember = "NombreCompleto";
                cmbAutor.ValueMember = "Id";
                cmbAutor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar autores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarGeneros()
        {
            try
            {
                _generos = (await _generoApiClient.GetAllAsync()).ToList();
                cmbGenero.DataSource = _generos;
                cmbGenero.DisplayMember = "Nombre";
                cmbGenero.ValueMember = "Id";
                cmbGenero.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar géneros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarEditoriales()
        {
            try
            {
                _editoriales = (await _editorialApiClient.GetAllAsync()).ToList();
                cmbEditorial.DataSource = _editoriales;
                cmbEditorial.DisplayMember = "Nombre";
                cmbEditorial.ValueMember = "Id";
                cmbEditorial.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar editoriales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtTitulo.Clear();
            txtISBN.Clear();
            cmbAutor.SelectedIndex = -1;
            cmbGenero.SelectedIndex = -1;
            cmbEditorial.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Método existente sin usar, lo mantengo para no romper el designer
        }
    }
}