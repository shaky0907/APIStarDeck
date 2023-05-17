using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Deck
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public Boolean Estado { get; set; }
<<<<<<< HEAD
=======
        public int Slot { get; set; }
>>>>>>> ee2203c1d2b088f89d16eff8400ba1cc64413834
        public string Id_usuario { get; set; }

    }
}
