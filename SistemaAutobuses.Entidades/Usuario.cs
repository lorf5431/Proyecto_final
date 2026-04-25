namespace SistemaAutobuses.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
    }
}
