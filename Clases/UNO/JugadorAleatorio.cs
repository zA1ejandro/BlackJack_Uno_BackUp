using System;
using BlackJack_Uno_BackUp.Clases.UNO;
using BlackJack_Uno_BackUp.Interfaces;
namespace BlackJack_Uno_BackUp.Clases.UNO;

class JugadorAleatorio:Jugador,IJugadorUNO
{
    public override string NombreJugador
    {
        get { return _nombreJugador; } // no es necesario que el get y el set evaluen nada especial
        set { _nombreJugador = value; }
    }

    public override int Puntos
    {
        get { return _puntos; }
        set { _puntos = value; }
    }

    public JugadorAleatorio(string nombre) : base(nombre)
    {
        Puntos = 0;
        // se genera un conflicto si se inicializa la mano aqui ... preguntar
    }

    public Carta TomarDecision()
    {
        Random cartaSeleccionada = new Random();
        int index = cartaSeleccionada.Next(ManoJugador.ManoCartas.Count);
        return ManoJugador.ManoCartas[index];
    }
}