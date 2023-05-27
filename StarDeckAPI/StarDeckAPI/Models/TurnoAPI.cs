namespace StarDeckAPI.Models
{
    public class TurnoAPI
    {
        public string Id_Partida { get; set; }
        public int Numero_turno { get; set; }
        public string Id_Usuario { get; set; }
        public int Energia_inicial { get; set; }
        public int Energia_gastada { get; set; }
        public Boolean Revela_primero { get; set; }
        public List<CartaXPlanetaAPI> cartasXPlaneta { get; set; }
        public List<string> cartaXDeck { get; set; }
        public List<string> cartaXMano { get; set; }


    }
}
