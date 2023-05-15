using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Turno
    {
        public string Id { get; set; }
        public string Id_Partida { get; set; }
        public int Numero_turno { get; set; }
    }
}
