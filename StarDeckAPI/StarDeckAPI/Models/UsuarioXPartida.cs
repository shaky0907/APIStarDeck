using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id_Usuario), nameof(Id_Partida))]
    public class UsuarioXPartida
    {
        public string Id_Usuario { get; set; }
        public string Id_Partida { get; set; }
        public Boolean Id_Master { get; set; }
        public Boolean Ganador { get; set; }
        public int Monedas_ingreso { get; set; }
        public int Monedas_apuesta { get; set; }
    }
}
