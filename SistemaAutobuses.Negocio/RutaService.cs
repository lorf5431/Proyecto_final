using SistemaAutobuses.Datos;
using SistemaAutobuses.Entidades;

namespace SistemaAutobuses.Negocio
{
    public class RutaService
    {
        private readonly RutaRepository _repo = new();

        public List<Ruta> ObtenerTodos() => _repo.ObtenerTodos();

        public List<Ruta> ObtenerDisponibles() => _repo.ObtenerDisponibles();

        public void Guardar(Ruta ruta)
        {
            Validar(ruta);

            if (ruta.Id == 0)
                _repo.Insertar(ruta);
            else
                _repo.Actualizar(ruta);
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar una ruta para eliminar.");

            _repo.Eliminar(id);
        }

        private static void Validar(Ruta ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta.NombreRuta))
                throw new ArgumentException("El nombre de la ruta es requerido.");
        }
    }
}
