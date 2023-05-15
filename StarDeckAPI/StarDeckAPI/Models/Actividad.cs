using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Actividad
    {
        
        public int Id { get; set; }
        public string Nombre_act { get; set; }
    }
}
