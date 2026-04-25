using Microsoft.Data.SqlClient;
using SistemaAutobuses.Entidades;
using System.Data;

namespace SistemaAutobuses.Datos
{
    public class AutobusRepository : IRepository<Autobus>
    {
        public List<Autobus> ObtenerTodos()
        {
            var lista = new List<Autobus>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerAutobuses", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapearAutobus(reader));
            }
            return lista;
        }

        public Autobus? ObtenerPorId(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerAutobusPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            return reader.Read() ? MapearAutobus(reader) : null;
        }

        public void Insertar(Autobus autobus)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_InsertarAutobus", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Marca", autobus.Marca);
            cmd.Parameters.AddWithValue("@Modelo", autobus.Modelo);
            cmd.Parameters.AddWithValue("@Placa", autobus.Placa);
            cmd.Parameters.AddWithValue("@Color", autobus.Color);
            cmd.Parameters.AddWithValue("@Anio", autobus.Anio);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Autobus autobus)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ActualizarAutobus", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", autobus.Id);
            cmd.Parameters.AddWithValue("@Marca", autobus.Marca);
            cmd.Parameters.AddWithValue("@Modelo", autobus.Modelo);
            cmd.Parameters.AddWithValue("@Placa", autobus.Placa);
            cmd.Parameters.AddWithValue("@Color", autobus.Color);
            cmd.Parameters.AddWithValue("@Anio", autobus.Anio);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_EliminarAutobus", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Autobus> ObtenerDisponibles()
        {
            var lista = new List<Autobus>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerAutobusesDisponibles", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapearAutobus(reader));
            }
            return lista;
        }

        private static Autobus MapearAutobus(SqlDataReader reader)
        {
            return new Autobus
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Marca = reader.GetString(reader.GetOrdinal("Marca")),
                Modelo = reader.GetString(reader.GetOrdinal("Modelo")),
                Placa = reader.GetString(reader.GetOrdinal("Placa")),
                Color = reader.GetString(reader.GetOrdinal("Color")),
                Anio = reader.GetInt32(reader.GetOrdinal("Anio"))
            };
        }
    }
}
