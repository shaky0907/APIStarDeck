using Microsoft.EntityFrameworkCore;
using StarDeckAPI.Models;

namespace StarDeckAPI.Data
{
    public class APIDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Avatar> Avatar { get; set; }
        public DbSet<Carta> Carta { get; set; }
        public DbSet<CartasXDeck> CartasXDeck { get; set; }
        public DbSet<CartasXTurnoXPlanetaXUsuario> CartasXTurnoXPlanetaXUsuario { get; set; }
        public DbSet<CartaXUsuario> CartaXUsuario { get; set; }
        public DbSet<Deck> Deck { get; set; }
        public DbSet<Estado_Partida> Estado_Partida { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Partida> Partida { get; set; }
        public DbSet<Planeta> Planeta { get; set; }
        public DbSet<PlanetasXPartida> PlanetasXPartida { get; set; }
        public DbSet<Raza> Raza { get; set; }
        public DbSet<Tipo_planeta> Tipo_planeta { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<TurnoXUsuario> TurnoXUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioXPartida> UsuarioXPartida { get; set; }
        public DbSet<CartasXTurnoXDeckXUsuario> CartasXTurnoXDeckXUsuario { get; set; }
        public DbSet<CartasXTurnoXManoXUsuario> CartasXTurnoXManoXUsuario { get; set; }
        public DbSet<Parametros> Parametros { get; set; }

    }
}