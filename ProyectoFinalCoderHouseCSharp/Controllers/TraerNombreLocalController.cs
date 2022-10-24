using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalCoderHouseCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraerNombreLocalController : ControllerBase
    {
        [HttpGet("{nombreLocal}")]
        public string TraerNombre(string nombreLocal)
        {
            return "Nombre del Local: " + nombreLocal;
        }
    }
}
