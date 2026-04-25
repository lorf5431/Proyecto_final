using Microsoft.Data.SqlClient;
using SistemaAutobuses.Entidades;
using System.Data;

namespace SistemaAutobuses.Datos
{
    public class UsuarioRepository
    {
        public Usuario? ValidarUsuario(string nombreUsuario, string contrasena)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ValidarUsuario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
            cmd.Parameters.AddWithValue("@Contrasena", contrasena);

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
                    TipoUsuario = reader.GetString(reader.GetOrdinal("TipoUsuario"))
                };
            }

            return null;
        }
    }
}
