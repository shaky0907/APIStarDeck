using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{

    [PrimaryKey(nameof(Id_Planeta), nameof(Id_Partida))]
    public class PlanetasXPartida
    {
        public string Id_Planeta { get; set; }
        public string Id_Partida { get; set; }
    }
}
