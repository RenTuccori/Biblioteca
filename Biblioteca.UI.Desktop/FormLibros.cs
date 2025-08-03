using Biblioteca.Domain.Model;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;
using System.Data;

namespace Biblioteca.UI.Desktop
{
    public partial class FormLibros : Form
    {
        private readonly LibroService _libroService;
        private readonly AutorService _autorService;
        private readonly GeneroService _generoService;

        public FormLibros()
        {
            InitializeComponent();
            _libroService = new LibroService();
            _autorService = new AutorService();
            _generoService = new GeneroService();
        }

        private void FormLibros_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarLibros();
        }

        private void CargarCombos()
        {
            // Cargar ComboBox de Autores
            var autores = _autorService.GetAll().Select(a => new
            {
                Id = a.Id,
                NombreCompleto = $"{a.Nombre} {a.Apellido}"
            }).ToList();
            cmbAutor.DataSource = autores;
            cmbAutor.DisplayMember = "NombreCompleto";
            cmbAutor.ValueMember = "Id";

            // Cargar ComboBox de Géneros
            var generos = _generoService.GetAll().Select(g => new
            {
                Id = g.Id,
                Nombre = g.Nombre
            }).ToList();
            cmbGenero.DataSource = generos;
            cmbGenero.DisplayMember = "Nombre";
            cmbGenero.ValueMember = "Id";
        }

        private void CargarLibros()
        {
            var librosDelDominio = _libroService.GetAll();
            var librosParaMostrar = librosDelDominio.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorNombreCompleto = $"{l.Autor.Nombre} {l.Autor.Apellido}",
                GeneroNombre = l.Genero.Nombre,
                // Llenamos los IDs para usarlos en la selección
                AutorId = l.Autor.Id,
                GeneroId = l.Genero.Id
            }).ToList();

            dgvLibros.DataSource = null;
            dgvLibros.DataSource = librosParaMostrar;
        }

        private void dgvLibros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count > 0)
            {
                var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;

                txtTitulo.Text = libroSeleccionado.Titulo;
                txtISBN.Text = libroSeleccionado.ISBN;

                // Seleccionamos el valor correcto en los ComboBox
                cmbAutor.SelectedValue = libroSeleccionado.AutorId;
                cmbGenero.SelectedValue = libroSeleccionado.GeneroId;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Creamos el DTO para la creación a partir de los controles
                var libroDto = new CrearLibroDto
                {
                    Titulo = txtTitulo.Text,
                    ISBN = txtISBN.Text,
                    AutorId = (int)cmbAutor.SelectedValue,
                    GeneroId = (int)cmbGenero.SelectedValue
                };

                _libroService.Add(libroDto);
                MessageBox.Show("Libro agregado con éxito.");
                CargarLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un libro para modificar.", "Atención");
                return;
            }

            try
            {
                var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                var libroDto = new CrearLibroDto
                {
                    Titulo = txtTitulo.Text,
                    ISBN = txtISBN.Text,
                    AutorId = (int)cmbAutor.SelectedValue,
                    GeneroId = (int)cmbGenero.SelectedValue
                };

                if (_libroService.Update(libroSeleccionado.Id, libroDto))
                {
                    MessageBox.Show("Libro modificado con éxito.");
                    CargarLibros();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el libro.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un libro para eliminar.", "Atención");
                return;
            }

            var confirmacion = MessageBox.Show("¿Está seguro?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmacion == DialogResult.Yes)
            {
                var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                if (_libroService.Delete(libroSeleccionado.Id))
                {
                    MessageBox.Show("Libro eliminado con éxito.");
                    CargarLibros();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el libro.", "Error");
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}