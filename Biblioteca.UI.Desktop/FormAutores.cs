// EN: Biblioteca.UI.Desktop/FormAutores.cs

using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormAutores : Form
    {
        private readonly AutorService _autorService;

        public FormAutores()
        {
            InitializeComponent();
            
            // Configurar servicios con dependencias
            var context = DatabaseHelper.CreateDbContext();
            var autorRepository = new AutorRepository(context);
            _autorService = new AutorService(autorRepository);
        }

        private void FormAutores_Load(object sender, EventArgs e)
        {
            CargarAutores();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevoAutorDto = new CrearAutorDto 
                { 
                    Nombre = txtNombreAutor.Text, 
                    Apellido = txtApellidoAutor.Text 
                };
                _autorService.Add(nuevoAutorDto);
                MessageBox.Show("Autor agregado con éxito.");
                CargarAutores();
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
            if (dgvAutores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un autor para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var autorSeleccionado = (AutorDto)dgvAutores.SelectedRows[0].DataBoundItem;
                var autorModificadoDto = new AutorDto 
                { 
                    Id = autorSeleccionado.Id, 
                    Nombre = txtNombreAutor.Text, 
                    Apellido = txtApellidoAutor.Text 
                };
                
                if (_autorService.Update(autorModificadoDto))
                {
                    MessageBox.Show("Autor modificado con éxito.");
                    CargarAutores();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el autor para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (_autorService.Delete(autorSeleccionado.Id))
                    {
                        MessageBox.Show("Autor eliminado con éxito.");
                        CargarAutores();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el autor para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarAutores()
        {
            var autoresDto = _autorService.GetAll().ToList();

            dgvAutores.DataSource = null;
            dgvAutores.DataSource = autoresDto;

            txtNombreAutor.Clear();
            txtApellidoAutor.Clear();
        }
    }
}