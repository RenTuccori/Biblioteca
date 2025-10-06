using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormUsuarios : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly PersonaService _personaService;

        public FormUsuarios()
        {
            InitializeComponent();
            
            var context = DatabaseHelper.CreateDbContext();
            var usuarioRepository = new UsuarioRepository(context);
            var personaRepository = new PersonaRepository(context);
            
            _usuarioService = new UsuarioService(usuarioRepository, personaRepository);
            _personaService = new PersonaService(personaRepository);
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            ConfigurarComboRol();
        }

        private void ConfigurarComboRol()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("socio");
            cmbRol.Items.Add("bibliotecario");
            cmbRol.SelectedIndex = 0;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                var usuarioSeleccionado = (UsuarioDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                
                // Cargar datos del usuario
                txtNombreUsuario.Text = usuarioSeleccionado.NombreUsuario;
                txtClave.Text = usuarioSeleccionado.Clave;
                cmbRol.SelectedItem = usuarioSeleccionado.Rol;
                
                // Cargar datos de la persona asociada
                var persona = _personaService.Get(usuarioSeleccionado.PersonaId);
                if (persona != null)
                {
                    txtNombre.Text = persona.Nombre;
                    txtApellido.Text = persona.Apellido;
                    txtDni.Text = persona.Dni;
                    txtEmail.Text = persona.Email;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Primero crear la persona
                var nuevaPersonaDto = new CrearPersonaDto
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Dni = txtDni.Text,
                    Email = txtEmail.Text
                };
                
                var personaCreada = _personaService.Add(nuevaPersonaDto);
                
                // Luego crear el usuario asociado
                var nuevoUsuarioDto = new CrearUsuarioDto
                {
                    NombreUsuario = txtNombreUsuario.Text,
                    Clave = txtClave.Text,
                    Rol = cmbRol.SelectedItem?.ToString() ?? "socio",
                    PersonaId = personaCreada.Id
                };
                
                _usuarioService.Add(nuevoUsuarioDto);
                MessageBox.Show("Usuario agregado con éxito.");
                CargarUsuarios();
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
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                var usuarioSeleccionado = (UsuarioDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                
                // Actualizar la persona
                var personaDto = new PersonaDto
                {
                    Id = usuarioSeleccionado.PersonaId,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Dni = txtDni.Text,
                    Email = txtEmail.Text
                };
                
                _personaService.Update(personaDto);
                
                // Actualizar el usuario
                var usuarioDto = new UsuarioDto
                {
                    Id = usuarioSeleccionado.Id,
                    NombreUsuario = txtNombreUsuario.Text,
                    Clave = txtClave.Text,
                    Rol = cmbRol.SelectedItem?.ToString() ?? "socio",
                    PersonaId = usuarioSeleccionado.PersonaId
                };
                
                if (_usuarioService.Update(usuarioDto))
                {
                    MessageBox.Show("Usuario modificado con éxito.");
                    CargarUsuarios();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el usuario para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var usuarioSeleccionado = (UsuarioDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                    
                    // Eliminar primero el usuario
                    if (_usuarioService.Delete(usuarioSeleccionado.Id))
                    {
                        // Luego eliminar la persona asociada
                        _personaService.Delete(usuarioSeleccionado.PersonaId);
                        
                        MessageBox.Show("Usuario eliminado con éxito.");
                        CargarUsuarios();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el usuario para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarUsuarios()
        {
            var usuariosDto = _usuarioService.GetAll().ToList();

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuariosDto;

            LimpiarControles();
        }

        private void LimpiarControles()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtNombreUsuario.Clear();
            txtClave.Clear();
            cmbRol.SelectedIndex = 0;
        }
    }
}
