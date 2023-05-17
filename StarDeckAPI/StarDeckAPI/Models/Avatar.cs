using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Avatar
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        
    }
}
