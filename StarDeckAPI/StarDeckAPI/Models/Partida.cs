using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Partida
    {
        public string Id { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha_hora { get; set; }
    }
}
