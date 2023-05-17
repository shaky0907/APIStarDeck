using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id_turno), nameof(Id_Usuario))]
    public class TurnoXUsuario
    {
        public string Id_turno { get; set; }
        public string Id_Usuario { get; set; }
        public int Energia_inicial { get; set; }
        public int Energia_gastada { get; set; }
        public Boolean Revela_primero { get; set; }
    }
}
