namespace SistemaAutobuses.Entidades
{
    public class Asignacion
    {
        public int Id { get; set; }
        public int ChoferId { get; set; }
        public int AutobusId { get; set; }
        public int RutaId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public bool Activa { get; set; }

        public string NombreChofer { get; set; } = string.Empty;
        public string InfoAutobus { get; set; } = string.Empty;
        public string NombreRuta { get; set; } = string.Empty;
    }
}
