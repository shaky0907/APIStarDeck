using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using StarDeckAPI.Controllers;
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

        private int getPuntosDePlaneta(Planeta planeta, string Id_turno, string Id_usuario)
        {
            int puntosPlaneta = 0;

            List<CartaAPI> cartas = new List<CartaAPI>();
            List<string> cartas_Ids = new List<string>();
            CartaData cartaData = new CartaData(this.apiDBContext);
            List<CartasXTurnoXPlanetaXUsuario> Cartas = this.apiDBContext.CartasXTurnoXPlanetaXUsuario.ToList().Where(x =>
                        (x.Id_Turno == Id_turno) && (x.Id_Planeta == planeta.Id) && (x.Id_Usuario == Id_usuario)).ToList();

            foreach (CartasXTurnoXPlanetaXUsuario carta in Cartas)
            {
                puntosPlaneta += cartaData.getCartaDB(carta.Id_Carta).Costo;
            }

            return puntosPlaneta;
        }

        public WinnerAPI getGanadorPartida(string Id_partida, string Id_usuario)
        {
            WinnerAPI winnerAPI = new WinnerAPI();
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            UsuarioData usuarioData = new UsuarioData(this.apiDBContext);
            PlanetaData planetaData = new PlanetaData(this.apiDBContext);

            List<Planeta> planetas = matchmakingData.getPlanetasPartida(Id_partida);

            Usuario rival_ = matchmakingData.getRival(Id_usuario, Id_partida);
            UsuarioAPI rival = usuarioData.getUsuario(rival_.Id);

            UsuarioAPI jugador = usuarioData.getUsuario(Id_usuario);

            TurnoXUsuario turno = this.getLastTurno(Id_partida, Id_usuario);
            TurnoXUsuario turno_rival = this.getLastTurno(Id_partida, rival.Id);

            List<UsuarioAPI> ganadorPorPlaneta = new List<UsuarioAPI>();

            winnerAPI.PointsPerPlanet = new List<int>();
            winnerAPI.PointsRivalPerPlanet = new List<int>();

            winnerAPI.PlanetsOnMatch = new List<PlanetaAPIGet>();

            foreach (Planeta planeta in planetas)
            {
                //Sacar los puntos del usuario
                int PuntosUsuario = this.getPuntosDePlaneta(planeta, turno.Id, Id_usuario);
                winnerAPI.PointsPerPlanet.Add(PuntosUsuario);

                //Sacar los puntos del rival
                int PuntosRival = this.getPuntosDePlaneta(planeta,turno_rival.Id, rival.Id);
                winnerAPI.PointsRivalPerPlanet.Add(PuntosRival);

                //Sacar el ganador teniendo los puntos del planeta
                ganadorPorPlaneta.Add(GanadorFinder.getGanadorPlaneta(rival, jugador, PuntosUsuario, PuntosRival));

                winnerAPI.PlanetsOnMatch.Add(planetaData.getPlaneta(planeta.Id));
            }

            //Settear los ganadores por planeta
            winnerAPI.WinnerPerPlanet = ganadorPorPlaneta;

            //Sacar el ganador de la partida en si
            winnerAPI.Winner = GanadorFinder.getGanadorPartidaCompleta(ganadorPorPlaneta);

            //Sacar el perdedor de la partida en si
            winnerAPI.Loser = GanadorFinder.getPerdedorPartidaCompleta(ganadorPorPlaneta, jugador, rival);

            return winnerAPI;
        }

        public TurnoCompletoAPI getInfoCompletaUltimoTurno(string Id_partida, string Id_usuario)
        {
            TurnoCompletoAPI turnoCompleto = new TurnoCompletoAPI();
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            UsuarioData usuarioData = new UsuarioData(this.apiDBContext);
            PlanetaData planetaData = new PlanetaData(this.apiDBContext);

            List<Planeta> planetas = matchmakingData.getPlanetasPartida(Id_partida);
            List<PlanetaAPIGet> planetasEnPartida = new List<PlanetaAPIGet>();

            string id_turno = this.getLastTurno(Id_partida, Id_usuario).Id;
            turnoCompleto.infoPartida = this.getTurno(id_turno);

            string rival_id = matchmakingData.getRival(Id_usuario, Id_partida).Id;
            UsuarioAPI rival = usuarioData.getUsuario(rival_id);
            string id_turno_rival = this.getLastTurno(Id_partida, rival.Id).Id;
            turnoCompleto.rival = rival;

            //Obtener todas las cartas de la mano y mazo tanto para el jugador como el rival
            turnoCompleto.cartasManoUsuario = this.getUserMano(Id_usuario, id_turno);
            turnoCompleto.cartasDeckUsuario = this.getUserDeck(Id_usuario, id_turno);
            turnoCompleto.cartasManoRival = this.getUserMano(rival.Id, id_turno_rival);
            turnoCompleto.cartasDeckRival = this.getUserDeck(rival.Id, id_turno_rival);


            //Obtener todas las cartas por planeta y planetas en si
            turnoCompleto.cartasPlanetas = new List<List<CartaAPI>>();
            turnoCompleto.cartasRivalPlanetas = new List<List<CartaAPI>>();

            int contador = 0;
            foreach (Planeta planeta in planetas)
            {
                turnoCompleto.cartasPlanetas.Add(this.getPlanetaCartas(planeta.Id, id_turno, Id_usuario));
                turnoCompleto.cartasRivalPlanetas.Add(this.getPlanetaCartas(planeta.Id, id_turno_rival, rival.Id));

                planetasEnPartida.Add(planetaData.getPlaneta(planeta.Id));

                contador++;
            }

            turnoCompleto.planetasEnPartida = planetasEnPartida;

            return turnoCompleto;
        }

        public TurnoCompletoAPI updateInfoCompletaTurno(string Id_partida, string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            TurnoCompletoAPI turnoCompletoUpdated = new TurnoCompletoAPI();
            
            //Se actualiza el turno
            string id_turno = this.getLastTurno(Id_partida, Id_usuario).Id;
            this.actualizarTurno(id_turno, turnoCompleto.infoPartida);

            //Se agarra toda la informacion del turno
            turnoCompletoUpdated = this.getInfoCompletaUltimoTurno(Id_partida, Id_usuario);

            return turnoCompletoUpdated;
        }

        public TurnoCompletoAPI CrearNuevoTurnoCompleto(string Id_partida, string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            TurnoCompletoAPI turnoCompletoNuevo = new TurnoCompletoAPI();
            TurnoAPI turnoNuevo = new TurnoAPI();
            TurnoXUsuario turnoCreado = new TurnoXUsuario();
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            CartasXTurnoXManoXUsuario cartaMano = new CartasXTurnoXManoXUsuario();
            CartasXTurnoXDeckXUsuario cartaDeck = new CartasXTurnoXDeckXUsuario();
            CartasXTurnoXPlanetaXUsuario cartaPlaneta = new CartasXTurnoXPlanetaXUsuario();

            //Se le sobreescribe la informacion del turno viejo al nuevo
            turnoCompletoNuevo = turnoCompleto;

            //Se crea un nuevo turno
            turnoNuevo = turnoCompleto.infoPartida;
            turnoNuevo.Terminado = false;
            turnoNuevo.Numero_turno += 1;
            turnoNuevo.Energia += 30;

            turnoCreado = this.addTurno(turnoNuevo);
            turnoNuevo = this.getTurno(turnoCreado.Id);

            turnoCompletoNuevo.infoPartida = turnoNuevo;

            //Se agarra una de las cartas del mazo y pasa a la mano
            turnoCompletoNuevo.cartasManoUsuario.Add(turnoCompletoNuevo.cartasDeckUsuario[0]);
            turnoCompletoNuevo.cartasDeckUsuario.RemoveAt(0);

            //Se actualiza el mazo y mano del usuario para el nuevo turno
            int contador = 0;
            foreach (CartaAPI carta in turnoCompleto.cartasManoUsuario)
            {
                cartaMano.Id_Carta = carta.Id;
                cartaMano.Id_Turno = turnoCreado.Id;
                cartaMano.Id_Usuario = Id_usuario;
                cartaMano.Posicion = contador;

                this.addCartaMano(cartaMano);

                contador++;
            }
            contador = 0;
            foreach (CartaAPI carta in turnoCompleto.cartasDeckUsuario)
            {
                cartaDeck.Id_Carta = carta.Id;
                cartaDeck.Id_Turno = turnoCreado.Id;
                cartaDeck.Id_Usuario = Id_usuario;
                cartaDeck.Posicion = contador;

                this.AddCartaDeck(cartaDeck);

                contador++;
            }

            //Se actualizan todas las cartas de los planetas
            contador = 0;
            List<Planeta> planetas = matchmakingData.getPlanetasPartida(Id_partida);
            
            foreach (List<CartaAPI> listaCartas in turnoCompleto.cartasPlanetas)
            {
                List<CartaAPI> cartasExistentesEnPlaneta = this.getPlanetaCartasPartida(planetas[contador].Id, Id_partida, Id_usuario);

                foreach (CartaAPI carta in listaCartas)
                {
                    if (!cartasExistentesEnPlaneta.Contains(carta))
                    {
                        cartaPlaneta.Id_Turno = turnoCreado.Id;
                        cartaPlaneta.Id_Carta = carta.Id;
                        cartaPlaneta.Id_Planeta = planetas[contador].Id;
                        cartaPlaneta.Id_Usuario = Id_usuario;

                        this.AddCartaPlaneta(cartaPlaneta);
                    }
                }
                contador++;
            }

            //Se updatea la informacion del turno para updatear la energia, numero de turno y estado
            turnoCompletoNuevo = this.updateInfoCompletaTurno(Id_partida, Id_usuario, turnoCompletoNuevo);

            return turnoCompletoNuevo;
        }

        public TurnoCompletoAPI AddTurnoCompleto(string Id_partida, string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            TurnoCompletoAPI turnoCompletoNuevo = new TurnoCompletoAPI();
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            ColeccionData coleccionData = new ColeccionData(this.apiDBContext);

            TurnoAPI turnoNuevo = new TurnoAPI();
            TurnoXUsuario turnoCreado = new TurnoXUsuario();

            CartasXTurnoXManoXUsuario cartaMano = new CartasXTurnoXManoXUsuario();
            CartasXTurnoXDeckXUsuario cartaDeck = new CartasXTurnoXDeckXUsuario();
            CartasXTurnoXPlanetaXUsuario cartaPlaneta = new CartasXTurnoXPlanetaXUsuario();
            List<DeckAPIGET> decks = new List<DeckAPIGET>();
            List<CartaAPI> deckEscogido = new List<CartaAPI>();

            //Se crea un nuevo turno
            turnoNuevo = turnoCompleto.infoPartida;
            turnoNuevo.Terminado = false;
            turnoNuevo.Numero_turno = 1;
            turnoNuevo.Id_Partida = Id_partida;
            turnoNuevo.Id_Usuario = Id_usuario;

            turnoCreado = this.addTurno(turnoNuevo);
            turnoNuevo = this.getTurno(turnoCreado.Id);

            turnoCompletoNuevo.infoPartida = turnoNuevo;

            //Con el nuevo turno creado, se crea la mano y deck restante del usuario
            decks = coleccionData.getDecksUsuario(Id_usuario);
            foreach(DeckAPIGET deck in decks)
            {
                if (deck.Estado == true)
                {
                    deckEscogido = deck.Cartas;
                }
            }

            //Se generan las cartas para la mano
            int[] NumerosRandomMano = RandomGenerator.GenerateRandomArray(5, 0, 17);
            RandomGenerator.OrdenarArray(NumerosRandomMano);

            List<CartaAPI> deckTemp = deckEscogido;
            List<CartaAPI> handTemp = new List<CartaAPI> ();

            foreach (int numero in NumerosRandomMano)
            {
                handTemp.Add(deckEscogido[numero]);
                deckTemp.Remove(deckEscogido[numero]);
            }


            turnoCompletoNuevo.cartasManoUsuario = handTemp;

            //Se mezclan las cartas del mazo restantes
            turnoCompletoNuevo.cartasDeckUsuario = deckTemp;
            RandomGenerator.ShuffleList(turnoCompletoNuevo.cartasDeckUsuario);

            //Se agregan a las listas de cada turno
            int contador = 0;
            foreach (CartaAPI carta in turnoCompletoNuevo.cartasManoUsuario)
            {
                cartaMano.Id_Carta = carta.Id;
                cartaMano.Id_Turno = turnoCreado.Id;
                cartaMano.Id_Usuario = Id_usuario;
                cartaMano.Posicion = contador;

                this.addCartaMano(cartaMano);

                contador++;
            }
            contador = 0;
            foreach (CartaAPI carta in turnoCompletoNuevo.cartasDeckUsuario)
            {
                cartaDeck.Id_Carta = carta.Id;
                cartaDeck.Id_Turno = turnoCreado.Id;
                cartaDeck.Id_Usuario = Id_usuario;
                cartaDeck.Posicion = contador;

                this.AddCartaDeck(cartaDeck);

                contador++;
            }

            turnoCompletoNuevo.cartasPlanetas = new List<List<CartaAPI>>() { null, null, null };
            turnoCompletoNuevo.cartasRivalPlanetas = new List<List<CartaAPI>>() { null, null, null };

            return turnoCompletoNuevo;
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
