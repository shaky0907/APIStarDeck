using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id_Carta), nameof(Id_Turno), nameof(Id_Usuario))]
    public class CartasXTurnoXDeckXUsuario
    {
        string Id_Carta { get; set; }
        string Id_Turno { get; set; }
	    string Id_Usuario { get; set; }
    }
}
