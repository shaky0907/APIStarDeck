namespace StarDeckAPI.Models
{
    public class MatchmakingResponse
    {
        public int estado;
        /*
         0: No ha encontrado
         1: El jugador 1 encontró al 2
         2: El jugador 2 descubrió que el 1 lo emparejó, ingresa a la partida
         3: El jugador 1 entra a la partida
         4: Esta en 1 o 2 pero ya paso el estado dependiendo de si es jugador 1 o 2 respectivamente
         */
        public string Id;
        public bool Id_Master;

    }
}
