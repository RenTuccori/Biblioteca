using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormEditoriales : Form
    {
        private readonly EditorialService _editorialService;

        public FormEditoriales()
        {
            InitializeComponent();
            
            var context = DatabaseHelper.CreateDbContext();
            var editorialRepository = new EditorialRepository(context);
            _editorialService = new EditorialService(editorialRepository);
        }

        private void FormEditoriales_Load(object sender, EventArgs e)
        {
            CargarEditoriales();
        }

        private void dgvEditoriales_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEditoriales.SelectedRows.Count > 0)
            {
                var editorialSeleccionada = (EditorialDto)dgvEditoriales.SelectedRows[0].DataBoundItem;
                txtNombreEditorial.Text = editorialSeleccionada.Nombre;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevaEditorialDto = new CrearEditorialDto 
                { 
                    Nombre = txtNombreEditorial.Text
                };
                _editorialService.Add(nuevaEditorialDto);
                MessageBox.Show("Editorial agregada con éxito.");
                CargarEditoriales();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEditoriales.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una editorial para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var editorialSeleccionada = (EditorialDto)dgvEditoriales.SelectedRows[0].DataBoundItem;
                var editorialModificadaDto = new EditorialDto 
                { 
                    Id = editorialSeleccionada.Id, 
                    Nombre = txtNombreEditorial.Text
                };
                
                if (_editorialService.Update(editorialModificadaDto))
                {
                    MessageBox.Show("Editorial modificada con éxito.");
                    CargarEditoriales();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar la editorial para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
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
                    if (_editorialService.Delete(editorialSeleccionada.Id))
                    {
                        MessageBox.Show("Editorial eliminada con éxito.");
                        CargarEditoriales();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar la editorial para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarEditoriales()
        {
            var editorialesDto = _editorialService.GetAll().ToList();

            dgvEditoriales.DataSource = null;
            dgvEditoriales.DataSource = editorialesDto;

            txtNombreEditorial.Clear();
        }
    }
}
