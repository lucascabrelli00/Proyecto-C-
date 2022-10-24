using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouseCSharp.Controllers.DTOS;
using ProyectoFinalCoderHouseCSharp.Model;
using ProyectoFinalCoderHouseCSharp.Repository;

namespace ProyectoFinalCoderHouseCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {

        //Crear Producto
        [HttpPost]
        public bool CrearProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CrearProducto(new Producto
                {

                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario,

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Modificar un Producto
        [HttpPut]
        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            return ProductoHandler.ModificarProducto(new Producto
            {

                Id = producto.Id,
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario,

            });
        }

        //Eliminar un Producto
        [HttpDelete("{idProducto}")]
        public bool EliminarProducto(int idProducto)
        {
            try
            {
                return ProductoHandler.EliminarProducto(idProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Traer todos los productos
        [HttpGet]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        //Traer todos los productos de cierto Usuario
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductos(int idUsuario)
        {
            return ProductoHandler.TraerProductos(idUsuario);
        }

    }
}
