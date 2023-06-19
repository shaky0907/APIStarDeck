using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Models;
using StarDeckAPI.Utilities;

namespace StarDeckAPI.Data
{
    public class MatchmakingData
    {
        private APIDbContext apiDBContext;
        public MatchmakingData(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }

        public MatchmakingResponse matchmakingCheck(string Id)
        {
            MatchmakingResponse matchmakingResponse = new MatchmakingResponse();
            List<UsuarioXPartida> uxpL = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == Id).ToList();
            int myUserCheck = 0;
            bool continue_check = false;

            if (apiDBContext.Usuario.ToList().Where(x => x.Id == Id).ToList().Any())
            {
                continue_check = true;
                myUserCheck = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First().Id_actividad;
            }

            if (continue_check)
            {
                if (uxpL.Any())
                {
                    bool alreadypaired = false;
                    bool alreadystarted = false;
                    foreach (UsuarioXPartida uxp in uxpL)
                    {
                        Partida partida = apiDBContext.Partida.ToList().Where(x => x.Id == uxp.Id_Partida).First();
                        bool uxp_check_master = apiDBContext.UsuarioXPartida.ToList().Where(x => x.Id_Usuario == Id).First().Id_Master;
                        int estado = 0;
                        if ((partida.Estado == 1) && (!uxp_check_master))
                        {
                            alreadypaired = true;
                            estado = 2;
                            this.setPartidaEstado(partida, estado);
                            matchmakingResponse.Id_Partida = partida.Id;
                            break;



                        }
                        else if ((partida.Estado == 2) && (uxp_check_master))
                        {
                            alreadystarted = true;
                            estado = 3;
                            this.setPartidaEstado(partida, estado);
                            matchmakingResponse.Id_Partida = partida.Id;
                            break;
                        }

                    }

                    if (alreadypaired)
                    {
                        matchmakingResponse.estado = 2;
                    }
                    else if (alreadystarted)
                    {
                        matchmakingResponse.estado = 3;
                    }

                    else if (myUserCheck == 2)
                    {

                        matchmakingResponse = this.MatchManager(Id);
                    }
                    else
                    {
                        matchmakingResponse.estado = 4;
                    }
                }
                else
                {

                    matchmakingResponse = this.MatchManager(Id);


                }
            }
            matchmakingResponse.Id = Id;

            return matchmakingResponse;
        }
        private MatchmakingResponse MatchManager(string Id)
        {

            Usuario myUser = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            MatchmakingResponse matchmakingResponse = new MatchmakingResponse();
            List<Usuario> usuarios = apiDBContext.Usuario.ToList();
            bool foundanotheruser = false;
            string idFoundUser = "";
            foreach (Usuario user in usuarios)
            {
                if ((user.Id != Id) && (user.Id_actividad == 2))
                {
                    if ((user.Ranking > myUser.Ranking - 50) && (user.Ranking < myUser.Ranking + 50))
                    {
                        idFoundUser = user.Id;
                        this.AgregarUsuariosPartida(myUser, user);
                        foundanotheruser = true;
                        break;
                    }
                }
            }
            if (foundanotheruser)
            {
                string idPartida = GeneratorID.GenerateRandomId("G-");
                matchmakingResponse.estado = 1;
                matchmakingResponse.Id_Partida = idPartida;
                this.InicializarPartida(idPartida, Id, idFoundUser);

                this.setPlanetasPartida(idPartida);
            }
            else
            {
                matchmakingResponse.estado = 0;

            }

            return matchmakingResponse;
        }

        private void AgregarUsuariosPartida(Usuario usuario1, Usuario usuario2)
        {
            usuario1.Id_actividad = 3;
            usuario2.Id_actividad = 3;
            apiDBContext.Update(usuario1);
            apiDBContext.Update(usuario2);
            apiDBContext.SaveChanges();
        }

        private void InicializarPartida(string idPartida, string Id, string idFoundUser)
        {
            
            Partida partidanueva = new Partida()
            {
                Id = idPartida,
                Estado = 1,
                Fecha_hora = DateTime.Now
            };
            
            apiDBContext.Add(partidanueva);
            apiDBContext.SaveChanges();

            UsuarioXPartida uxpnew1 = new UsuarioXPartida()
            {
                Id_Partida = idPartida,
                Id_Usuario = Id,
                Id_Master = true
            };

            UsuarioXPartida uxpnew2 = new UsuarioXPartida()
            {
                Id_Partida = idPartida,
                Id_Usuario = idFoundUser,
                Id_Master = false
            };

            apiDBContext.Add(uxpnew1);
            apiDBContext.Add(uxpnew2);
            apiDBContext.SaveChanges();
        }

        private void setPartidaEstado(Partida partida, int estado)
        {
            partida.Estado = estado;

            apiDBContext.Update(partida);
            apiDBContext.SaveChanges();
        }

