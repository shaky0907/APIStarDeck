namespace StarDeckAPI.Models
{
    public class DeckAPI
    {
        public string Nombre { get; set; }
        public Boolean Estado { get; set; }
        public int Slot { get; set; }
        public string Id_usuario { get; set; }
<<<<<<< HEAD
        public List<CartaAPI> Cartas { get; set; }
=======
        public List<string> id_cartas { get; set; }
>>>>>>> ee2203c1d2b088f89d16eff8400ba1cc64413834
    }
}
