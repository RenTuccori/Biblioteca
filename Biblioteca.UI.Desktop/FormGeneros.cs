using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class FormGeneros : Form
    {
        private readonly GeneroService _generoService;

        public FormGeneros()
        {
            InitializeComponent();
            
            // Configurar servicios con dependencias
            var context = DatabaseHelper.CreateDbContext();
            var generoRepository = new GeneroRepository(context);
            _generoService = new GeneroService(generoRepository);
        }

        private void FormGenero_Load(object sender, EventArgs e)
        {
            CargarGeneros();
        }

        private void dgvGeneros_SelectionChanged(object sender, EventArgs e)
        {
            // Verificamos si hay alguna fila seleccionada.
            if (dgvGeneros.SelectedRows.Count > 0)
            {
                // Obtenemos el objeto DTO de la fila seleccionada.
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;

                // Ponemos el nombre del g�nero en el TextBox para que el usuario pueda editarlo.
                txtNombreGenero.Text = generoSeleccionado.Nombre;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevoGeneroDto = new CrearGeneroDto { Nombre = txtNombreGenero.Text };
                _generoService.Add(nuevoGeneroDto);
                MessageBox.Show("G�nero agregado con �xito.");
                CargarGeneros();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un g�nero para modificar.", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Obtenemos el DTO del g�nero seleccionado.
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;

                // 2. Creamos un DTO con el ID original y el nuevo nombre del TextBox.
                var generoModificadoDto = new GeneroDto 
                { 
                    Id = generoSeleccionado.Id, 
                    Nombre = txtNombreGenero.Text 
                };

                // 3. Llamamos al servicio de actualizaci�n.
                bool resultado = _generoService.Update(generoModificadoDto);

                if (resultado)
                {
                    MessageBox.Show("G�nero modificado con �xito.");
                    CargarGeneros(); // Refrescamos la grilla.
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el g�nero para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un g�nero para eliminar.", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pedimos confirmaci�n antes de una acci�n destructiva.
            var confirmacion = MessageBox.Show("�Est� seguro de que desea eliminar este g�nero?", "Confirmar eliminaci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;
                    bool resultado = _generoService.Delete(generoSeleccionado.Id);

                    if (resultado)
                    {
                        MessageBox.Show("G�nero eliminado con �xito.");
                        CargarGeneros();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el g�nero para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarGeneros()
        {
            var generosDto = _generoService.GetAll().ToList();

            dgvGeneros.DataSource = null;
            dgvGeneros.DataSource = generosDto;

            txtNombreGenero.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}