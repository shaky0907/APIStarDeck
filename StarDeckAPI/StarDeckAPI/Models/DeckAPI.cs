namespace StarDeckAPI.Models
{
    public class DeckAPI
    {
        public string Nombre { get; set; }
        public Boolean Estado { get; set; }
        public string Id_usuario { get; set; }
        public List<CartaAPI> Cartas { get; set; }
    }
}
