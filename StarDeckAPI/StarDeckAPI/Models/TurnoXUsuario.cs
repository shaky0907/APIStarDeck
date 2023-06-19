using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class TurnoXUsuario
    {
        public string Id { get; set; }
        public string Id_Partida { get; set; }
        public int Numero_turno { get; set; }
        public string Id_Usuario { get; set; }
        public int Energia { get; set; }
        public Boolean Terminado { get; set; }
    }
}
