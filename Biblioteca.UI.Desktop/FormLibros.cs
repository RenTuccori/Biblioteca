using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

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
            
            // Configurar servicios con dependencias
            var context = DatabaseHelper.CreateDbContext();
            var libroRepository = new LibroRepository(context);
            var autorRepository = new AutorRepository(context);
            var generoRepository = new GeneroRepository(context);
            
            _libroService = new LibroService(libroRepository, autorRepository, generoRepository);
            _autorService = new AutorService(autorRepository);
            _generoService = new GeneroService(generoRepository);
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
            var librosDto = _libroService.GetAll().ToList();

            dgvLibros.DataSource = null;
            dgvLibros.DataSource = librosDto;
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
                // Validar que hay valores seleccionados
                if (cmbAutor.SelectedValue == null || cmbGenero.SelectedValue == null)
                {
                    MessageBox.Show("Por favor seleccione un autor y un género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Creamos el DTO para la creación a partir de los controles
                var libroDto = new CrearLibroDto
                {
                    Titulo = txtTitulo.Text,
                    ISBN = txtISBN.Text,
                    AutorId = Convert.ToInt32(cmbAutor.SelectedValue),
                    GeneroId = Convert.ToInt32(cmbGenero.SelectedValue)
                };

                _libroService.Add(libroDto);
                MessageBox.Show("Libro agregado con éxito.");
                CargarLibros();
                LimpiarControles();
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
                // Validar que hay valores seleccionados
                if (cmbAutor.SelectedValue == null || cmbGenero.SelectedValue == null)
                {
                    MessageBox.Show("Por favor seleccione un autor y un género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                var libroDto = new LibroDto
                {
                    Id = libroSeleccionado.Id,
                    Titulo = txtTitulo.Text,
                    ISBN = txtISBN.Text,
                    AutorId = Convert.ToInt32(cmbAutor.SelectedValue),
                    GeneroId = Convert.ToInt32(cmbGenero.SelectedValue)
                };

                if (_libroService.Update(libroDto))
                {
                    MessageBox.Show("Libro modificado con éxito.");
                    CargarLibros();
                    LimpiarControles();
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
                try
                {
                    var libroSeleccionado = (LibroDto)dgvLibros.SelectedRows[0].DataBoundItem;
                    if (_libroService.Delete(libroSeleccionado.Id))
                    {
                        MessageBox.Show("Libro eliminado con éxito.");
                        CargarLibros();
                        LimpiarControles();
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
        }

        private void LimpiarControles()
        {
            txtTitulo.Clear();
            txtISBN.Clear();
            if (cmbAutor.Items.Count > 0)
                cmbAutor.SelectedIndex = 0;
            if (cmbGenero.Items.Count > 0)
                cmbGenero.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}