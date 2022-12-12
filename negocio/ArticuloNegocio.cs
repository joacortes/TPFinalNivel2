using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        
        
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesosDatos datos = new AccesosDatos();

            try
            {
                string consulta = "SELECT A.Id IdArticulo, Codigo, Nombre, A.Descripcion Articulo, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio, IdMarca, IdCategoria FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C ON A.IdCategoria = C.Id";
                datos.setQuery(consulta);
                datos.executeReader();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["IdArticulo"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Articulo"];

                    

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.closeConnection();
            }
        }

        public void addArticle(Articulo nuevo)
        {
            AccesosDatos datos = new AccesosDatos();

            try
            {
                datos.setQuery("INSERT INTO ARTICULOS VALUES (@codigo, @nombre, @descripcion, @IdMarca, @IdCategoria, @Url, @precio)");
                datos.setParameter("@codigo", nuevo.Codigo);
                datos.setParameter("@nombre", nuevo.Nombre);
                datos.setParameter("@descripcion", nuevo.Descripcion);
                datos.setParameter("@IdMarca", nuevo.Marca.Id);
                datos.setParameter("@IdCategoria", nuevo.Categoria.Id);
                datos.setParameter("@Url", nuevo.ImagenUrl);
                datos.setParameter("@precio", nuevo.Precio);

                datos.executeNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.closeConnection();
            }
            
        }

        public void modArticle(Articulo nuevo)
        {
            AccesosDatos datos = new AccesosDatos();

            try
            {
                datos.setQuery("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idmarca, IdCategoria = @idcategoria, ImagenUrl = @url, Precio = @precio WHERE Id = @IdArticulo");
                datos.setParameter("@codigo", nuevo.Codigo);
                datos.setParameter("@nombre", nuevo.Nombre);
                datos.setParameter("@descripcion", nuevo.Descripcion);
                datos.setParameter("@IdMarca", nuevo.Marca.Id);
                datos.setParameter("@IdCategoria", nuevo.Categoria.Id);
                datos.setParameter("@Url", nuevo.ImagenUrl);
                datos.setParameter("@precio", nuevo.Precio);

                datos.setParameter("@IdArticulo", nuevo.Id);
                datos.executeNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.closeConnection();
            }
        }

        public void deleteArticle(Articulo articulo)
        {
            AccesosDatos datos = new AccesosDatos();

            try
            {
                datos.setQuery("DELETE FROM ARTICULOS WHERE Id = @idArticulo");
                datos.setParameter("@idArticulo", articulo.Id);
                datos.executeNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.closeConnection();
            }
        }

        public List<Articulo> filterArticle(string campo, string criterio, string filtro)
        {
            AccesosDatos datos = new AccesosDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string consulta = "SELECT A.Id IdArticulo, Codigo, Nombre, A.Descripcion Articulo, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio, IdMarca, IdCategoria FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C ON A.IdCategoria = C.Id WHERE ";

                switch (campo)
                {
                    case "Codigo":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += " A.Codigo like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += " A.Codigo like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += " A.Codigo like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += " A.Nombre like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += " A.Nombre like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += " A.Nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Marca":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += " M.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += " M.Descripcion like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += " M.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Categoria":
                        switch (criterio)
                        {
                            case "Empieza con":
                                consulta += " C.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += " C.Descripcion like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += " C.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                }

                datos.setQuery(consulta);
                datos.executeReader();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["IdArticulo"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Articulo"];



                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

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
