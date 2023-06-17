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


        public CartasXTurnoXManoXUsuario addCartaMano(CartasXTurnoXManoXUsuario cartasxturnoxmanoxusuario)
        {


            apiDBContext.CartasXTurnoXManoXUsuario.Add(cartasxturnoxmanoxusuario);

            Console.WriteLine("CARTA POR INSERTAR");
            Console.WriteLine(cartasxturnoxmanoxusuario.Id_Turno);
            Console.WriteLine(cartasxturnoxmanoxusuario.Id_Carta);

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

        private List<UsuarioAPI> anadirGanadorPlaneta(WinnerAPI winnerAPI, List<Planeta> planetas, UsuarioAPI rival, string Id_partida, UsuarioAPI jugador)
        {
            PlanetaData planetaData = new PlanetaData(this.apiDBContext);
            TurnoXUsuario turno = this.getLastTurno(Id_partida, jugador.Id);
            TurnoXUsuario turno_rival = this.getLastTurno(Id_partida, rival.Id);
            List<UsuarioAPI> ganadorPorPlaneta = new List<UsuarioAPI>();
            winnerAPI.PointsPerPlanet = new List<int>();
            winnerAPI.PointsRivalPerPlanet = new List<int>();
            winnerAPI.PlanetsOnMatch = new List<PlanetaAPIGet>();

            foreach (Planeta planeta in planetas)
            {
                //Sacar los puntos del usuario
                int PuntosUsuario = this.getPuntosDePlaneta(planeta, turno.Id, jugador.Id);
                winnerAPI.PointsPerPlanet.Add(PuntosUsuario);

                //Sacar los puntos del rival
                int PuntosRival = this.getPuntosDePlaneta(planeta, turno_rival.Id, rival.Id);
                winnerAPI.PointsRivalPerPlanet.Add(PuntosRival);

                //Sacar el ganador teniendo los puntos del planeta
                ganadorPorPlaneta.Add(GanadorFinder.getGanadorPlaneta(rival, jugador, PuntosUsuario, PuntosRival));

                winnerAPI.PlanetsOnMatch.Add(planetaData.getPlaneta(planeta.Id));
            }

            return ganadorPorPlaneta;
        }
        
        private void actualizarResultadosPartida(WinnerAPI winnerAPI, UsuarioAPI jugador, UsuarioAPI rival, string Id_partida)
        {   
            //Declaraciones iniciales
            UsuarioXPartida jugadorXPartida = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == jugador.Id &&
                x.Id_Partida == Id_partida).First();
            UsuarioXPartida rivalXPartida = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == rival.Id &&
                x.Id_Partida == Id_partida).First();

            Console.WriteLine("---- JUGADOR ----");
            Console.WriteLine(jugadorXPartida.Id_Usuario);
            Console.WriteLine(jugadorXPartida.Ganador);
            Console.WriteLine("---- RIVAL ----");
            Console.WriteLine(rivalXPartida.Id_Usuario);
            Console.WriteLine(rivalXPartida.Ganador);

            //Sacar el ganador de la partida en si
            winnerAPI.Winner = GanadorFinder.getGanadorPartidaCompleta(winnerAPI.WinnerPerPlanet, jugador, rival);

            //Actualizar informacion en la base
            if (!jugadorXPartida.Ganador && !rivalXPartida.Ganador)
            {
                UsuarioXPartida usuarioXPartida = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == winnerAPI.Winner.Id &&
                    x.Id_Partida == Id_partida).First();
                usuarioXPartida.Ganador = true;
                apiDBContext.UsuarioXPartida.Update(usuarioXPartida);
                apiDBContext.SaveChanges();
            }
            else if (jugadorXPartida.Ganador)
            {
                winnerAPI.Winner = jugador;
            }
            else if (rivalXPartida.Ganador)
            {
                winnerAPI.Winner = rival;
            }

            //Comprobar el ganador de la partida
            this.comprobarVictoriaJugador(winnerAPI, jugador, rival);
        }

        public UsuarioXPartida giveUpMatch(string Id_partida, string Id_usuario)
        { 
            Console.WriteLine("---- GIVE UP ----");
            MatchmakingData matchmakingData = new MatchmakingData(apiDBContext);
            string rival_id = matchmakingData.getRival(Id_usuario, Id_partida).Id;
            Console.WriteLine("---- BEFORE DB USUARIO X PARTIDA ----");
            UsuarioXPartida usuarioXPartida = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == rival_id &&
                x.Id_Partida == Id_partida).First();
            
            Console.WriteLine("---- AFTER DB USUARIO X PARTIDA ----");
            usuarioXPartida.Ganador = true;
            apiDBContext.UsuarioXPartida.Update(usuarioXPartida);
            apiDBContext.SaveChanges();

            return usuarioXPartida;
        }

        public WinnerAPI getGanadorPartida(string Id_partida, string Id_usuario)
        {
            //Declaraciones inciales
            WinnerAPI winnerAPI = new WinnerAPI();
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            UsuarioData usuarioData = new UsuarioData(this.apiDBContext);

            List<Planeta> planetas = matchmakingData.getPlanetasPartida(Id_partida);

            Usuario rival_ = matchmakingData.getRival(Id_usuario, Id_partida);
            UsuarioAPI rival = usuarioData.getUsuario(rival_.Id);

            UsuarioAPI jugador = usuarioData.getUsuario(Id_usuario);

            //Settear los ganadores por planeta
            winnerAPI.WinnerPerPlanet = this.anadirGanadorPlaneta(winnerAPI,planetas,rival,Id_partida,jugador);

            //Actualizar resultados de partida
            this.actualizarResultadosPartida(winnerAPI, jugador, rival, Id_partida);

            return winnerAPI;

        }

        private void anadirManoTurno(TurnoCompletoAPI turnoCompleto, string Id_usuario)
        {
            CartasXTurnoXManoXUsuario cartaMano = new CartasXTurnoXManoXUsuario();
            string id_turno = this.getLastTurno(turnoCompleto.infoPartida.Id_Partida, Id_usuario).Id;
            int contador = 0;

            foreach (CartaAPI carta in turnoCompleto.cartasManoUsuario)
            {
                cartaMano.Id_Carta = carta.Id;
                cartaMano.Id_Turno = id_turno;
                cartaMano.Id_Usuario = Id_usuario;
                cartaMano.Posicion = contador;

                this.addCartaMano(cartaMano);

                contador++;
            }
        }

        private void anadirDeckTurno(TurnoCompletoAPI turnoCompleto, string Id_usuario)
        {
            CartasXTurnoXDeckXUsuario cartaDeck = new CartasXTurnoXDeckXUsuario();
            string id_turno = this.getLastTurno(turnoCompleto.infoPartida.Id_Partida, Id_usuario).Id;
            int contador = 0;

            foreach (CartaAPI carta in turnoCompleto.cartasDeckUsuario)
            {
                cartaDeck.Id_Carta = carta.Id;
                cartaDeck.Id_Turno = id_turno;
                cartaDeck.Id_Usuario = Id_usuario;
                cartaDeck.Posicion = contador;

                this.AddCartaDeck(cartaDeck);

                contador++;
            }
        }

        private void crearTurnoNuevo(TurnoCompletoAPI turnoCompleto, string Id_partida, string Id_usuario)
        {
            TurnoAPI turnoNuevo = new TurnoAPI();
            TurnoXUsuario turnoCreado = new TurnoXUsuario();

            const int energiaGanadaPorTurno = 30;

            turnoNuevo = turnoCompleto.infoPartida;
            if (turnoNuevo.Numero_turno != 0) {
                turnoNuevo.Energia += energiaGanadaPorTurno;
            }
            turnoNuevo.Terminado = false;
            turnoNuevo.Numero_turno += 1;
            turnoNuevo.Id_Partida = Id_partida;
            turnoNuevo.Id_Usuario = Id_usuario;

            turnoCreado = this.addTurno(turnoNuevo);
            turnoNuevo = this.getTurno(turnoCreado.Id);

            turnoCompleto.infoPartida = turnoNuevo;
        }

        private void anadirCartaAPlanetaTurno(TurnoCompletoAPI turnoCompleto, string Id_usuario, string Id_partida)
        {
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            CartasXTurnoXPlanetaXUsuario cartaPlaneta = new CartasXTurnoXPlanetaXUsuario();
            string id_turno = this.getLastTurno(turnoCompleto.infoPartida.Id_Partida, Id_usuario).Id;


            int contador = 0;
            List<Planeta> planetas = matchmakingData.getPlanetasPartida(Id_partida);

            foreach (List<CartaAPI> listaCartas in turnoCompleto.cartasPlanetas)
            {
                List<CartaAPI> cartasExistentesEnPlaneta = this.getPlanetaCartasPartida(planetas[contador].Id, Id_partida, Id_usuario);

                foreach (CartaAPI carta in listaCartas)
                {
                    if (!cartasExistentesEnPlaneta.Contains(carta))
                    {
                        cartaPlaneta.Id_Turno = id_turno;
                        cartaPlaneta.Id_Carta = carta.Id;
                        cartaPlaneta.Id_Planeta = planetas[contador].Id;
                        cartaPlaneta.Id_Usuario = Id_usuario;

                        this.AddCartaPlaneta(cartaPlaneta);
                    }
                }
                contador++;
            }
        }

        private void agarrarCartaDeck(TurnoCompletoAPI turnoCompleto)
        {
            const int maximoCartasEnMano = 7;
            if (turnoCompleto.cartasManoUsuario.Count < maximoCartasEnMano && turnoCompleto.cartasDeckUsuario.Count > 0)
            {
                turnoCompleto.cartasManoUsuario.Add(turnoCompleto.cartasDeckUsuario[0]);
                turnoCompleto.cartasDeckUsuario.RemoveAt(0);
            }
        }

        private void obtenerTotalidadCartasTurno(TurnoCompletoAPI turnoCompleto, string Id_usuario, string Id_partida)
        {
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            UsuarioData usuarioData = new UsuarioData(this.apiDBContext);

            string id_turno = this.getLastTurno(Id_partida, Id_usuario).Id;
            turnoCompleto.infoPartida = this.getTurno(id_turno);

            string rival_id = matchmakingData.getRival(Id_usuario, Id_partida).Id;
            turnoCompleto.rival = usuarioData.getUsuario(rival_id);
            string id_turno_rival = this.getLastTurno(Id_partida, rival_id).Id;


            turnoCompleto.cartasManoUsuario = this.getUserMano(Id_usuario, id_turno);
            turnoCompleto.cartasDeckUsuario = this.getUserDeck(Id_usuario, id_turno);
            turnoCompleto.cartasManoRival = this.getUserMano(rival_id, id_turno_rival);
            turnoCompleto.cartasDeckRival = this.getUserDeck(rival_id, id_turno_rival);
        }

        private void obtenerTotalidadPlanetas(TurnoCompletoAPI turnoCompleto, string Id_usuario, string Id_partida)
        {
            PlanetaData planetaData = new PlanetaData(this.apiDBContext);
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);

            List<Planeta> planetas = matchmakingData.getPlanetasPartida(Id_partida);
            List<PlanetaAPIGet> planetasEnPartida = new List<PlanetaAPIGet>();

            turnoCompleto.cartasPlanetas = new List<List<CartaAPI>>();
            turnoCompleto.cartasRivalPlanetas = new List<List<CartaAPI>>();

            string id_turno = this.getLastTurno(Id_partida, Id_usuario).Id;

            string rival_id = matchmakingData.getRival(Id_usuario, Id_partida).Id;
            string id_turno_rival = this.getLastTurno(Id_partida, rival_id).Id;

            foreach (Planeta planeta in planetas)
            {
                turnoCompleto.cartasPlanetas.Add(this.getPlanetaCartas(planeta.Id, id_turno, Id_usuario));
                turnoCompleto.cartasRivalPlanetas.Add(this.getPlanetaCartas(planeta.Id, id_turno_rival, rival_id));

                planetasEnPartida.Add(planetaData.getPlaneta(planeta.Id));
            }

            turnoCompleto.planetasEnPartida = planetasEnPartida;
        }

        public async Task<TurnoCompletoAPI> getInfoCompletaUltimoTurno(string Id_partida, string Id_usuario)
        {
            //Declaraciones Iniciales
            TurnoCompletoAPI turnoCompleto = new TurnoCompletoAPI();

            //Obtener todas las cartas de la mano y mazo tanto para el jugador como el rival
            this.obtenerTotalidadCartasTurno(turnoCompleto, Id_usuario, Id_partida);

            //Obtener todas las cartas por planeta y planetas en si
            this.obtenerTotalidadPlanetas(turnoCompleto, Id_usuario, Id_partida);

            return turnoCompleto;
        }

        public async Task<TurnoCompletoAPI> updateInfoCompletaTurno(string Id_partida, string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            TurnoCompletoAPI turnoCompletoUpdated = new TurnoCompletoAPI();

            //Se actualiza el turno
            string id_turno = this.getLastTurno(Id_partida, Id_usuario).Id;
            this.actualizarTurno(id_turno, turnoCompleto.infoPartida);

            //Se agarra toda la informacion del turno
            turnoCompletoUpdated = await this.getInfoCompletaUltimoTurno(Id_partida, Id_usuario);

            return turnoCompletoUpdated;
        }

        public async Task<TurnoCompletoAPI> CrearNuevoTurnoCompleto(string Id_partida, string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {
            //Se crea un nuevo turno
            this.crearTurnoNuevo(turnoCompleto, Id_partida, Id_usuario);

            //Se agarra una de las cartas del mazo y pasa a la mano si la mano no supera las 7 cartas
            this.agarrarCartaDeck(turnoCompleto);

            //Se actualiza el mazo y mano del usuario para el nuevo turno
            this.anadirManoTurno(turnoCompleto, Id_usuario);
            this.anadirDeckTurno(turnoCompleto, Id_usuario);

            //Se actualizan todas las cartas de los planetas
            this.anadirCartaAPlanetaTurno(turnoCompleto, Id_usuario, Id_partida);

            //Bucle para nuevo turno
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            string rival_id = matchmakingData.getRival(Id_usuario, Id_partida).Id;
            Boolean opponentIsInNewTurn = false;
            while(!opponentIsInNewTurn)
            {
                TurnoCompletoAPI turnoRival = await this.getInfoCompletaUltimoTurno(Id_partida, rival_id);
                if (turnoCompleto.infoPartida.Numero_turno == turnoRival.infoPartida.Numero_turno && turnoRival.infoPartida.Terminado == false) {
                    opponentIsInNewTurn = true;
                }
                await Task.Delay(1500);
            } 

            //Se updatea la informacion del turno para updatear la energia, numero de turno y estado
            turnoCompleto = await this.updateInfoCompletaTurno(Id_partida, Id_usuario, turnoCompleto);

            return turnoCompleto;
        }

        private List<CartaAPI> deckHabilitado(string Id_usuario)
        {
            ColeccionData coleccionData = new ColeccionData(this.apiDBContext);
            List<DeckAPIGET> decks = new List<DeckAPIGET>();

            decks = coleccionData.getDecksUsuario(Id_usuario);
            foreach (DeckAPIGET deck in decks)
            {
                if (deck.Estado == true)
                {
                    return deck.Cartas;
                }
            }

            return null;
        }

        private List<CartaAPI> generarCartasManoUsuario(TurnoCompletoAPI turnoCompleto, List<CartaAPI> deckEscogido, string Id_usuario)
        {

            int[] NumerosRandomMano = RandomGenerator.GenerateRandomArray(5, 0, 17);
            RandomGenerator.OrdenarArray(NumerosRandomMano);

            List<CartaAPI> deckTemp = deckEscogido;
            List<CartaAPI> handTemp = new List<CartaAPI>();

            foreach (int numero in NumerosRandomMano)
            {
                handTemp.Add(deckEscogido[numero]);
                deckTemp.Remove(deckEscogido[numero]);
            }


            turnoCompleto.cartasManoUsuario = handTemp;

            //Añadir la mano al turno
            this.anadirManoTurno(turnoCompleto, Id_usuario);
            return deckTemp;
        }

        public TurnoCompletoAPI AddTurnoCompleto(string Id_partida, string Id_usuario, TurnoCompletoAPI turnoCompleto)
        {   
            //Declaraciones Iniciales
            MatchmakingData matchmakingData = new MatchmakingData(this.apiDBContext);
            CartasXTurnoXPlanetaXUsuario cartaPlaneta = new CartasXTurnoXPlanetaXUsuario();
            List<CartaAPI> deckEscogido = new List<CartaAPI>();

            //Se crea un nuevo turno
            this.crearTurnoNuevo(turnoCompleto, Id_partida, Id_usuario);

            //Con el nuevo turno creado, se crea la mano y deck restante del usuario
            //Sacar el deck habilitado del usuario
            deckEscogido = this.deckHabilitado(Id_usuario);

            //Se generan las cartas para la mano
            List<CartaAPI> deckGenerado = this.generarCartasManoUsuario(turnoCompleto, deckEscogido, Id_usuario);
            
            //Se mezclan las cartas del mazo restantes y se añaden al mazo del turno
            turnoCompleto.cartasDeckUsuario = deckGenerado;
            RandomGenerator.ShuffleList(turnoCompleto.cartasDeckUsuario);
            this.anadirDeckTurno(turnoCompleto, Id_usuario);

            //Crear estas listas vacias inicialmente
            turnoCompleto.cartasPlanetas = new List<List<CartaAPI>>() { null, null, null };
            turnoCompleto.cartasRivalPlanetas = new List<List<CartaAPI>>() { null, null, null };

            return turnoCompleto;
        }

        private void comprobarVictoriaJugador(WinnerAPI winnerAPI, UsuarioAPI jugador, UsuarioAPI rival)
        {
            if (winnerAPI.Winner == null)
            {
                winnerAPI.Loser = null;
            }
            else if (winnerAPI.Winner == jugador) 
            {
                winnerAPI.Loser = rival;
            }
            else
            {
                winnerAPI.Loser = jugador;
            }
        }
    }
}
