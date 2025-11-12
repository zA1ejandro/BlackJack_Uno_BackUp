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
        public void Barajear(Baraja baraja)
        {
            var cartas = baraja.BarajaCartas;
            for (int i = cartas.Count - 1; i > 0; i--)
            {
                int newPosicion = _random.Next(i + 1);
                var temp = cartas[i];
                cartas[i] = cartas[newPosicion];
                cartas[newPosicion] = temp;
            }
        }

        public void RepartirCartas(List<Jugador> jugadores, Baraja baraja)
        {
            foreach (var jugador in jugadores)
            {
                jugador.RecibirCarta(baraja.Repartir());
                jugador.RecibirCarta(baraja.Repartir());
            }
        }

        public void RepartirCarta(List<Jugador> jugadores, Baraja barajaRepartir, int numeroCartas)
        {
            throw new NotImplementedException();
        }
    }
}
