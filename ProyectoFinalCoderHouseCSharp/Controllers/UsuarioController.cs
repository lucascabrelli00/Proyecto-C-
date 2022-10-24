using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouseCSharp.Controllers.DTOS;
using ProyectoFinalCoderHouseCSharp.Model;
using ProyectoFinalCoderHouseCSharp.Repository;

namespace ProyectoFinalCoderHouseCSharp.Controllers
{
    [ApiController]
    [Route("controller")]
    public class UsuarioController : ControllerBase
    {
        //Inicio de sesion
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public bool InicioSesionUsuarios(string nombreUsuario, string contraseña)
        {
            Usuario usuario = UsuarioHandler.InicioSesionUsuarios(nombreUsuario, contraseña);
            if (usuario.Nombre == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //CREAR UN USUARIO
        [HttpPost]
        public string CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario
                {

                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error";
            }
        }

        //MODIFICAR UN USUARIO
        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail
            });
        }


        //Traer Usuario
        [HttpGet("{nombreUsuario}")]
        public List<Usuario> GetUsuarios(string nombreUsuario)
        {
            return UsuarioHandler.GetUsuarios(nombreUsuario);
        }

        //BORRAR USUARIO
        [HttpDelete("{id}")]
        public bool EliminarUsuario(int id)
        {
            try
            {
                return UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
