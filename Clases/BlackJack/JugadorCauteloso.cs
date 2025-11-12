using BlackJack_Uno_BackUp.Clases.BlackJack;
using BlackJack_Uno_BackUp.Interfaces;
using System;
using System.Collections.Generic;

namespace BlackJack_Uno_BackUp.Clases
{
    class JugadorCauteloso : Jugador, IJugadorBJ
    {
        private  int _puntoDeCorte;

        public override string NombreJugador { get => _nombreJugador; set => _nombreJugador = value; }
        public override int Puntos { get => _puntos; set => _puntos = value; }

        public JugadorCauteloso(string nombre, int puntoDeCorte) : base(nombre)
        {
            _puntoDeCorte = puntoDeCorte;
            ManoJugador = new ManoBJ();
        }

        public JugadorCauteloso(string Nombre) : base(Nombre)
        {
            _puntoDeCorte = 17;
            ManoJugador = new ManoBJ();
        }


        public bool TomarDecision()
        {
            return CalcularPuntos() < _puntoDeCorte;
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
                if (carta is CartasEspecialessBJ cartaEspecial && cartaEspecial.FiguraSuit == CartasEspecialessBJ.TipoFigura.As)
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
    }
}
