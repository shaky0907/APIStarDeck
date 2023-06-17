namespace StarDeckAPI.Models
{
    public class WinnerAPI
    {
        public List<UsuarioAPI> WinnerPerPlanet { get; set; }
        public UsuarioAPI Winner { get; set; }
        public List<int> PointsPerPlanet { get; set; }
        public List<int> PointsRivalPerPlanet { get; set; }
        public List<PlanetaAPIGet> PlanetsOnMatch { get; set; }
        public UsuarioAPI Loser { get; set; }
    }
}
