using ProyectoFinalCoderHouseCSharp.Model;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ProyectoFinalCoderHouseCSharp.Repository
{
    public class VentaHandler
    {
        public const string ConnectionString = "Server=PCCESAR;DataBase=SistemaGestion;Trusted_Connection=True";

        //Cargar Venta
        public static bool CargarVenta(int Stock, int IdProducto, Venta venta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "IF EXISTS(SELECT * FROM Producto WHERE Descripciones = @comentariosParameter) BEGIN INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios,IdUsuario) VALUES (@comentariosParameter,@idUsuarioParameter) END;";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = venta.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }

                }
                sqlConnection.Close();

            }

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert2 = "IF EXISTS(SELECT Descripciones FROM Producto WHERE Descripciones = @comentariosParameter) BEGIN INSERT INTO ProductoVendido(Stock,IdProducto,IdVenta) VALUES (@stockParameter,@idProductoParameter,@idUsuarioParameter) END;";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter stockParameter = new SqlParameter("stockParameter", SqlDbType.BigInt) { Value = Stock };
                SqlParameter idProductoParameter = new SqlParameter("idProductoParameter", SqlDbType.BigInt) { Value = IdProducto };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = venta.IdUsuario };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert2, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);


                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                }

                sqlConnection.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                string queryUpdate = "UPDATE Producto SET Stock = Stock - @stockParameter WHERE Descripciones = @descripcionParameter;";

                SqlParameter stockParameter = new SqlParameter("stockParameter", SqlDbType.BigInt) { Value = Stock};
                SqlParameter descripcionParameter = new SqlParameter("descripcionParameter", SqlDbType.VarChar) { Value = venta.Comentarios };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionParameter);
                    sqlCommand.Parameters.Add(stockParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                }

                sqlConnection.Close();
            }

            return resultado;
        }

        //Eliminar Venta
        public static bool EliminarVenta(int id,string comentario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "IF EXISTS(SELECT Id,Comentarios FROM Venta WHERE Id = @idParameter1 AND Comentarios = @descripcionesParameter) " +
                    "BEGIN UPDATE Producto SET Stock = Stock + 1 WHERE Descripciones = @descripcionesParameter END;";
                SqlParameter descripcionesParameter = new SqlParameter("descripcionesParameter", SqlDbType.VarChar) { Value = comentario };
                SqlParameter idParameter1 = new SqlParameter("idParameter1", SqlDbType.BigInt) { Value = id };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(idParameter1);


                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                }

                sqlConnection.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Venta WHERE Id = @idParameter";

                SqlParameter idParameter = new SqlParameter("idParameter", SqlDbType.BigInt) { Value = id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
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
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete2 = "DELETE FROM ProductoVendido WHERE IdProducto = @idParameter1";

                SqlParameter idParameter1 = new SqlParameter("idParameter1", SqlDbType.BigInt) { Value = id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete2, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter1);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                }

                sqlConnection.Close();
            }
            return resultado;
        }

        //Traer Ventas
        public static List<ProductoVendidoWithVenta> ProductoVendidoWithVenta()
        {
            List<ProductoVendidoWithVenta> productosVendido = new List<ProductoVendidoWithVenta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT Venta.Id,Venta.Comentarios,Venta.IdUsuario,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta " +
                    "FROM ProductoVendido INNER JOIN Venta ON Venta.IdUsuario = ProductoVendido.IdVenta", sqlConnection))
                {

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                ProductoVendidoWithVenta productoVendido = new ProductoVendidoWithVenta();

                                productoVendido.VentaId = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Comentarios = dataReader["Comentarios"].ToString();
                                productoVendido.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                productosVendido.Add(productoVendido);

                            }
                        }
                        else
                        {
                            ProductoVendidoWithVenta productoVendido = new ProductoVendidoWithVenta();

                            productoVendido.VentaId = 0;
                            productoVendido.Comentarios = "";
                            productoVendido.IdUsuario = 0;
                            productoVendido.Stock = 0;
                            productoVendido.IdProducto = 0;
                            productoVendido.IdVenta = 0;


                            productosVendido.Add(productoVendido);
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productosVendido;
        }

        //Traer Ventas de cierto Usuario
        public static List<ProductoVendidoWithVenta> ProductoVendidoWithVenta(int idVenta)
        {
            List<ProductoVendidoWithVenta> productosVendido = new List<ProductoVendidoWithVenta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT Venta.Id,Venta.Comentarios,Venta.IdUsuario,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta " +
                    "FROM ProductoVendido INNER JOIN Venta ON Venta.IdUsuario = ProductoVendido.IdVenta WHERE IdVenta = @idVenta", sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@idVenta", idVenta);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                ProductoVendidoWithVenta productoVendido = new ProductoVendidoWithVenta();

                                productoVendido.VentaId = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Comentarios = dataReader["Comentarios"].ToString();
                                productoVendido.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                productosVendido.Add(productoVendido);

                            }
                        }
                        else
                        {
                            ProductoVendidoWithVenta productoVendido = new ProductoVendidoWithVenta();

                            productoVendido.VentaId = 0;
                            productoVendido.Comentarios = "";
                            productoVendido.IdUsuario = 0;
                            productoVendido.Stock = 0;
                            productoVendido.IdProducto = 0;
                            productoVendido.IdVenta = idVenta;


                            productosVendido.Add(productoVendido);
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productosVendido;
        }
    }
}
