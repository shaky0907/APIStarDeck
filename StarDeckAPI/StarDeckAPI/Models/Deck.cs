using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Deck
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public Boolean Estado { get; set; }
        public string Id_usuario { get; set; }

    }
}
