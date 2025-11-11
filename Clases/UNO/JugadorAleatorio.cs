using System;
using BlackJack_Uno_BackUp.Clases.UNO;
using BlackJack_Uno_BackUp.Interfaces;
namespace BlackJack_Uno_BackUp.Clases.UNO;

class JugadorAleatorio:Jugador,IJugadorUNO
{
    public override string NombreJugador
    {
        get { return _nombreJugador; } 
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
        ManoJugador = new ManoUNO();
    }

    public Carta TomarDecision()
    {
        Random cartaSeleccionada = new Random();
        int index = cartaSeleccionada.Next(0,ManoJugador.ManoCartas.Count);
        return ManoJugador.ManoCartas[index];
    }
}