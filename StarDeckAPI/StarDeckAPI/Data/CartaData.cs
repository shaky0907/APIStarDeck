﻿
using StarDeckAPI.Models;
using StarDeckAPI.Data;
using StarDeckAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace StarDeckAPI.Data
{

    public class CartaData
    {
        private APIDbContext apiDBContext;
        private Random random = new Random();
        public CartaData(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }
        public Carta guardarCartaDB(CartaAPI cartaAPI)
        {
            Carta carta = new Carta()
            {
                Id = GeneratorID.GenerateRandomId("C-"),
                N_Personaje = cartaAPI.Nombre,
                Energia = cartaAPI.Energia,
                C_batalla = cartaAPI.Costo,
                Imagen = cartaAPI.Imagen,
                Raza = apiDBContext.Raza.ToList().Where(x => x.Nombre == cartaAPI.Raza).First().Id,
                Tipo = apiDBContext.Tipo.ToList().Where(x => x.Nombre == cartaAPI.Tipo).First().Id,
                Activa = cartaAPI.Estado,
                Descripcion = cartaAPI.Descripcion
            };
            apiDBContext.Add(carta);
            apiDBContext.SaveChanges();

            return carta;
        }

        public Carta deleteCartaDB(string Id)
        {
            Carta carta = apiDBContext.Carta.ToList().Where(x => x.Id == Id).First();
            apiDBContext.Remove(carta);
            apiDBContext.SaveChanges();
            return carta;
        }

        public CartaAPI getCartaDB(string Id)
        {
            Carta carta = apiDBContext.Carta.ToList().Where(x => x.Id == Id).First();
            CartaAPI cApi = new CartaAPI()
            {
                Id = carta.Id,
                Nombre = carta.N_Personaje,
                Energia = carta.Energia,
                Costo = carta.C_batalla,
                Imagen = carta.Imagen,
                Raza = apiDBContext.Raza.ToList().Where(x => x.Id == carta.Raza).First().Nombre,
                Tipo = apiDBContext.Tipo.ToList().Where(x => x.Id == carta.Tipo).First().Nombre,
                Estado = carta.Activa,
                Descripcion = carta.Descripcion
            };

            return cApi;
        }

        public List<CartaAPI> getAllCartas()
        {
            List<Carta> cartas = apiDBContext.Carta.ToList();
            List<CartaAPI> cartasReturn = new List<CartaAPI>();

            foreach (Carta carta in cartas)
            {
                CartaAPI cApi = new CartaAPI()
                {
                    Id = carta.Id,
                    Nombre = carta.N_Personaje,
                    Energia = carta.Energia,
                    Costo = carta.C_batalla,
                    Imagen = carta.Imagen,
                    Raza = apiDBContext.Raza.ToList().Where(x => x.Id == carta.Raza).First().Nombre,
                    Tipo = apiDBContext.Tipo.ToList().Where(x => x.Id == carta.Tipo).First().Nombre,
                    Estado = carta.Activa,
                    Descripcion = carta.Descripcion
                };

                cartasReturn.Add(cApi);
            }

            return cartasReturn;
        }


        private List<Carta> GenerateRandomCartas(List<Carta> inputCards, int count)
        {
            if (inputCards.Count() < count)
            {
                throw new ArgumentException("The input list must contain at least 15 cards.");
            }

            List<Carta> selectedCards = new List<Carta>();

            while (selectedCards.Count() < count)
            {
                int randomIndex = random.Next(inputCards.Count());
                Carta randomCard = inputCards[randomIndex];

                if (!selectedCards.Contains(randomCard))
                {
                    selectedCards.Add(randomCard);
                }
            }

            return selectedCards;
        }

        public List<CartaAPI> getCartasNuevoDeck()
        {
            List<Carta> cartasBasicas = apiDBContext.Carta.ToList().Where(x => x.Tipo == 5).ToList();

            List<Carta> cartasTotales = GenerateRandomCartas(cartasBasicas, 15);

            List<Carta> cartasrn = apiDBContext.Carta.ToList().Where(x => (x.Tipo == 4) || (x.Tipo == 3)).ToList();

            List<Carta> cartasrestantes = GenerateRandomCartas(cartasrn, 9);

            cartasTotales.AddRange(cartasrestantes);

            List<CartaAPI> cartasReturn = new List<CartaAPI>();

            foreach (Carta carta in cartasTotales)
            {
                CartaAPI cApi = new CartaAPI()
                {
                    Id = carta.Id,
                    Nombre = carta.N_Personaje,
                    Energia = carta.Energia,
                    Costo = carta.C_batalla,
                    Imagen = carta.Imagen,
                    Raza = apiDBContext.Raza.ToList().Where(x => x.Id == carta.Raza).First().Nombre,
                    Tipo = apiDBContext.Tipo.ToList().Where(x => x.Id == carta.Tipo).First().Nombre,
                    Estado = carta.Activa,
                    Descripcion = carta.Descripcion
                };

                cartasReturn.Add(cApi);
            }

            return cartasReturn;
        }


    }
}
