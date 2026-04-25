using SistemaAutobuses.Datos;
using SistemaAutobuses.Entidades;

namespace SistemaAutobuses.Negocio
{
    public class AsignacionService
    {
        private readonly AsignacionRepository _repo = new();
        private readonly ChoferRepository _choferRepo = new();
        private readonly AutobusRepository _autobusRepo = new();
        private readonly RutaRepository _rutaRepo = new();

        public List<Asignacion> ObtenerTodas() => _repo.ObtenerTodas();

        public List<Chofer> ObtenerChoferesDisponibles() => _choferRepo.ObtenerDisponibles();

        public List<Autobus> ObtenerAutobusesDisponibles() => _autobusRepo.ObtenerDisponibles();

        public List<Ruta> ObtenerRutasDisponibles() => _rutaRepo.ObtenerDisponibles();

        public void Asignar(Asignacion asignacion)
        {
            if (asignacion.ChoferId <= 0)
                throw new ArgumentException("Debe seleccionar un chofer.");

            if (asignacion.AutobusId <= 0)
                throw new ArgumentException("Debe seleccionar un autobús.");

            if (asignacion.RutaId <= 0)
                throw new ArgumentException("Debe seleccionar una ruta.");

            _repo.Insertar(asignacion);
        }

        public void Liberar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar una asignación para liberar.");

            _repo.Liberar(id);
        }
    }
}
