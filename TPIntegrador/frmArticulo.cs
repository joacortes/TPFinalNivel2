using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPIntegrador
{
    
    public partial class frmArticulo : Form
    {
        private List<Articulo> listaArticulo = new List<Articulo>();
        private List<string> listaCampo = new List<string>() { "Codigo", "Nombre", "Marca", "Categoria" };
        private List<string> listaCriterio = new List<string>() { "Empieza con", "Termina con", "Contiene" };
        public frmArticulo()
        {
            InitializeComponent();
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {

            foreach (string lista in listaCampo)
                cmbCampo.Items.Add(lista);

            updateTable();
            
        }

        
        public bool validateFilter()
        {
            if(cmbCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Debe ingresar el campo");
                return true;
            }
            if(cmbCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Debe ingresar el criterio");
                return true;
            }

            return false;
        }

        public void updateTable()
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            try
            {
                listaArticulo = articulo.listar();
                dgvArticulo.DataSource = listaArticulo;
                hideColumns();

                uploadImage(listaArticulo[0].ImagenUrl);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void uploadImage(string imageUrl)
        {
            try
            {
                pbImagen.Load(imageUrl);
            }
            catch
            {
                pbImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRzSH3L8jGqve0cTlNx9_1-dtSldkgtWKYLNLnU3KWk5EXMpbimLUa0JgmcvE1yWvZ_Q4&usqp=CAU");
            }
        }
        public void hideColumns()
        {
            dgvArticulo.Columns["Id"].Visible = false;
            dgvArticulo.Columns["Precio"].Visible = false;
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulo.CurrentRow != null)
            {
                Articulo articleSelected = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                uploadImage(articleSelected.ImagenUrl);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar ventanaAgregar = new frmAgregar();
            ventanaAgregar.ShowDialog();
            updateTable();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(dgvArticulo.CurrentRow != null)
            {
                Articulo seleccionado;

                seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

                frmAgregar ventanaModificar = new frmAgregar(seleccionado);

                ventanaModificar.ShowDialog();
                updateTable();
            }
            else
            {
                MessageBox.Show("No hay ningun item seleccionado en la grilla");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            if(dgvArticulo.CurrentRow != null)
            {
                Articulo seleccionado;

                seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

                negocio.deleteArticle(seleccionado);
                MessageBox.Show("El articulo se elimino correctamente");
                updateTable();
            }
            else
            {
                MessageBox.Show("No hay ningun articulo para eliminar");
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            try
            {
                if (validateFilter())
                    return;

                string campo = cmbCampo.SelectedItem.ToString();
                string criterio = cmbCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dgvArticulo.DataSource = articulo.filterArticle(campo, criterio, filtro);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCampo.SelectedItem != null)
            {
                cmbCriterio.Items.Clear();
                foreach (string lista in listaCriterio)
                    cmbCriterio.Items.Add(lista);
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow != null)
            {
                Articulo seleccionado;

                seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

                frmDetalle ventanaDetalle = new frmDetalle(seleccionado);

                ventanaDetalle.ShowDialog();
                updateTable();
            }
            else
            {
                MessageBox.Show("No hay ningun item seleccionado en la grilla");
            }
        }
    }
}
