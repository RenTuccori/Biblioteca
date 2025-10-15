using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormPrestamos : Form
    {
        private readonly PrestamoApiClient _prestamoApiClient;
        private readonly LibroApiClient _libroApiClient;
        private readonly UsuarioApiClient _usuarioApiClient;
        private List<PrestamoDto> _prestamos = new();
        private List<LibroDto> _librosDisponibles = new();
        private List<UsuarioDto> _socios = new();
        private string _filtroActual = "todos";

        public FormPrestamos(PrestamoApiClient prestamoApiClient, LibroApiClient libroApiClient, UsuarioApiClient usuarioApiClient)
        {
            InitializeComponent();
            _prestamoApiClient = prestamoApiClient;
            _libroApiClient = libroApiClient;
            _usuarioApiClient = usuarioApiClient;
        }

        private async void FormPrestamos_Load(object sender, EventArgs e)
        {
            await CargarDatos();
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

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbLibro.SelectedValue == null || cmbSocio.SelectedValue == null)
                {
                    MessageBox.Show("Por favor seleccione un libro y un socio.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoPrestamoDto = new CrearPrestamoDto 
                { 
                    LibroId = (int)cmbLibro.SelectedValue,
                    SocioId = (int)cmbSocio.SelectedValue,
                    FechaPrestamo = dtpFechaPrestamo.Value,
                    FechaDevolucionPrevista = dtpFechaDevolucionPrevista.Value
                };
                
                await _prestamoApiClient.CreateAsync(nuevoPrestamoDto);
                MessageBox.Show("Préstamo creado con éxito. El libro ahora está prestado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Por favor seleccione un libro y un socio.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var prestamoSeleccionado = (PrestamoDto)dgvPrestamos.SelectedRows[0].DataBoundItem;
                var prestamoModificadoDto = new PrestamoDto 
                { 
                    Id = prestamoSeleccionado.Id,
                    LibroId = (int)cmbLibro.SelectedValue,
                    SocioId = (int)cmbSocio.SelectedValue,
                    FechaPrestamo = dtpFechaPrestamo.Value,
                    FechaDevolucionPrevista = dtpFechaDevolucionPrevista.Value,
                    FechaDevolucionReal = prestamoSeleccionado.FechaDevolucionReal
                };
                
                var resultado = await _prestamoApiClient.UpdateAsync(prestamoModificadoDto);
                if (resultado)
                {
                    MessageBox.Show("Préstamo modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatos();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el préstamo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
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
                    var resultado = await _prestamoApiClient.DeleteAsync(prestamoSeleccionado.Id);
                    if (resultado)
                    {
                        MessageBox.Show("Préstamo eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarDatos();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el préstamo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnDevolver_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un préstamo para devolver.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var prestamoSeleccionado = (PrestamoDto)dgvPrestamos.SelectedRows[0].DataBoundItem;
            if (prestamoSeleccionado.FechaDevolucionReal.HasValue)
            {
                MessageBox.Show("Este libro ya fue devuelto.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Confirma la devolución de este libro?", "Confirmar devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    await _prestamoApiClient.DevolverAsync(prestamoSeleccionado.Id, DateTime.Now);
                    MessageBox.Show("Libro devuelto exitosamente. El libro ahora está disponible.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatos();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al devolver libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnVerActivos_Click(object sender, EventArgs e)
        {
            _filtroActual = "activos";
            await CargarPrestamos();
        }

        private async void btnVerVencidos_Click(object sender, EventArgs e)
        {
            _filtroActual = "vencidos";
            await CargarPrestamos();
        }

        private async void btnVerTodos_Click(object sender, EventArgs e)
        {
            _filtroActual = "todos";
            await CargarPrestamos();
        }

        private async Task CargarDatos()
        {
            try
            {
                await CargarLibrosDisponibles();
                await CargarSocios();
                await CargarPrestamos();
                ConfigurarFechas();
                // Dejar combos sin selección y botones deshabilitados para ingreso del usuario
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarPrestamos()
        {
            try
            {
                switch (_filtroActual)
                {
                    case "activos":
                        _prestamos = (await _prestamoApiClient.GetActivosAsync()).ToList();
                        break;
                    case "vencidos":
                        _prestamos = (await _prestamoApiClient.GetVencidosAsync()).ToList();
                        break;
                    default:
                        _prestamos = (await _prestamoApiClient.GetAllAsync()).ToList();
                        break;
                }

                dgvPrestamos.DataSource = null;
                dgvPrestamos.DataSource = _prestamos;
                
                if (dgvPrestamos.Columns.Count > 0)
                {
                    dgvPrestamos.Columns["Id"].HeaderText = "ID";
                    dgvPrestamos.Columns["LibroId"].HeaderText = "ID Libro";
                    dgvPrestamos.Columns["SocioId"].HeaderText = "ID Socio";
                    dgvPrestamos.Columns["LibroTitulo"].HeaderText = "Libro";
                    dgvPrestamos.Columns["SocioNombreCompleto"].HeaderText = "Socio";
                    dgvPrestamos.Columns["FechaPrestamo"].HeaderText = "Fecha Préstamo";
                    dgvPrestamos.Columns["FechaDevolucionPrevista"].HeaderText = "Devolución Prevista";
                    dgvPrestamos.Columns["FechaDevolucionReal"].HeaderText = "Devolución Real";
                    
                    // Ocultar IDs
                    dgvPrestamos.Columns["LibroId"].Visible = false;
                    dgvPrestamos.Columns["SocioId"].Visible = false;
                    
                    // Formato de fechas
                    dgvPrestamos.Columns["FechaPrestamo"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvPrestamos.Columns["FechaDevolucionPrevista"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvPrestamos.Columns["FechaDevolucionReal"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    
                    // Colorear filas vencidas
                    foreach (DataGridViewRow row in dgvPrestamos.Rows)
                    {
                        var prestamo = (PrestamoDto)row.DataBoundItem;
                        if (prestamo != null && !prestamo.FechaDevolucionReal.HasValue && 
                            prestamo.FechaDevolucionPrevista < DateTime.Now)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                    }
                }

                lblEstado.Text = $"Mostrando préstamos {_filtroActual} ({_prestamos.Count})";
                
                // Limpiar la selección para que no se cargue automáticamente el primer elemento
                dgvPrestamos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar préstamos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarLibrosDisponibles()
        {
            try
            {
                _librosDisponibles = (await _libroApiClient.GetAllAsync()).Where(l => l.Estado == "disponible").ToList();
                var librosCombo = _librosDisponibles.Select(l => new
                {
                    Id = l.Id,
                    Display = $"{l.Titulo} - {l.AutorNombreCompleto}"
                }).ToList();
                
                cmbLibro.DataSource = librosCombo;
                cmbLibro.DisplayMember = "Display";
                cmbLibro.ValueMember = "Id";
                cmbLibro.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar libros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarSocios()
        {
            try
            {
                _socios = (await _usuarioApiClient.GetByRolAsync("socio")).ToList();
                var sociosCombo = _socios.Select(s => new
                {
                    Id = s.Id,
                    Display = $"{s.NombreUsuario} - {s.PersonaNombreCompleto}"
                }).ToList();
                
                cmbSocio.DataSource = sociosCombo;
                cmbSocio.DisplayMember = "Display";
                cmbSocio.ValueMember = "Id";
                cmbSocio.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar socios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarFechas()
        {
            dtpFechaPrestamo.Value = DateTime.Now;
            dtpFechaDevolucionPrevista.Value = DateTime.Now.AddDays(15);
        }

        private void LimpiarCampos()
        {
            cmbLibro.SelectedIndex = -1;
            cmbSocio.SelectedIndex = -1;
            ConfigurarFechas();
            btnDevolver.Enabled = false;
            dgvPrestamos.ClearSelection();
        }
    }
}
