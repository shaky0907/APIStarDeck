using StarDeckAPI.Data;
using StarDeckAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITesting.Controller
{
    public class MatchMakingControllerTest
    {

        private async Task<APIDbContext> GetDatabaseContext()
        {
            MockUpDataBase mock = new MockUpDataBase();
            var databaseContext = mock.GetDatabaseContext();

            return databaseContext;

        }

        [Fact]
        public async void buscarPartida() {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            var uscontroller = new UsuarioData(dbContext);

            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            var result = uscontroller.getJugadores();

            //Assert

            Assert.NotNull(result);
            Assert.Equal("Buscando partida", result[0].Actividad);
            Assert.Equal("Buscando partida", result[1].Actividad);
        }

        [Fact]
        public async void encontrarPartida()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            
            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            controller.matchmakingCheck("1");
            controller.matchmakingCheck("2");
            controller.matchmakingCheck("1");

            var partida = controller.isInMatch("1");
            var rival = controller.getRival("1",partida.Id);
            //Assert
            Assert.NotNull(partida);
            Assert.Equal("2",rival.Id);
            Assert.Contains("G-", partida.Id);
        }

        [Fact]
        public async void FinalizarPartida()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            var uscontroller = new UsuarioData(dbContext);

            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            controller.matchmakingCheck("1");
            controller.matchmakingCheck("2");
            controller.matchmakingCheck("1");

            var partida = controller.isInMatch("1");

            controller.finalizarJuego(partida.Id);
            controller.terminarPartida("1",partida.Id);
            controller.terminarPartida("2",partida.Id);

            var result = uscontroller.getJugadores();
            var partidaFin = dbContext.Partida.ToList().Where(x => x.Id == partida.Id).First();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("No busca partida", result[0].Actividad);
            Assert.Equal("No busca partida", result[1].Actividad);
            Assert.Equal(4, partidaFin.Estado);
        }

        [Fact]
        public async void GetWinner()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            var uscontroller = new UsuarioData(dbContext);
            var tcontroller = new TurnoData(dbContext);

            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            controller.matchmakingCheck("1");
            controller.matchmakingCheck("2");
            controller.matchmakingCheck("1");

            var partida = controller.isInMatch("1");

            tcontroller.AddTurnoCompleto(partida.Id, "1", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "1",
                    Energia = 50,
                    Terminado = false
                }
            });

            tcontroller.AddTurnoCompleto(partida.Id, "2", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "2",
                    Energia = 50,
                    Terminado = false
                }
            });

            var turno0Us1 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "1");
            var turno0Us2 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "2");

            var carta = new CartaAPI()
            {
                Id = "1",
                Nombre = "Carta nueva",
                Energia = 20,
                Costo = 20,
                Imagen = "1211313",
                Raza = "1",
                Tipo = "1",
                Estado = true,
                Descripcion = "1",
            };

            turno0Us1.cartasPlanetas[0].Add(carta);

            var turno1Us2 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "2", turno0Us2);
            var turno1Us1 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "1", turno0Us1);
            

            var ganador = tcontroller.getGanadorPartida(partida.Id,"1");

            controller.finalizarJuego(partida.Id);
            controller.terminarPartida("1", partida.Id);
            controller.terminarPartida("2", partida.Id);

            var usuario = uscontroller.getUsuario("1");


            //Assert

            Assert.NotNull(turno0Us1);
            Assert.NotNull(turno0Us2);
            Assert.Equal("1", ganador.Winner.Id);
            Assert.Equal(101, usuario.Ranking);
        }

        [Fact]
        public async void GetEmpate()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            var uscontroller = new UsuarioData(dbContext);
            var tcontroller = new TurnoData(dbContext);

            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            controller.matchmakingCheck("1");
            controller.matchmakingCheck("2");
            controller.matchmakingCheck("1");

            var partida = controller.isInMatch("1");

            tcontroller.AddTurnoCompleto(partida.Id, "1", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "1",
                    Energia = 50,
                    Terminado = false
                }
            });

            tcontroller.AddTurnoCompleto(partida.Id, "2", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "2",
                    Energia = 50,
                    Terminado = false
                }
            });

            var turno0Us1 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "1");
            var turno0Us2 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "2");



            var turno1Us2 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "2", turno0Us2);
            var turno1Us1 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "1", turno0Us1);


            var ganador = tcontroller.getGanadorPartida(partida.Id, "1");

            controller.finalizarJuego(partida.Id);
            controller.terminarPartida("1", partida.Id);
            controller.terminarPartida("2", partida.Id);
            var usuario = uscontroller.getUsuario("1");

            //Assert

            Assert.NotNull(turno0Us1);
            Assert.NotNull(turno0Us2);
            Assert.Null(ganador.Winner);
            Assert.Equal(100, usuario.Ranking);

        }

        [Fact]
        public async void Rendirse()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            var uscontroller = new UsuarioData(dbContext);
            var tcontroller = new TurnoData(dbContext);

            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            controller.matchmakingCheck("1");
            controller.matchmakingCheck("2");
            controller.matchmakingCheck("1");

            var partida = controller.isInMatch("1");

            tcontroller.AddTurnoCompleto(partida.Id, "1", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "1",
                    Energia = 50,
                    Terminado = false
                }
            });

            tcontroller.AddTurnoCompleto(partida.Id, "2", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "2",
                    Energia = 50,
                    Terminado = false
                }
            });

            var turno0Us1 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "1");
            var turno0Us2 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "2");

            var turno1Us2 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "2", turno0Us2);
            var turno1Us1 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "1", turno0Us1);

            tcontroller.giveUpMatch(partida.Id, "1");

            var ganador = tcontroller.getGanadorPartida(partida.Id, "1");

            //Assert 
            Assert.Equal("2", ganador.Winner.Id);
        }


        [Fact]
        public async void Reconectarse()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var controller = new MatchmakingData(dbContext);
            var uscontroller = new UsuarioData(dbContext);
            var tcontroller = new TurnoData(dbContext);

            //Act
            controller.buscarPartida("1");
            controller.buscarPartida("2");

            controller.matchmakingCheck("1");
            controller.matchmakingCheck("2");
            controller.matchmakingCheck("1");

            var partida = controller.isInMatch("1");

            tcontroller.AddTurnoCompleto(partida.Id, "1", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "1",
                    Energia = 50,
                    Terminado = false
                }
            });

            tcontroller.AddTurnoCompleto(partida.Id, "2", new TurnoCompletoAPI()
            {
                infoPartida = new TurnoAPI()
                {
                    Id_Partida = partida.Id,
                    Numero_turno = 0,
                    Id_Usuario = "2",
                    Energia = 50,
                    Terminado = false
                }
            });

            var turno0Us1 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "1");
            var turno0Us2 = await tcontroller.getInfoCompletaUltimoTurno(partida.Id, "2");

            var turno1Us2 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "2", turno0Us2);
            var turno1Us1 = tcontroller.CrearNuevoTurnoCompleto(partida.Id, "1", turno0Us1);


            //El usuario de desconecta
            var usuario = uscontroller.getUsuario("1");

            Assert.Equal("En partida", usuario.Actividad);

            var partidaReconect = controller.isInMatch("1");

            //Assert
            Assert.Equal(partida.Id,partidaReconect.Id);
        }
    }
}