        private void setPlanetasPartida(string idPartida)
        {
            int i = 0;
                int planetsLeft = 3;
                Random rand = new Random();
                List<Planeta> planetasSelect = apiDBContext.Planeta.ToList();
                List<Tipo_planeta> probabilidades = apiDBContext.Tipo_planeta.ToList();
                List<Planeta> planetas_partida = new List<Planeta>();

                while (planetsLeft > 0)
                {
                    if ((rand.NextDouble() < ((float)(probabilidades.Where(x => x.Id == planetasSelect.ElementAt(i).Tipo).First().Probabilidad)) / (float)100.0) &&
                        !(planetas_partida.Contains(planetasSelect.ElementAt(i))))
                    {
                        planetas_partida.Add(planetasSelect.ElementAt(i));
                        planetsLeft--;
                        PlanetasXPartida pxp = new PlanetasXPartida()
                        {
                            Id_Partida = idPartida,
                            Id_Planeta = planetasSelect.ElementAt(i).Id
                        };
                        apiDBContext.Add(pxp);
                    }
                    i++;



                    if (i == planetasSelect.Count)
                    {
                        i = 0;
                    }
                }
                apiDBContext.SaveChanges();
        }

        public Usuario buscarPartida(string Id)
        {
            Usuario user = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            user.Id_actividad = 2;
            apiDBContext.Update(user);
            apiDBContext.SaveChanges();
            return user;
        }

        public Usuario terminarPartida(string Id, string matchId)
        {
            TurnoData turnoData = new TurnoData(apiDBContext);
            WinnerAPI winnerApi = turnoData.getGanadorPartida(matchId,Id);
            Usuario user = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            user.Id_actividad = 1;
            if (winnerApi.Winner != null && winnerApi.Winner.Id == Id) {
                user.Ranking += 1;
            }
            apiDBContext.Update(user);
            apiDBContext.SaveChanges();
            return user;
        }

        public Usuario terminarBusquedaPartida(string Id) {
            Usuario user = apiDBContext.Usuario.ToList().Where(x => x.Id == Id).First();
            user.Id_actividad = 1;
            apiDBContext.Update(user);
            apiDBContext.SaveChanges();
            return user;
        }

        public Partida finalizarJuego(string Id)
        {
            Partida partida = apiDBContext.Partida.ToList().Where(x => x.Id == Id).First();
            partida.Estado = 4;
            apiDBContext.Update(partida);
            apiDBContext.SaveChanges();

            return partida;
        }

        public List<Planeta> getPlanetasPartida(string Id)
        {
            List<PlanetasXPartida> pxps = apiDBContext.PlanetasXPartida.ToList().Where(x => x.Id_Partida == Id).ToList();
            List<Planeta> planetas = new List<Planeta>();

            foreach (PlanetasXPartida pxp in pxps)
            {
                Planeta p = new Planeta()
                {
                    Id = pxp.Id_Planeta,
                    Nombre = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Nombre,
                    Tipo = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Tipo,
                    Descripcion = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Descripcion,
                    Estado = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Estado,
                    Imagen = apiDBContext.Planeta.ToList().Where(x => x.Id == pxp.Id_Planeta).First().Imagen

                };
                planetas.Add(p);
            }

            return planetas;
        }

        public Usuario getRival(string Id_usuario, string Id_Partida)
        {
            string rival = apiDBContext.UsuarioXPartida.ToList().Where(x => (x.Id_Partida == Id_Partida) && (x.Id_Usuario != Id_usuario)).First().Id_Usuario;
            Usuario rivalUsuario = new Usuario();
            if (rival != null)
            {
                rivalUsuario = apiDBContext.Usuario.ToList().Where(x => x.Id == rival).First();
            }

            return rivalUsuario;
        }

        public Partida isInMatch(string Id_usuario)
        {
            Usuario usuario = apiDBContext.Usuario.ToList().Where(x => x.Id == Id_usuario).First();
            Partida partidaActual = new Partida();
            List<UsuarioXPartida> partidas = apiDBContext.UsuarioXPartida.ToList();
            List<UsuarioXPartida> partidasXUsuario = new List<UsuarioXPartida>();

            foreach (UsuarioXPartida partida in partidas)
            {
                if (partida.Id_Usuario == Id_usuario)
                {
                    partidasXUsuario.Add(partida);
                }
            }
            if (usuario.Id_actividad == 3)
            {
                foreach (UsuarioXPartida partida in partidasXUsuario)
                {
                    try
                    {
                        partidaActual = apiDBContext.Partida.ToList().Where(x => x.Id == partida.Id_Partida && x.Estado == 3).First();
                    }
                    catch
                    {
                        //DO NOTHING
                    }
                }
            }
            return partidaActual;
        }
    }
}
