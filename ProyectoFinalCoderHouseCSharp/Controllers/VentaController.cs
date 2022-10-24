using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouseCSharp.Controllers.DTOS;
using ProyectoFinalCoderHouseCSharp.Model;
using ProyectoFinalCoderHouseCSharp.Repository;

namespace ProyectoFinalCoderHouseCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {

        //Cargar Venta
        [HttpPost]
        public bool CargarVenta([FromBody] PostVenta venta, int Stock, int IdProducto)
        {
            try
            {
                return VentaHandler.CargarVenta(Stock, IdProducto, new Venta
                {

                    Comentarios = venta.Comentarios,
                    IdUsuario = venta.IdUsuario

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Eliminar Venta
        [HttpDelete("{id}/{comentario}")]
        public bool EliminarVenta(int id,string comentario)
        {
            try
            {
                return VentaHandler.EliminarVenta(id,comentario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Traer Venta
        [HttpGet]
        public List<ProductoVendidoWithVenta> ProductoVendidoWithVenta()
        {
            return VentaHandler.ProductoVendidoWithVenta();
        }

        //Traer Ventas de cierto Usuario
        [HttpGet("{idVenta}")]
        public List<ProductoVendidoWithVenta> ProductoVendidoWithVenta(int idVenta)
        {
            return VentaHandler.ProductoVendidoWithVenta(idVenta);
        }
    }
}
