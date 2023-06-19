using Microsoft.EntityFrameworkCore;

namespace StarDeckAPI.Models
{
    [PrimaryKey(nameof(Id))]
    public class Parametros
    {
        public int Id { get; set; }
        public int Tiempo_turno { get; set; }
        public int Turnos_totales { get; set; }
        public int Cartas_Mano_Inicial { get; set; }
        public int Energia_Inicial { get; set; }

    }
}
