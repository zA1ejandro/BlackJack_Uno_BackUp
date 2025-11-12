using System;
using System.Collections.Generic;
using BlackJack_Uno_BackUp.Clases;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.BlackJack
{

    class JugadorDealer : Jugador, IJugadorBJ, IDealer
    {
        private readonly Random _random = new Random();

        public override string NombreJugador { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Puntos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


