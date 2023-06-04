using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Planeta
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public string Descripcion { get; set; }
        public Boolean Estado { get; set; }
        public string Imagen { get; set; }
    }
}
