using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Tipo_planeta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Probabilidad { get; set; }
    }
}
