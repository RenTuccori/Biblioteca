// EN: Biblioteca.UI.Desktop/FormAutores.cs

using Biblioteca.Domain.Services;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormAutores : Form
    {
        private readonly AutorService _autorService;

        public FormAutores()
        {
            InitializeComponent();
            _autorService = new AutorService(); // Usamos el servicio de Autores
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
                var nuevoAutor = new Autor(0, txtNombreAutor.Text, txtApellidoAutor.Text);
                _autorService.Add(nuevoAutor);
                MessageBox.Show("Autor agregado con éxito.");
                CargarAutores();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var autorModificado = new Autor(autorSeleccionado.Id, txtNombreAutor.Text, txtApellidoAutor.Text);
                if (_autorService.Update(autorModificado))
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
        }

        private void CargarAutores()
        {
            List<Autor> autoresDelDominio = _autorService.GetAll();
            List<AutorDto> autoresParaMostrar = autoresDelDominio.Select(a => new AutorDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido
            }).ToList();

            dgvAutores.DataSource = null;
            dgvAutores.DataSource = autoresParaMostrar;

            txtNombreAutor.Clear();
            txtApellidoAutor.Clear();
        }
    }
}