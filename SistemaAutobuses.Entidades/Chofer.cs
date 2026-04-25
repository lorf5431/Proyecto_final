namespace SistemaAutobuses.Entidades
{
    public class Chofer
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Cedula { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public override string ToString() => NombreCompleto;
    }
}
