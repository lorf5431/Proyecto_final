using SistemaAutobuses.Datos;
using SistemaAutobuses.Entidades;

namespace SistemaAutobuses.Negocio
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo = new();

        public Usuario? Autenticar(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es requerido.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña es requerida.");

            return _repo.ValidarUsuario(nombreUsuario, contrasena);
        }
    }
}
