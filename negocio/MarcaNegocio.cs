using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {


        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesosDatos datos = new AccesosDatos();

            datos.setQuery("SELECT Id, Descripcion FROM MARCAS");
            datos.executeReader();

            try
            {
                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }
                return lista;
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.closeConnection();
            }
        }
    }
}
