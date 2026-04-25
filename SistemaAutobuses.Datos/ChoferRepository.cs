using Microsoft.Data.SqlClient;
using SistemaAutobuses.Entidades;
using System.Data;

namespace SistemaAutobuses.Datos
{
    public class ChoferRepository : IRepository<Chofer>
    {
        public List<Chofer> ObtenerTodos()
        {
            var lista = new List<Chofer>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerChoferes", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapearChofer(reader));
            }
            return lista;
        }

        public Chofer? ObtenerPorId(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerChoferPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            return reader.Read() ? MapearChofer(reader) : null;
        }

        public void Insertar(Chofer chofer)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_InsertarChofer", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombre", chofer.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", chofer.Apellido);
            cmd.Parameters.AddWithValue("@FechaNacimiento", chofer.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Cedula", chofer.Cedula);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Chofer chofer)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ActualizarChofer", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", chofer.Id);
            cmd.Parameters.AddWithValue("@Nombre", chofer.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", chofer.Apellido);
            cmd.Parameters.AddWithValue("@FechaNacimiento", chofer.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Cedula", chofer.Cedula);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_EliminarChofer", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Chofer> ObtenerDisponibles()
        {
            var lista = new List<Chofer>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerChoferesDisponibles", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapearChofer(reader));
            }
            return lista;
        }

        private static Chofer MapearChofer(SqlDataReader reader)
        {
            return new Chofer
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
                Cedula = reader.GetString(reader.GetOrdinal("Cedula"))
            };
        }
    }
}
