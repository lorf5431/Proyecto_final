namespace SistemaAutobuses.Entidades
{
    public class Autobus
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Anio { get; set; }

        public string Descripcion => $"{Marca} {Modelo} ({Placa})";

        public override string ToString() => Descripcion;
    }
}
