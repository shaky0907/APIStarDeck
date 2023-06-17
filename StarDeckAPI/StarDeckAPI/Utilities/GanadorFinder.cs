using System.Text;
using System;
using StarDeckAPI.Models;
using StarDeckAPI.Data;

namespace StarDeckAPI.Utilities
{
    public static class GanadorFinder
    {
        public static UsuarioAPI getGanadorPlaneta(UsuarioAPI rival, UsuarioAPI jugador, int PuntosUsuario, int PuntosRival)
        {
            UsuarioAPI ganador = new UsuarioAPI();

            if (PuntosUsuario > PuntosRival)
            {
                ganador = jugador;
            }
            else if (PuntosUsuario < PuntosRival)
            {
                ganador = rival;
            }
            else
            {
                return null;
            }

            return ganador;
        }
        public static UsuarioAPI getGanadorPartidaCompleta(List<UsuarioAPI> ganadorPorPlaneta, UsuarioAPI jugador, UsuarioAPI rival)
        {
            UsuarioAPI ganador = null;
            int planetasGanadosJugador1 = 0;
            int planetasGanadosJugador2 = 0;

            foreach (UsuarioAPI usuarioGanadorDelPlaneta in ganadorPorPlaneta)
            {
                if (jugador == usuarioGanadorDelPlaneta)
                {
                    planetasGanadosJugador1++;
                }
                else if (rival == usuarioGanadorDelPlaneta)
                {
                    planetasGanadosJugador2++;
                }
            }
            
            if (planetasGanadosJugador1 > planetasGanadosJugador2)
            {
                ganador = jugador;
            }
            else if (planetasGanadosJugador1 < planetasGanadosJugador2)
            {
                ganador = rival;
            }
            else if (planetasGanadosJugador1 == planetasGanadosJugador2)
            {
                ganador = null;
            }

            return ganador;
        }
    }
}