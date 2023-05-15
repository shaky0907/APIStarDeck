using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Estado_Partida
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
