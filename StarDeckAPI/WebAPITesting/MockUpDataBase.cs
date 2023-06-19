using Microsoft.EntityFrameworkCore;
using StarDeckAPI.Data;
using StarDeckAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITesting
{
    public class MockUpDataBase
    {

        public APIDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new APIDbContext(options);
            databaseContext.Database.EnsureCreated();

            addCartas(databaseContext);
            addRazas(databaseContext);
            addTipo(databaseContext);
            addPais(databaseContext);
            addAvatar(databaseContext);
            addUsuarios(databaseContext);
            createColeccion(databaseContext);
            createDeck(databaseContext);
            addTipoPlaneta(databaseContext);
            addPlanetas(databaseContext);
            addActividad(databaseContext);
            addEstadoPartida(databaseContext);

            return databaseContext;
        }

        public void addEstadoPartida(APIDbContext databaseContext)
        {
            if (databaseContext.Estado_Partida.Count() <= 0)
            {
                databaseContext.Estado_Partida.Add(new Estado_Partida()
                {
                    Id = 1,
                    Nombre = "Emparejando"
                });
                databaseContext.SaveChanges();

                databaseContext.Estado_Partida.Add(new Estado_Partida()
                {
                    Id = 2,
                    Nombre = "En curso"
                });
                databaseContext.SaveChanges();

                databaseContext.Estado_Partida.Add(new Estado_Partida()
                {
                    Id = 3,
                    Nombre = "Finalizada"
                });
                databaseContext.SaveChanges();
            }
        }

        public void addActividad(APIDbContext databaseContext)
        {
            if (databaseContext.Actividad.Count() <= 0)
            {
                databaseContext.Actividad.Add(new Actividad()
                {
                    Id = 1,
                    Nombre_act = "No busca partida"
                });
                databaseContext.SaveChanges();

                databaseContext.Actividad.Add(new Actividad()
                {
                    Id = 2,
                    Nombre_act = "Buscando partida"
                });
                databaseContext.SaveChanges();

                databaseContext.Actividad.Add(new Actividad()
                {
                    Id = 3,
                    Nombre_act = "En partida"
                });
                databaseContext.SaveChanges();
            }
        }

        public void addTipoPlaneta(APIDbContext databaseContext)
        {
            if (databaseContext.Tipo_planeta.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Tipo_planeta.Add(new Tipo_planeta()
                    {
                        Id = i,
                        Nombre = "Planeta "+i.ToString(),
                        Probabilidad = 50/i
                    });
                    databaseContext.SaveChanges();
                }
            }
        }

        public void addPlanetas(APIDbContext databaseContext)
        {
            if (databaseContext.Planeta.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++) 
                {
                    databaseContext.Planeta.Add(new Planeta()
                    {
                        Id = i.ToString(),
                        Nombre = "Planeta "+i.ToString(),
                        Tipo = 1,
                        Descripcion = i.ToString(),
                        Estado = true,
                        Imagen = "00000"

                    });
                    databaseContext.SaveChanges();
                }
            }
        }


        public void createDeck(APIDbContext databaseContext)
        {
            if (databaseContext.Deck.Count() <= 0)
            {
                var usuarios = databaseContext.Usuario.ToList();
                for (int i = 0; i < usuarios.Count; i++)
                {
                    databaseContext.Deck.Add(new Deck()
                    {
                        Id = usuarios[i].Id,
                        Nombre = "Deck "+i.ToString(),
                        Estado = true,
                        Id_usuario = usuarios[i].Id
                    });
                    databaseContext.SaveChanges();
                }
            }

            if (databaseContext.CartasXDeck.Count() <= 0)
            {
                var usuarios = databaseContext.Usuario.ToList();
                var cartas = databaseContext.Carta.ToList();
                for (int i = 0; i < usuarios.Count; i++)
                {
                    for (int j = 0; j < cartas.Count; j++)
                    {
                        databaseContext.CartasXDeck.Add(new CartasXDeck()
                        {
                            Id_Deck = usuarios[i].Id,
                            Id_Carta = cartas[j].Id
                        });
                        databaseContext.SaveChanges();
                    }

                }
            }
        }


        public void createColeccion(APIDbContext databaseContext)
        {
            var cartas = databaseContext.Carta.ToList();
            var usuarios = databaseContext.Usuario.ToList();

            if(databaseContext.CartaXUsuario.Count() <= 0)
            {
                for (int i = 0; i < usuarios.Count; i++)
                {
                    for (int j = 0; j < cartas.Count; j++)
                    {
                        databaseContext.CartaXUsuario.Add(new CartaXUsuario()
                        {
                            Id_usuario = usuarios[i].Id,
                            Id_carta = cartas[j].Id
                        });
                        databaseContext.SaveChanges();
                    }
                    
                }
                
            }
        }

        public void addAvatar(APIDbContext databaseContext)
        {
            if (databaseContext.Avatar.Count() <= 0)
            {
                databaseContext.Avatar.Add(new Avatar()
                {
                    Id =1,
                    Imagen ="fewfefaewdgtryntrhbrty"
                });
                databaseContext.SaveChanges();
            }
        }

        public void addPais(APIDbContext databaseContext)
        {
            if (databaseContext.Paises.Count() <= 0)
            {
                for (int i = 1; i < 3; i++)
                {
                    databaseContext.Paises.Add(new Pais()
                    {
                        Id = i,
                        Nombre = "pais " + i.ToString()
                    });
                    databaseContext.SaveChanges();
                }
            }
        }

        public void addUsuarios(APIDbContext databaseContext)
        {
            if (databaseContext.Usuario.Count() <= 0)
            {
                for (int i = 1; i < 3; i++)
                {
                    databaseContext.Usuario.Add(new Usuario()
                    {
                        Id = i.ToString(),
                        Administrador = false,
                        Nombre = "Usuario "+i.ToString(),
                        Username = "Username " + i.ToString(),
                        Contrasena = "1234567"+i.ToString(),
                        Correo = "correo"+i.ToString()+"@gmail.com",
                        Nacionalidad = 1,
                        Estado = true,
                        Avatar = 1,
                        Ranking = 100,
                        Monedas = 100,
                        Id_actividad = 1,
                    });

                    databaseContext.SaveChanges();
                }
            }
        }



        public void addCartas(APIDbContext databaseContext) 
        {
            if (databaseContext.Carta.Count() <= 0)
            {
                for (int i = 1; i <= 18; i++)
                {
                    databaseContext.Carta.Add(new Carta()
                    {
                        Id = i.ToString(),
                        N_Personaje = i.ToString(),
                        Energia = 1,
                        C_batalla = 1,
                        Imagen = i.ToString(),
                        Raza =1,
                        Tipo =1,
                        Activa =true,
                        Descripcion = i.ToString()
                    });

                    databaseContext.SaveChanges();
                }
            }
        }

        public void addRazas(APIDbContext databaseContext) 
        {
            if (databaseContext.Raza.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Raza.Add(new Raza()
                    {
                        Id = i,
                        Nombre = i.ToString()
                        
                    });

                    databaseContext.SaveChanges();
                }
            }
        }
        public void addTipo(APIDbContext databaseContext) 
        {
            if (databaseContext.Tipo.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Tipo.Add(new Tipo()
                    {
                        Id = i,
                        Nombre = i.ToString()

                    });

                    databaseContext.SaveChanges();
                }
            }
        }



    }
}
