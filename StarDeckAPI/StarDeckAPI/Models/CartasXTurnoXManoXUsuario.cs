using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{

        [PrimaryKey(nameof(Id_Carta), nameof(Id_Turno), nameof(Id_Usuario))]
        public class CartasXTurnoXManoXUsuario
        {
            public string Id_Carta { get; set; }
            public string Id_Turno { get; set; }
            public string Id_Usuario { get; set; }
        }
}
