using Microsoft.Data.SqlClient;
using SistemaAutobuses.Entidades;
using System.Data;

namespace SistemaAutobuses.Datos
{
    public class RutaRepository : IRepository<Ruta>
    {
        public List<Ruta> ObtenerTodos()
        {
            var lista = new List<Ruta>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerRutas", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapearRuta(reader));
            }
            return lista;
        }

        public Ruta? ObtenerPorId(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerRutaPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            return reader.Read() ? MapearRuta(reader) : null;
        }

        public void Insertar(Ruta ruta)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_InsertarRuta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreRuta", ruta.NombreRuta);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)ruta.Descripcion ?? DBNull.Value);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Ruta ruta)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ActualizarRuta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", ruta.Id);
            cmd.Parameters.AddWithValue("@NombreRuta", ruta.NombreRuta);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)ruta.Descripcion ?? DBNull.Value);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_EliminarRuta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Ruta> ObtenerDisponibles()
        {
            var lista = new List<Ruta>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerRutasDisponibles", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapearRuta(reader));
            }
            return lista;
        }

        private static Ruta MapearRuta(SqlDataReader reader)
        {
            return new Ruta
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                NombreRuta = reader.GetString(reader.GetOrdinal("NombreRuta")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                    ? string.Empty
                    : reader.GetString(reader.GetOrdinal("Descripcion"))
            };
        }
    }
}
