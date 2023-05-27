using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    public class CartasXTurnoXManoXUsuario
    {
        [PrimaryKey(nameof(Id_Carta), nameof(Id_Turno), nameof(Id_Usuario))]
        public class CartasXTurnoXManoXUsuario
        {
            string Id_Carta { get; set; }
            string Id_Turno { get; set; }
            string Id_Usuario { get; set; }
        }
    }
}
}
