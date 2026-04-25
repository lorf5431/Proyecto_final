using Microsoft.Data.SqlClient;
using SistemaAutobuses.Entidades;
using System.Data;

namespace SistemaAutobuses.Datos
{
    public class AsignacionRepository
    {
        public List<Asignacion> ObtenerTodas()
        {
            var lista = new List<Asignacion>();
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_ObtenerAsignaciones", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Asignacion
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    ChoferId = reader.GetInt32(reader.GetOrdinal("ChoferId")),
                    AutobusId = reader.GetInt32(reader.GetOrdinal("AutobusId")),
                    RutaId = reader.GetInt32(reader.GetOrdinal("RutaId")),
                    FechaAsignacion = reader.GetDateTime(reader.GetOrdinal("FechaAsignacion")),
                    Activa = reader.GetBoolean(reader.GetOrdinal("Activa")),
                    NombreChofer = reader.GetString(reader.GetOrdinal("NombreChofer")),
                    InfoAutobus = reader.GetString(reader.GetOrdinal("InfoAutobus")),
                    NombreRuta = reader.GetString(reader.GetOrdinal("NombreRuta"))
                });
            }
            return lista;
        }

        public void Insertar(Asignacion asignacion)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_InsertarAsignacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ChoferId", asignacion.ChoferId);
            cmd.Parameters.AddWithValue("@AutobusId", asignacion.AutobusId);
            cmd.Parameters.AddWithValue("@RutaId", asignacion.RutaId);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        public void Liberar(int id)
        {
            using SqlConnection conexion = ConexionDB.Instancia.ObtenerConexion();
            using SqlCommand cmd = new("sp_EliminarAsignacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
