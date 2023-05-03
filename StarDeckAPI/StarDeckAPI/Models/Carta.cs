namespace StarDeckAPI.Models
{
    public class Carta
    {
        public string Id { get; set; }
        public string N_Personaje { get; set; }
        public int Energia { get; set; }
        public int C_batalla { get; set; }
        public string Imagen { get; set; }
        public int Raza { get; set; }
        public int Tipo { get; set; }
        public bool Activa { get; set; }
        public string Descripcion { get; set; }
    }
}
