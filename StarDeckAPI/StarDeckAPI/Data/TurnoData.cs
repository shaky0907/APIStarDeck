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

        public List<string> getUserMano(string Id_usuario, string Id_turno)
        {
            List<CartasXTurnoXManoXUsuario> Cartas = this.apiDBContext.CartasXTurnoXManoXUsuario.ToList().Where(x => 
            (x.Id_Turno == Id_turno) && (x.Id_Usuario == Id_usuario)).ToList();
            List<string> cartas_Ids = new List<string>();

            foreach (CartasXTurnoXManoXUsuario carta in Cartas)
            {
                cartas_Ids.Add(carta.Id_Carta);
            }

            return cartas_Ids;
        }

        public List<string> getUserDeck(string Id_usuario, string Id_turno)
        {

            List<CartasXTurnoXDeckXUsuario> Cartas = this.apiDBContext.CartasXTurnoXDeckXUsuario.ToList().Where(x => (x.Id_Turno == Id_turno) && (x.Id_Usuario == Id_usuario)).ToList();
            List<string> cartas_Ids = new List<string>();

            foreach (CartasXTurnoXDeckXUsuario carta in Cartas)
            {
                cartas_Ids.Add(carta.Id_Carta);
            }
            return cartas_Ids;
        }

        public List<string> getPlanetaCartas(string Id_planeta,string Id_turno, string Id_usuario)
        {
            List<CartasXTurnoXPlanetaXUsuario> Cartas = this.apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(x =>
            (x.Id_Turno == Id_turno) && (x.Id_Planeta == Id_planeta) && (x.Id_Planeta == Id_planeta)).ToList();
            List<string> cartas_Ids = new List<string>();

            foreach (CartasXTurnoXPlanetaXUsuario carta in Cartas)
            {
                cartas_Ids.Add(carta.Id_Carta);
            }

            return cartas_Ids;
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
                Revela_primero = turnoApi.Revela_primero,
                Energia = turnoApi.Energia

            };

            apiDBContext.TurnoXUsuario.Add(turnoXUsuario);
            apiDBContext.SaveChanges();

            return turnoXUsuario;
        }

        public TurnoAPI getLastTurno(string Id_Partida, string Id_Usuario)
        {
            List<TurnoXUsuario> turnos = apiDBContext.TurnoXUsuario.ToList().Where(x =>
            (x.Id_Partida == Id_Partida) && (x.Id_Usuario == Id_Usuario)).ToList();
            int last_Turno = 1;
            TurnoXUsuario turnoLast = new TurnoXUsuario();
            foreach (TurnoXUsuario turno in turnos)
            {
                if (last_Turno >= turno.Numero_turno)
                {
                    last_Turno = turno.Numero_turno;
                    turnoLast = turno;
                }

            }

            TurnoAPI turnoReturn = new TurnoAPI()
            {
                Id_Partida = Id_Partida,
                Id_Usuario = Id_Usuario,
                Numero_turno = last_Turno,
                Energia = turnoLast.Energia,
                Revela_primero = turnoLast.Revela_primero

            };

            return turnoReturn;
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
                Revela_primero = turnoXUsuario.Revela_primero

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
                turno.Revela_primero = turnoApi.Revela_primero;
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
