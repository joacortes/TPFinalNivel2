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
using dominio;

namespace TPIntegrador
{
    public partial class frmDetalle : Form
    {
        Articulo articulo = null;
        public frmDetalle()
        {
            InitializeComponent();
        }

        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            

            lblCodigo.Text = "Codigo: " + articulo.Codigo;
            lblNombre.Text = "Nombre: " + articulo.Nombre;
            lblDescripcion.Text = "Descripcion: " + articulo.Descripcion;
            lblCategoria.Text = "Categoria: " + articulo.Categoria.Descripcion;
            lblMarca.Text = "Marca: " + articulo.Marca.Descripcion;
            lblPrecio.Text = "Precio: $ " + articulo.Precio.ToString("00.00");

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
