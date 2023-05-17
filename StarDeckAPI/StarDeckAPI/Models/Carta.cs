using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Carta
    {
        public string Id { get; set; }
        public string N_Personaje { get; set; }
        public int Energia { get; set; }
        public int C_batalla { get; set; }
        public string Imagen { get; set; }
        public int Raza { get; set; }
        public int Tipo { get; set; }
        public Boolean Activa { get; set; }
        public string Descripcion { get; set; }
    }
}
