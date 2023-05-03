﻿namespace StarDeckAPI.Models
{
    public class UsuarioAPI
    {
        public string Id { get; set; }
        public bool Administrador { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        public string Nacionalidad { get; set; }
        public bool Estado { get; set; }
        public string Avatar { get; set; }
        public int Ranking { get; set; }
        public int Monedas { get; set; }
    }
}
