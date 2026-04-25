using SistemaAutobuses.Datos;
using SistemaAutobuses.Entidades;

namespace SistemaAutobuses.Negocio
{
    public class ChoferService
    {
        private readonly ChoferRepository _repo = new();

        public List<Chofer> ObtenerTodos() => _repo.ObtenerTodos();

        public List<Chofer> ObtenerDisponibles() => _repo.ObtenerDisponibles();

        public void Guardar(Chofer chofer)
        {
            Validar(chofer);

            if (chofer.Id == 0)
                _repo.Insertar(chofer);
            else
                _repo.Actualizar(chofer);
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Debe seleccionar un chofer para eliminar.");

            _repo.Eliminar(id);
        }

        private static void Validar(Chofer chofer)
        {
            if (string.IsNullOrWhiteSpace(chofer.Nombre))
                throw new ArgumentException("El nombre del chofer es requerido.");

            if (string.IsNullOrWhiteSpace(chofer.Apellido))
                throw new ArgumentException("El apellido del chofer es requerido.");

            if (string.IsNullOrWhiteSpace(chofer.Cedula))
                throw new ArgumentException("La cédula del chofer es requerida.");

            if (chofer.FechaNacimiento >= DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento no es válida.");

            int edad = DateTime.Today.Year - chofer.FechaNacimiento.Year;
            if (chofer.FechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
            if (edad < 18)
                throw new ArgumentException("El chofer debe ser mayor de 18 años.");
        }
    }
}
