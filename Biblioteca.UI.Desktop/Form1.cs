// EN: Biblioteca.UI.Desktop/Form1.cs

using Biblioteca.Domain.Services;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.UI.Desktop
{
    public partial class Form1 : Form
    {
        private readonly GeneroService _generoService;

        public Form1()
        {
            InitializeComponent();
            _generoService = new GeneroService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarGeneros();
        }

        // --- EVENTO NUEVO: SelectionChanged ---
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
                var nuevoGenero = new Genero(0, txtNombreGenero.Text);
                _generoService.Add(nuevoGenero);
                MessageBox.Show("G�nero agregado con �xito.");
                CargarGeneros();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENTO NUEVO: Click del bot�n Modificar ---
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvGeneros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un g�nero para modificar.", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Obtenemos el ID del g�nero seleccionado.
                var generoSeleccionado = (GeneroDto)dgvGeneros.SelectedRows[0].DataBoundItem;

                // 2. Creamos un nuevo objeto de dominio con el ID original y el nuevo nombre del TextBox.
                var generoModificado = new Genero(generoSeleccionado.Id, txtNombreGenero.Text);

                // 3. Llamamos al servicio de actualizaci�n.
                bool resultado = _generoService.Update(generoModificado);

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
        }

        // --- EVENTO NUEVO: Click del bot�n Eliminar ---
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
        }

        private void CargarGeneros()
        {
            List<Genero> generosDelDominio = _generoService.GetAll();
            List<GeneroDto> generosParaMostrar = generosDelDominio.Select(g => new GeneroDto
            {
                Id = g.Id,
                Nombre = g.Nombre
            }).ToList();

            // Es una buena pr�ctica limpiar el DataSource antes de reasignarlo.
            dgvGeneros.DataSource = null;
            dgvGeneros.DataSource = generosParaMostrar;

            // Limpiamos el texto y la selecci�n.
            txtNombreGenero.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Puedes dejar vac�o el m�todo o agregar l�gica si es necesaria
            // Por ejemplo, habilitar/deshabilitar botones seg�n si hay texto
        }

    }
}