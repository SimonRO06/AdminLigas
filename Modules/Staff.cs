using Option;
using Team;

namespace Staff
{
    public class MedicalBodyObject
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string? Especialidad { get; set; } = string.Empty;
        public int EquipoId { get; set; }
        public TeamObject? Equipo { get; set; }
    }
    public class TecnicalBodyObject
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string? Cargo { get; set; } = string.Empty;  
        public int EquipoId { get; set; }
        public TeamObject? Equipo { get; set; }
    }
}