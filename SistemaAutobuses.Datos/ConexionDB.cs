using Microsoft.Data.SqlClient;

namespace SistemaAutobuses.Datos
{
    public class ConexionDB
    {
        private static ConexionDB? _instancia;
        private static readonly object _lock = new();
        private readonly string _cadenaConexion;

        private ConexionDB()
        {
            _cadenaConexion = System.Configuration.ConfigurationManager
                .ConnectionStrings["SistemaAutobuses"]?.ConnectionString
                ?? "Server=localhost;Database=SistemaAutobuses;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public static ConexionDB Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        _instancia ??= new ConexionDB();
                    }
                }
                return _instancia;
            }
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
