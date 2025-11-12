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


        public JugadorDealer() : base("Dealer")
        {
            NombreJugador = "Dealer";
            ManoJugador = new ManoBJ();
        }

        public bool TomarDecision()
        {
            int puntosActuales = CalcularPuntos();
            return puntosActuales < 17;
        }

        public  void RecibirCarta(Carta carta)
        {
            if (ManoJugador is IMano mano)
            {
                mano.AgregarCarta(carta);
                return;
            }
            throw new Exception("La mano del jugador no implementa IMano");
        }
