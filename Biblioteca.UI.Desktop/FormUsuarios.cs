using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormUsuarios : Form
    {
        private readonly UsuarioApiClient _usuarioApiClient;
        private readonly PersonaApiClient _personaApiClient;
        private List<UsuarioDto> _usuarios = new();
        private List<PersonaDto> _personas = new();

        public FormUsuarios(UsuarioApiClient usuarioApiClient, PersonaApiClient personaApiClient)
        {
            InitializeComponent();
            _usuarioApiClient = usuarioApiClient;
            _personaApiClient = personaApiClient;
        }

        private async void FormUsuarios_Load(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                var usuarioSeleccionado = (UsuarioDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                txtNombreUsuario.Text = usuarioSeleccionado.NombreUsuario;
                txtClave.Text = usuarioSeleccionado.Clave;
                cmbRol.SelectedItem = usuarioSeleccionado.Rol;
                
                // Buscar y mostrar datos de la persona
                var persona = _personas.FirstOrDefault(p => p.Id == usuarioSeleccionado.PersonaId);
                if (persona != null)
                {
                    txtNombre.Text = persona.Nombre;
                    txtApellido.Text = persona.Apellido;
                    txtDni.Text = persona.Dni;
                    txtEmail.Text = persona.Email;
                }
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtDni.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreUsuario.Text) || 
                    string.IsNullOrWhiteSpace(txtClave.Text) ||
                    cmbRol.SelectedItem == null)
                {
                    MessageBox.Show("Todos los campos son requeridos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Primero crear la persona
                var nuevaPersonaDto = new CrearPersonaDto
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Dni = txtDni.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };
                
                var personaCreada = await _personaApiClient.CreateAsync(nuevaPersonaDto);

                // Luego crear el usuario asociado
                var nuevoUsuarioDto = new CrearUsuarioDto 
                { 
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    Clave = txtClave.Text,
                    Rol = cmbRol.SelectedItem.ToString()!,
                    PersonaId = personaCreada.Id
                };
                
                await _usuarioApiClient.CreateAsync(nuevoUsuarioDto);
                MessageBox.Show("Usuario agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtDni.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreUsuario.Text) || 
                    string.IsNullOrWhiteSpace(txtClave.Text) ||
                    cmbRol.SelectedItem == null)
                {
                    MessageBox.Show("Todos los campos son requeridos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuarioSeleccionado = (UsuarioDto)dgvUsuarios.SelectedRows[0].DataBoundItem;

                // Actualizar la persona
                var personaDto = new PersonaDto
                {
                    Id = usuarioSeleccionado.PersonaId,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Dni = txtDni.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };
                
                await _personaApiClient.UpdateAsync(personaDto);

                // Actualizar el usuario
                var usuarioModificadoDto = new UsuarioDto 
                { 
                    Id = usuarioSeleccionado.Id,
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    Clave = txtClave.Text,
                    Rol = cmbRol.SelectedItem.ToString()!,
                    PersonaId = usuarioSeleccionado.PersonaId
                };
                
                var resultado = await _usuarioApiClient.UpdateAsync(usuarioModificadoDto);
                if (resultado)
                {
                    MessageBox.Show("Usuario modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatos();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este usuario? Esto también eliminará la persona asociada.", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var usuarioSeleccionado = (UsuarioDto)dgvUsuarios.SelectedRows[0].DataBoundItem;
                    
                    // Eliminar el usuario (esto debería manejar la cascada en la API)
                    var resultado = await _usuarioApiClient.DeleteAsync(usuarioSeleccionado.Id);
                    if (resultado)
                    {
                        MessageBox.Show("Usuario eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarDatos();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task CargarDatos()
        {
            try
            {
                await CargarPersonas();
                await CargarUsuarios();
                ConfigurarComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarUsuarios()
        {
            try
            {
                _usuarios = (await _usuarioApiClient.GetAllAsync()).ToList();
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = _usuarios;
                
                if (dgvUsuarios.Columns.Count > 0)
                {
                    dgvUsuarios.Columns["Id"].HeaderText = "ID";
                    dgvUsuarios.Columns["NombreUsuario"].HeaderText = "Usuario";
                    dgvUsuarios.Columns["Clave"].HeaderText = "Clave";
                    dgvUsuarios.Columns["Rol"].HeaderText = "Rol";
                    dgvUsuarios.Columns["PersonaId"].HeaderText = "ID Persona";
                    dgvUsuarios.Columns["PersonaNombreCompleto"].HeaderText = "Nombre Completo";
                    
                    // Ocultar campos sensibles
                    dgvUsuarios.Columns["Clave"].Visible = false;
                    dgvUsuarios.Columns["PersonaId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarPersonas()
        {
            try
            {
                _personas = (await _personaApiClient.GetAllAsync()).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarComboBoxes()
        {
            // Configurar combo de roles
            cmbRol.Items.Clear();
            cmbRol.Items.AddRange(new[] { "administrador", "bibliotecario", "socio" });
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtNombreUsuario.Clear();
            txtClave.Clear();
            cmbRol.SelectedIndex = -1;
        }
    }
}
