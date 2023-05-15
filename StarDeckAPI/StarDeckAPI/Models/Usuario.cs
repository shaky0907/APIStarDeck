using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Usuario
    {
        public string Id { get; set; }
        public Boolean Administrador { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set;}
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        public int Nacionalidad { get; set; }
        public Boolean Estado { get; set; }
        public int Avatar { get; set; }
        public int Ranking { get; set; }
        public int Monedas { get; set; }
        public int Id_actividad { get; set; }

    }
}
