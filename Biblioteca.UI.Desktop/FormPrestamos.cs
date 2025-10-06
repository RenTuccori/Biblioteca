using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormPrestamos : Form
    {
        private readonly PrestamoService _prestamoService;
        private readonly LibroService _libroService;
        private readonly UsuarioService _usuarioService;

        public FormPrestamos()
        {
            InitializeComponent();
            
            var context = DatabaseHelper.CreateDbContext();
            var prestamoRepository = new PrestamoRepository(context);
            var libroRepository = new LibroRepository(context);
            var usuarioRepository = new UsuarioRepository(context);
            var autorRepository = new AutorRepository(context);
            var generoRepository = new GeneroRepository(context);
            var editorialRepository = new EditorialRepository(context);
            var personaRepository = new PersonaRepository(context);
            
            _prestamoService = new PrestamoService(prestamoRepository, libroRepository, usuarioRepository);
            _libroService = new LibroService(libroRepository, autorRepository, generoRepository, editorialRepository);
            _usuarioService = new UsuarioService(usuarioRepository, personaRepository);
        }

        private void FormPrestamos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarPrestamos();
            ConfigurarFechas();
        }

        private void ConfigurarFechas()
        {
            dtpFechaPrestamo.Value = DateTime.Now;
            dtpFechaDevolucionPrevista.Value = DateTime.Now.AddDays(15); // 15 días por defecto
        }

        private void CargarCombos()
        {
            // Cargar libros disponibles
            var librosDisponibles = _libroService.GetByEstado("disponible").Select(l => new
            {
                Id = l.Id,
                Display = $"{l.Titulo} - {l.AutorNombreCompleto}"
            }).ToList();
            
            cmbLibro.DataSource = librosDisponibles;
            cmbLibro.DisplayMember = "Display";
            cmbLibro.ValueMember = "Id";

            // Cargar solo socios (usuarios con rol socio)
            var socios = _usuarioService.GetByRol("socio").Select(u => new
            {
                Id = u.Id,
                Display = $"{u.NombreUsuario} - {u.PersonaNombreCompleto}"
            }).ToList();
            
            cmbSocio.DataSource = socios;
            cmbSocio.DisplayMember = "Display";
            cmbSocio.ValueMember = "Id";
        }

        private void dgvPrestamos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count > 0)
            {
                var prestamoSeleccionado = (PrestamoDto)dgvPrestamos.SelectedRows[0].DataBoundItem;
                
                cmbLibro.SelectedValue = prestamoSeleccionado.LibroId;
                cmbSocio.SelectedValue = prestamoSeleccionado.SocioId;
                dtpFechaPrestamo.Value = prestamoSeleccionado.FechaPrestamo;
                dtpFechaDevolucionPrevista.Value = prestamoSeleccionado.FechaDevolucionPrevista;
                
                // Habilitar botón devolver solo si no ha sido devuelto
                btnDevolver.Enabled = !prestamoSeleccionado.FechaDevolucionReal.HasValue;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbLibro.SelectedValue == null || cmbSocio.SelectedValue == null)
                {
                    MessageBox.Show("Por favor seleccione un libro y un socio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoPrestamoDto = new CrearPrestamoDto
                {
                    LibroId = Convert.ToInt32(cmbLibro.SelectedValue),
                    SocioId = Convert.ToInt32(cmbSocio.SelectedValue),
                    FechaPrestamo = dtpFechaPrestamo.Value,
                    FechaDevolucionPrevista = dtpFechaDevolucionPrevista.Value
                };
                
                _prestamoService.Add(nuevoPrestamoDto);
                MessageBox.Show("Préstamo registrado con éxito. El libro ahora está en estado 'prestado'.");
                CargarPrestamos();
                CargarCombos(); // Recargar para actualizar libros disponibles
                LimpiarControles();
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
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un préstamo para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                if (cmbLibro.SelectedValue == null || cmbSocio.SelectedValue == null)
                {
                    MessageBox.Show("Por favor seleccione un libro y un socio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var prestamoSeleccionado = (PrestamoDto)dgvPrestamos.SelectedRows[0].DataBoundItem;
                
                var prestamoDto = new PrestamoDto
                {
                    Id = prestamoSeleccionado.Id,
                    LibroId = Convert.ToInt32(cmbLibro.SelectedValue),
                    SocioId = Convert.ToInt32(cmbSocio.SelectedValue),
                    FechaPrestamo = dtpFechaPrestamo.Value,
                    FechaDevolucionPrevista = dtpFechaDevolucionPrevista.Value,
                    FechaDevolucionReal = prestamoSeleccionado.FechaDevolucionReal
                };
                
                if (_prestamoService.Update(prestamoDto))
                {
                    MessageBox.Show("Préstamo modificado con éxito.");
                    CargarPrestamos();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el préstamo para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un préstamo para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este préstamo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var prestamoSeleccionado = (PrestamoDto)dgvPrestamos.SelectedRows[0].DataBoundItem;
                    
                    if (_prestamoService.Delete(prestamoSeleccionado.Id))
                    {
                        MessageBox.Show("Préstamo eliminado con éxito. El libro ha vuelto a estado 'disponible'.");
                        CargarPrestamos();
                        CargarCombos(); // Recargar para actualizar libros disponibles
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el préstamo para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un préstamo para devolver.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Confirma la devolución de este libro?", "Confirmar devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var prestamoSeleccionado = (PrestamoDto)dgvPrestamos.SelectedRows[0].DataBoundItem;
                    
                    if (_prestamoService.DevolverLibro(prestamoSeleccionado.Id, DateTime.Now))
                    {
                        MessageBox.Show("Libro devuelto con éxito. El libro ha vuelto a estado 'disponible'.");
                        CargarPrestamos();
                        CargarCombos(); // Recargar para actualizar libros disponibles
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar la devolución.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVerActivos_Click(object sender, EventArgs e)
        {
            var prestamosActivos = _prestamoService.GetPrestamosActivos().ToList();
            dgvPrestamos.DataSource = null;
            dgvPrestamos.DataSource = prestamosActivos;
            lblEstado.Text = $"Mostrando préstamos activos ({prestamosActivos.Count})";
        }

        private void btnVerVencidos_Click(object sender, EventArgs e)
        {
            var prestamosVencidos = _prestamoService.GetPrestamosVencidos().ToList();
            dgvPrestamos.DataSource = null;
            dgvPrestamos.DataSource = prestamosVencidos;
            lblEstado.Text = $"Mostrando préstamos vencidos ({prestamosVencidos.Count})";
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            CargarPrestamos();
        }

        private void CargarPrestamos()
        {
            var prestamosDto = _prestamoService.GetAll().ToList();

            dgvPrestamos.DataSource = null;
            dgvPrestamos.DataSource = prestamosDto;

            lblEstado.Text = $"Mostrando todos los préstamos ({prestamosDto.Count})";
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            if (cmbLibro.Items.Count > 0)
                cmbLibro.SelectedIndex = 0;
            if (cmbSocio.Items.Count > 0)
                cmbSocio.SelectedIndex = 0;
            
            ConfigurarFechas();
            btnDevolver.Enabled = false;
        }
    }
}
