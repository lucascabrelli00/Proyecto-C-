using ProyectoFinalCoderHouseCSharp.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouseCSharp.Repository
{
    public class ProductoHandler
    {

        public const string ConnectionString = "Server=PCCESAR;DataBase=SistemaGestion;Trusted_Connection=True";

        //Crear Producto
        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "IF NOT EXISTS(SELECT * FROM Producto WHERE Descripciones = @descripcionesParameter) BEGIN INSERT INTO [SistemaGestion].[dbo].[Producto] " +
                    "(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES " +
                    "(@descripcionesParameter, @costoParameter, @precioVentaParameter, @stockParameter, @idUsuarioParameter) END;";

                SqlParameter descripcionesParameter = new SqlParameter("descripcionesParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costoParameter", SqlDbType.BigInt) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVentaParameter", SqlDbType.BigInt) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stockParameter", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }

                }

                sqlConnection.Close();

            }
            return resultado;
        }

        //Modificar Producto
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                    "SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id ";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.BigInt) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.BigInt) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }

        //Eliminar Producto
        public static bool EliminarProducto(int idProducto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete2 = "DELETE FROM Producto WHERE Id = @idParameter";

                SqlParameter idParameter = new SqlParameter("idParameter", SqlDbType.BigInt) { Value = idProducto };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete2, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }

        //Traer Productos
        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();

                                resultados.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }


        //Traer Productos cargados por cierto usuario
        public static List<Producto> TraerProductos(int idUsuario)
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario = @idUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();

                                resultados.Add(producto);
                            }
                        }
                        else
                        {
                            Producto producto = new Producto();
                            producto.Id = 0;
                            producto.Descripciones = "No cargo ningun Producto, el Usuario: " + idUsuario ;
                            producto.Costo = 0;
                            producto.PrecioVenta = 0;
                            producto.Stock = 0;
                            producto.IdUsuario = idUsuario;

                            resultados.Add(producto);

                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }
    }
}
