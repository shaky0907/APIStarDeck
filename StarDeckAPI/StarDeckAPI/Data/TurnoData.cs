using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Data
{
    public class TurnoData
    {
        private APIDbContext apiDBContext;
        public TurnoData(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        public List<CartaAPI> getUserMano(string Id_usuario, string Id_turno)
        {
            List<CartasXTurnoXManoXUsuario> Cartas = this.apiDBContext.CartasXTurnoXManoXUsuario.ToList().Where(x =>
            (x.Id_Turno == Id_turno) && (x.Id_Usuario == Id_usuario)).ToList();
            List<string> cartas_Ids = new List<string>();
            List<CartaAPI> cartas = new List<CartaAPI>();
            CartaData cartaData = new CartaData(this.apiDBContext);

            foreach (CartasXTurnoXManoXUsuario carta in Cartas)
            {
                cartas_Ids.Add(carta.Id_Carta);
            }

            foreach (string id in cartas_Ids)
            {
                cartas.Add(cartaData.getCartaDB(id));
            }

            return cartas;
        }

        public List<CartaAPI> getUserDeck(string Id_usuario, string Id_turno)
        {

            List<CartasXTurnoXDeckXUsuario> Cartas = this.apiDBContext.CartasXTurnoXDeckXUsuario.ToList().Where(x => (x.Id_Turno == Id_turno) && (x.Id_Usuario == Id_usuario)).ToList();
            List<string> cartas_Ids = new List<string>();
            List<CartaAPI> cartas = new List<CartaAPI>();
            CartaData cartaData = new CartaData(this.apiDBContext);

            foreach (CartasXTurnoXDeckXUsuario carta in Cartas)
            {
                cartas_Ids.Add(carta.Id_Carta);
            }

            foreach (string id in cartas_Ids)
            {
                cartas.Add(cartaData.getCartaDB(id));
            }

            return cartas;

        }

        public List<CartaAPI> getPlanetaCartas(string Id_planeta, string Id_turno, string Id_usuario)
        {
            List<CartasXTurnoXPlanetaXUsuario> Cartas = this.apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(x =>
            (x.Id_Turno == Id_turno) && (x.Id_Planeta == Id_planeta) && (x.Id_Planeta == Id_planeta)).ToList();
            List<string> cartas_Ids = new List<string>();
            List<CartaAPI> cartas = new List<CartaAPI>();
            CartaData cartaData = new CartaData(this.apiDBContext);

            foreach (CartasXTurnoXPlanetaXUsuario carta in Cartas)
            {
                cartas_Ids.Add(carta.Id_Carta);
            }

            foreach (string id in cartas_Ids)
            {
                cartas.Add(cartaData.getCartaDB(id));
            }

            return cartas;
        }
        
        public List<CartaAPI> getPlanetaCartasPartida(string Id_planeta, string Id_partida, string Id_usuario)
        {
            List<TurnoXUsuario> Turnos = this.apiDBContext.TurnoXUsuario.ToList().Where(x => 
            (x.Id_Partida == Id_partida) && (x.Id_Usuario == Id_usuario)).ToList();

            List<CartaAPI> cartas = new List<CartaAPI>();
            List<string> cartas_Ids = new List<string>();
            CartaData cartaData = new CartaData(this.apiDBContext);

            foreach (TurnoXUsuario turno in Turnos)
            {
                List<CartasXTurnoXPlanetaXUsuario> Cartas = this.apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(x =>
                (x.Id_Turno == turno.Id) && (x.Id_Planeta == Id_planeta) && (x.Id_Planeta == Id_planeta)).ToList();

                foreach (CartasXTurnoXPlanetaXUsuario carta in Cartas)
                {
                    cartas_Ids.Add(carta.Id_Carta);
                }
            }
            
            foreach (string id in cartas_Ids)
            {
                cartas.Add(cartaData.getCartaDB(id));
            }

            return cartas;
        }

        public UsuarioAPI getGanadorPlanetaPartida(string Id_planeta, string Id_partida, string Id_usuario, string Id_rival)
        {
            List<TurnoXUsuario> Turnos = this.apiDBContext.TurnoXUsuario.ToList().Where(x =>
            (x.Id_Partida == Id_partida) && (x.Id_Usuario == Id_usuario)).ToList();
            List<TurnoXUsuario> Turnos_Rival = this.apiDBContext.TurnoXUsuario.ToList().Where(x =>
            (x.Id_Partida == Id_partida) && (x.Id_Usuario == Id_rival)).ToList();

            List<CartaAPI> cartas = new List<CartaAPI>();
            List<string> cartas_Ids = new List<string>();
            List<CartaAPI> cartas_Rival = new List<CartaAPI>();
            List<string> cartas_Ids_Rival = new List<string>();
            int puntos_Planeta = 0;
            int puntos_Planeta_Rival = 0;
            UsuarioAPI usuario = new UsuarioAPI();

            CartaData cartaData = new CartaData(this.apiDBContext);
            UsuarioData usuarioData = new UsuarioData(this.apiDBContext);

            //Sacar los puntos del usuario
            foreach (TurnoXUsuario turno in Turnos)
            {
                List<CartasXTurnoXPlanetaXUsuario> Cartas = this.apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(x =>
                (x.Id_Turno == turno.Id) && (x.Id_Planeta == Id_planeta) && (x.Id_Planeta == Id_planeta)).ToList();

                foreach (CartasXTurnoXPlanetaXUsuario carta in Cartas)
                {
                    cartas_Ids.Add(carta.Id_Carta);
                }
            }
            foreach (string id in cartas_Ids)
            {
                cartas.Add(cartaData.getCartaDB(id));
            }
            foreach (CartaAPI carta in cartas)
            {
                puntos_Planeta += carta.Costo;
            }

            //Sacar los puntos del rival
            foreach (TurnoXUsuario turno in Turnos_Rival)
            {
                List<CartasXTurnoXPlanetaXUsuario> Cartas = this.apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(x =>
                (x.Id_Turno == turno.Id) && (x.Id_Planeta == Id_planeta) && (x.Id_Planeta == Id_planeta)).ToList();

                foreach (CartasXTurnoXPlanetaXUsuario carta in Cartas)
                {
                    cartas_Ids_Rival.Add(carta.Id_Carta);
                }
            }
            foreach (string id in cartas_Ids_Rival)
            {
                cartas_Rival.Add(cartaData.getCartaDB(id));
            }
            foreach (CartaAPI carta in cartas_Rival)
            {
                puntos_Planeta_Rival += carta.Costo;
            }

            //Sacar el ganador y mandar el usuario ganador
            if (puntos_Planeta > puntos_Planeta_Rival)
            {
                usuario = usuarioData.getUsuario(Id_usuario);
            } else if (puntos_Planeta < puntos_Planeta_Rival)
            {
                usuario = usuarioData.getUsuario(Id_rival);
            } else
            {
                usuario = null;
            }


            return usuario;
        }

        public CartasXTurnoXManoXUsuario addCartaMano(CartasXTurnoXManoXUsuario cartasxturnoxmanoxusuario)
        {


            apiDBContext.CartasXTurnoXManoXUsuario.Add(cartasxturnoxmanoxusuario);

            apiDBContext.SaveChanges();

            return cartasxturnoxmanoxusuario;

        }

        public CartasXTurnoXDeckXUsuario AddCartaDeck(CartasXTurnoXDeckXUsuario cartasxturnoxdeckxusuario)
        {


            apiDBContext.CartasXTurnoXDeckXUsuario.Add(cartasxturnoxdeckxusuario);

            apiDBContext.SaveChanges();

            return cartasxturnoxdeckxusuario;

        }

        

        public CartasXTurnoXPlanetaXUsuario AddCartaPlaneta(CartasXTurnoXPlanetaXUsuario cartasXTurnoXPlanetaXUsuario)
        {


            apiDBContext.CartasXTurnoXPlanetaXUsuario.Add(cartasXTurnoXPlanetaXUsuario);
            apiDBContext.SaveChanges();

            return cartasXTurnoXPlanetaXUsuario;

        }

        public CartasXTurnoXManoXUsuario deleteCartaMano(string Id_Usuario,string Id_Turno, string Id_Carta)
        {
            CartasXTurnoXManoXUsuario cxtxmxu = apiDBContext.CartasXTurnoXManoXUsuario.ToList().Where(
                x => (x.Id_Usuario == Id_Usuario) && (x.Id_Turno == Id_Turno) && (x.Id_Carta == Id_Carta)).First();

            if (cxtxmxu != null)
            {
                apiDBContext.CartasXTurnoXManoXUsuario.Remove(cxtxmxu);
                apiDBContext.SaveChanges();
            }
            else
            {
                cxtxmxu = new CartasXTurnoXManoXUsuario();
            }
            return cxtxmxu;
        }

        public CartasXTurnoXDeckXUsuario deleteCartaDeck(string Id_Usuario, string Id_Turno, string Id_Carta)
        {
            CartasXTurnoXDeckXUsuario cxtxdxu = apiDBContext.CartasXTurnoXDeckXUsuario.ToList().Where(
                x => (x.Id_Usuario == Id_Usuario) && (x.Id_Turno == Id_Turno) && (x.Id_Carta == Id_Carta)).First();

            if (cxtxdxu != null)
            {
                apiDBContext.CartasXTurnoXDeckXUsuario.Remove(cxtxdxu);
                apiDBContext.SaveChanges();
            }
            else
            {
                cxtxdxu = new CartasXTurnoXDeckXUsuario();
            }
            return cxtxdxu;
        }

        public CartasXTurnoXPlanetaXUsuario deleteCartaPlaneta(string Id_Usuario, string Id_Turno, string Id_Carta)
        {
            CartasXTurnoXPlanetaXUsuario cxtxpxu = apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(
                x => (x.Id_Usuario == Id_Usuario) && (x.Id_Turno == Id_Turno) && (x.Id_Carta == Id_Carta)).First();

            if (cxtxpxu != null)
            {
                apiDBContext.CartasXTurnoXPlanetaXUsuario.Remove(cxtxpxu);
                apiDBContext.SaveChanges();
            }
            else
            {
                cxtxpxu = new CartasXTurnoXPlanetaXUsuario();
            }
            return cxtxpxu;
        }

        public TurnoXUsuario addTurno(TurnoAPI turnoApi)
        {
            string id = GeneratorID.GenerateRandomId("T-");
            TurnoXUsuario turnoXUsuario = new TurnoXUsuario()
            {
                Id = id,
                Id_Partida = turnoApi.Id_Partida,
                Id_Usuario = turnoApi.Id_Usuario,
                Numero_turno = turnoApi.Numero_turno,
                Terminado = turnoApi.Terminado,
                Energia = turnoApi.Energia

            };

            apiDBContext.TurnoXUsuario.Add(turnoXUsuario);
            apiDBContext.SaveChanges();

            return turnoXUsuario;
        }

        public TurnoXUsuario getLastTurno(string Id_Partida, string Id_Usuario)
        {
            List<TurnoXUsuario> turnos = apiDBContext.TurnoXUsuario.ToList().Where(x =>
            (x.Id_Partida == Id_Partida) && (x.Id_Usuario == Id_Usuario)).ToList();
            int last_Turno = 1;
            TurnoXUsuario turnoLast = new TurnoXUsuario();
            foreach (TurnoXUsuario turno in turnos)
            {
                if (last_Turno <= turno.Numero_turno)
                {
                    last_Turno = turno.Numero_turno;
                    turnoLast = turno;
                }

            }

            return turnoLast;
        }

        public TurnoXUsuario getLastTurnoNumero(string Id_Partida, string Id_Usuario, int numero)
        {
            List<TurnoXUsuario> turnos = apiDBContext.TurnoXUsuario.ToList().Where(x =>
            (x.Id_Partida == Id_Partida) && (x.Id_Usuario == Id_Usuario) && (x.Numero_turno == numero)).ToList();
            int last_Turno = 1;
            TurnoXUsuario turnoLast = new TurnoXUsuario();
            foreach (TurnoXUsuario turno in turnos)
            {
                if (last_Turno <= turno.Numero_turno)
                {
                    last_Turno = turno.Numero_turno;
                    turnoLast = turno;
                }

            }

            return turnoLast;
        }

        public TurnoAPI getTurno(string Id)
        {
            TurnoXUsuario turnoXUsuario = apiDBContext.TurnoXUsuario.ToList().Where(x => x.Id == Id).First();
            TurnoAPI turnoReturn = new TurnoAPI()
            {
                Id_Partida = turnoXUsuario.Id_Partida,
                Id_Usuario = turnoXUsuario.Id_Usuario,
                Numero_turno = turnoXUsuario.Numero_turno,
                Energia = turnoXUsuario.Energia,
                Terminado = turnoXUsuario.Terminado

            };
            return turnoReturn;
        }

        public TurnoXUsuario actualizarTurno(string Id, TurnoAPI turnoApi)
        {
            TurnoXUsuario turno = apiDBContext.TurnoXUsuario.ToList().Where(x => x.Id == Id).First();
            if (turno != null)
            {

                turno.Numero_turno = turnoApi.Numero_turno;
                turno.Id_Partida = turnoApi.Id_Partida;
                turno.Id_Usuario = turnoApi.Id_Usuario;
                turno.Terminado = turnoApi.Terminado;
                turno.Energia = turnoApi.Energia;

                apiDBContext.TurnoXUsuario.Update(turno);
                apiDBContext.SaveChanges();
            }
            else
            {
                turno = new TurnoXUsuario();
            }

            return turno;
        }

        public TurnoXUsuario actualizarEnergia(string Id, string Id_Usuario,int energia)
        {
            TurnoXUsuario turno = apiDBContext.TurnoXUsuario.ToList().Where(x => (x.Id == Id) && (x.Id_Usuario == Id_Usuario)).First();
            if (turno != null)
            {
                turno.Energia = energia;

                apiDBContext.TurnoXUsuario.Update(turno);
                apiDBContext.SaveChanges();
            }
            else
            {
                turno = new TurnoXUsuario();
            }

            return turno;
        }
    }
}
