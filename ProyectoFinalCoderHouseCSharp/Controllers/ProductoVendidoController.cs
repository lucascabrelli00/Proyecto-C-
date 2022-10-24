using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouseCSharp.Model;
using ProyectoFinalCoderHouseCSharp.Repository;

namespace ProyectoFinalCoderHouseCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        //Traer Productos vendidos de cierto Usuario
        [HttpGet("{idVenta}")]
        public List<ProductoVendidoWithProducto> ProductoVendidoWithProducto(int idVenta)
        {
            return ProductoVendidoHandler.ProductoVendidoWithProducto(idVenta);
        }
    }
}
