using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id_usuario), nameof(Id_carta))]
    public class CartaXUsuario
    {
  
        public string Id_usuario { get; set; }
 
        public string Id_carta { get; set; }
    }
}
