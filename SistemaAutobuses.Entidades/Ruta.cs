namespace SistemaAutobuses.Entidades
{
    public class Ruta
    {
        public int Id { get; set; }
        public string NombreRuta { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public override string ToString() => NombreRuta;
    }
}
