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

                // Ponemos el nombre del género en el TextBox para que el usuario pueda editarlo.
                txtNombreGenero.Text = generoSeleccionado.Nombre;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevoGeneroDto = new CrearGeneroDto { Nombre = txtNombreGenero.Text };
                _generoService.Add(nuevoGeneroDto);
                MessageBox.Show("Género agregado con éxito.");
                CargarGeneros();
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
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un género para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Obtenemos el DTO del género seleccionado.
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;

                // 2. Creamos un DTO con el ID original y el nuevo nombre del TextBox.
                var generoModificadoDto = new GeneroDto 
                { 
                    Id = generoSeleccionado.Id, 
                    Nombre = txtNombreGenero.Text 
                };

                // 3. Llamamos al servicio de actualización.
                bool resultado = _generoService.Update(generoModificadoDto);

                if (resultado)
                {
                    MessageBox.Show("Género modificado con éxito.");
                    CargarGeneros(); // Refrescamos la grilla.
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el género para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un género para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pedimos confirmación antes de una acción destructiva.
            var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este género?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;
                    bool resultado = _generoService.Delete(generoSeleccionado.Id);

                    if (resultado)
                    {
                        MessageBox.Show("Género eliminado con éxito.");
                        CargarGeneros();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar el género para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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