using SistemaAutobuses.Datos;
using SistemaAutobuses.Entidades;

namespace SistemaAutobuses.Negocio
{
    public class AutobusService
    {
        private readonly AutobusRepository _repo = new();

        public List<Autobus> ObtenerTodos() => _repo.ObtenerTodos();

        public List<Autobus> ObtenerDisponibles() => _repo.ObtenerDisponibles();

        public void Guardar(Autobus autobus)
        {
            Validar(autobus);

            if (autobus.Id == 0)
                _repo.Insertar(autobus);
            else
                _repo.Actualizar(autobus);
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar un autobús para eliminar.");

            _repo.Eliminar(id);
        }

        private static void Validar(Autobus autobus)
        {
            if (string.IsNullOrWhiteSpace(autobus.Marca))
                throw new ArgumentException("La marca del autobús es requerida.");

            if (string.IsNullOrWhiteSpace(autobus.Modelo))
                throw new ArgumentException("El modelo del autobús es requerido.");

            if (string.IsNullOrWhiteSpace(autobus.Placa))
                throw new ArgumentException("La placa del autobús es requerida.");

            if (string.IsNullOrWhiteSpace(autobus.Color))
                throw new ArgumentException("El color del autobús es requerido.");

            if (autobus.Anio < 1950 || autobus.Anio > DateTime.Today.Year + 1)
                throw new ArgumentException("El año del autobús no es válido.");
        }
    }
}
