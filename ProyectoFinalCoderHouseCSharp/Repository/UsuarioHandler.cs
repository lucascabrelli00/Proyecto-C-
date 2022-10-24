using ProyectoFinalCoderHouseCSharp.Model;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouseCSharp.Repository
{
    public class UsuarioHandler
    {
        public const string ConnectionString = "Server=PCCESAR;DataBase=SistemaGestion;Trusted_Connection=True";

        //Inicio de sesion
        public static Usuario InicioSesionUsuarios(string nombreUsuario, string contraseña)
        {
            Usuario resultado = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuarioParameter AND Contraseña = @contraseñaParameter", sqlConnection))
                {
                    
                    sqlCommand.Parameters.AddWithValue("@nombreUsuarioParameter", nombreUsuario);
                    sqlCommand.Parameters.AddWithValue("@contraseñaParameter", contraseña);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                resultado = usuario;
                            }
                        }
                    }

                    sqlConnection.Close();
                }
                
                return resultado;
            }

        }

        //Crear Usuario
        public static string CrearUsuario(Usuario usuario)
        {
            string resultado = "NO SE LOGRO CREAR EL USUARIO, posibles errores: " +
                "\n* Ya existe una cuenta con ese nombre y apellido." +
                "\n* Nombre de usuario ya en uso." +
                "\n* Ya existe una cuenta con ese mail.";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "IF NOT EXISTS(SELECT * FROM Usuario " +
                    "WHERE (Nombre = @nombreParameter AND Apellido = @apellidoParameter) OR NombreUsuario = @nombreUsuarioParameter OR Mail = @mailParameter) " +
                    "BEGIN INSERT INTO [SistemaGestion].[dbo].[Usuario] " +
                    "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES " +
                    "(@nombreParameter, @apellidoParameter, @nombreUsuarioParameter, @contraseñaParameter, @mailParameter) END;";

                SqlParameter nombreParameter = new SqlParameter("nombreParameter", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mailParameter", SqlDbType.VarChar) { Value = usuario.Mail };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = "SE CREO EL USUARIO CON EXITO";
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        //Modificar Usuario
        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                    "SET Nombre = @nombre ,Apellido = @apellido ,NombreUsuario = @nombreUsuario ,Contraseña = @contraseña ,Mail = @mail WHERE Id = @id";
                //string queryInsert = "IF EXISTS (SELECT * FROM Usuario WHERE ((Nombre=@nombre AND Apellido = @apellido) OR NombreUsuario = @nombreUsuario OR Mail = @mail))" +
                //    "BEGIN UPDATE [SistemaGestion].[dbo].[Usuario] SET Nombre=@nombre,Apellido=@apellido,NombreUsuario=@nombreUsuario,Contraseña=@contraseña,Mail=@mail " +
                //    "WHERE Id = @id END;";
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };
                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

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


        //Traer Usuario
        public static List<Usuario> GetUsuarios(string nombreUsuario)
        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                resultados.Add(usuario);
                            }
                        }
                        else
                        {
                            Usuario usuario = new Usuario();

                            usuario.Id = 0;
                            usuario.NombreUsuario = "";
                            usuario.Nombre = "";
                            usuario.Apellido = "";
                            usuario.Contraseña = "";
                            usuario.Mail = "";

                            resultados.Add(usuario);

                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }


        //Eliminar Usuario
        public static bool EliminarUsuario(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Usuario WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
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
    }
}
