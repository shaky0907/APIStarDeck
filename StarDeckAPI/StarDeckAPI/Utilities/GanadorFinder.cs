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
        public static UsuarioAPI getGanadorPartidaCompleta(List<UsuarioAPI> ganadorPorPlaneta)
        {
            UsuarioAPI ganador = null;
            UsuarioAPI jugador1 = new UsuarioAPI();
            UsuarioAPI jugador2 = new UsuarioAPI();
            int planetasGanadosJugador1 = 0;
            int planetasGanadosJugador2 = 0;

            foreach (UsuarioAPI jugador in ganadorPorPlaneta)
            {
                if (jugador1 != jugador)
                {
                    jugador1 = jugador;
                    planetasGanadosJugador1++;
                }
                else
                {
                    jugador2 = jugador;
                    planetasGanadosJugador2++;
                }
            }

            if (planetasGanadosJugador1 > planetasGanadosJugador2)
            {
                ganador = jugador1;
            }
            else if (planetasGanadosJugador1 < planetasGanadosJugador2)
            {
                ganador = jugador2;
            }
            else
            {
                return null;
            }

            return ganador;
        }

        public static UsuarioAPI getPerdedorPartidaCompleta(List<UsuarioAPI> ganadorPorPlaneta, UsuarioAPI jugador1, UsuarioAPI jugador2)
        {
            UsuarioAPI perdedor = null;
            int planetasGanadosJugador1 = 0;
            int planetasGanadosJugador2 = 0;

            foreach (UsuarioAPI jugador in ganadorPorPlaneta)
            {
                if (jugador1 == jugador)
                {
                    jugador1 = jugador;
                    planetasGanadosJugador1++;
                }
                else
                {
                    planetasGanadosJugador2++;
                }
            }

            if (planetasGanadosJugador1 > planetasGanadosJugador2)
            {
                perdedor = jugador2;
            }
            else if (planetasGanadosJugador1 < planetasGanadosJugador2)
            {
                perdedor = jugador1;
            }
            else
            {
                return null;
            }

            return perdedor;
        }
    }
}