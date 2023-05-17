using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id_Deck), nameof(Id_Carta))]
    public class CartasXDeck
    {
        public string Id_Deck { get; set; }
        public string Id_Carta { get; set; }
    }
}
