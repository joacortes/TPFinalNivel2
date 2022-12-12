using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;

namespace TPIntegrador
{
    public partial class frmAgregar : Form
    {
        Articulo articulo = null;

        public frmAgregar()
        {
            InitializeComponent();
        }

        public frmAgregar(Articulo Articulo)
        {
            this.articulo = Articulo;
            InitializeComponent();
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            MarcaNegocio marca = new MarcaNegocio();
            CategoriaNegocio categoria = new CategoriaNegocio();
            try
            {
                cmbMarca.DataSource = marca.listar();
                cmbMarca.ValueMember = "Id";
                cmbMarca.DisplayMember = "Descripcion";

                cmbCategoria.DataSource = categoria.listar();
                cmbCategoria.ValueMember = "Id";
                cmbCategoria.DisplayMember = "Descripcion";

                if(articulo != null)
                {
                    Text = "Modificar articulo";
                    txtNombre.Text = articulo.Nombre;
                    txtCodigo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImagen.Text = articulo.ImagenUrl;
                    txtPrecio.Text = articulo.Precio.ToString();

                    uploadImage(articulo.ImagenUrl);

                    cmbMarca.SelectedValue = articulo.Marca.Id;
                    cmbCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            uploadImage(txtUrlImagen.Text);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {

                if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtPrecio.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos");
                    return;
                }
                    

                if (articulo == null)
                    articulo = new Articulo();

                articulo.Nombre = txtNombre.Text;
                articulo.Codigo = txtCodigo.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.ImagenUrl = txtUrlImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)cmbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;

                if (articulo.Id == 0)
                {
                    negocio.addArticle(articulo);
                    MessageBox.Show("Se agrego el articulo exitosamente");
                }
                else
                {
                    negocio.modArticle(articulo);
                    MessageBox.Show("El articulo se modifico exitosamente");
                }

                Close();
                    
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
