using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id_Turno), nameof(Id_Planeta))]
    public class CartasXTurnoXPlanetaXUsuario
    {
        public string Id_Carta { get; set; }
        public string Id_Turno { get; set; }
        public string Id_Planeta { get; set; }
        public string Id_Usuario { get; set; }
    }
}
