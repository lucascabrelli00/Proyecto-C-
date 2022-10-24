using ProyectoFinalCoderHouseCSharp.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouseCSharp.Repository
{
    public class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=PCCESAR;DataBase=SistemaGestion;Trusted_Connection=True";

        //Traer Productos Vendidos de Cierto Usuario
        public static List<ProductoVendidoWithProducto> ProductoVendidoWithProducto(int idVenta)
        {
            List<ProductoVendidoWithProducto> productosVendido = new List<ProductoVendidoWithProducto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT ProductoVendido.Id,ProductoVendido.Stock,ProductoVendido.IdProducto,ProductoVendido.IdVenta,Producto.Descripciones,Producto.IdUsuario FROM Producto INNER JOIN ProductoVendido ON Producto.Id = ProductoVendido.IdProducto WHERE IdVenta = @idVenta", sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@idVenta", idVenta);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                ProductoVendidoWithProducto productoVendido = new ProductoVendidoWithProducto();

                                productoVendido.ProductoVendidoId = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                productoVendido.Descripciones = dataReader["Descripciones"].ToString();
                                productoVendido.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                productosVendido.Add(productoVendido);

                            }
                        }
                        else
                        {
                            ProductoVendidoWithProducto productoVendido = new ProductoVendidoWithProducto();

                            productoVendido.ProductoVendidoId = 0;
                            productoVendido.Stock = 0;
                            productoVendido.IdProducto = 0;
                            productoVendido.IdVenta = idVenta;
                            productoVendido.Descripciones = "";
                            productoVendido.IdUsuario = 0;


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
