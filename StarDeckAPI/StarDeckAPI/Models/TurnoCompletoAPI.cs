namespace StarDeckAPI.Models
{
    public class TurnoCompletoAPI
    {
        public TurnoAPI infoPartida { get; set; }
        public List<CartaAPI> cartasManoUsuario { get; set; }
        public List<CartaAPI> cartasDeckUsuario { get; set; }
        public List<CartaAPI> cartasManoRival { get; set; }
        public List<CartaAPI> cartasDeckRival { get; set; }
        public List<List<CartaAPI>> cartasPlanetas { get; set; }
        public List<List<CartaAPI>> cartasRivalPlanetas { get; set; }
        public List<PlanetaAPIGet> planetasEnPartida { get; set; }
        public UsuarioAPI rival { get; set; }

    }
}