using System;
using System.Collections.Generic;
using BlackJack_Uno_BackUp.Clases;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.BlackJack
{

    class JugadorDealer : Jugador, IJugadorBJ, IDealer
    {
        private Random _random = new Random();
        private  BarajaDescarteBJ _barajaDescarte = new BarajaDescarteBJ();

        public override string NombreJugador { get => _nombreJugador; set => _nombreJugador = value; }
        public override int Puntos { get => _puntos; set => _puntos = value; }


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

        public void RecibirCarta(Carta carta)
        {
            if (ManoJugador is IMano mano)
            {
                mano.AgregarCarta(carta);
                return;
            }
            throw new Exception("La mano del jugador no implementa IMano");
        }

        public int CalcularPuntos()
        {
            int suma = 0;
            int ases = 0;
            foreach (var carta in ManoJugador.ManoCartas)
            {
                suma += carta.Valor;
                if (carta is CartasEspecialessBJ cartaEspecial && cartaEspecial.Figura == CartasEspecialessBJ.TipoFigura.As)
                {
                    ases++;
                }
            }

            while (suma > 21 && ases > 0)
            {
                suma -= 10;
                ases--;
            }
            return suma;
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

        public void RepartirCarta(List<Jugador> jugadores, Baraja barajaRepartir, int numeroCartas)
        {
            throw new NotImplementedException();
        }

        public void RecibirCartas(List<Jugador> jugadores)
        {
            foreach (var jugador in jugadores)
            {
                _barajaDescarte.BarajaCartas.AddRange(jugador.ManoJugador.ManoCartas);
                jugador.ManoJugador.ManoCartas.Clear();
            }
        }
    }
}
