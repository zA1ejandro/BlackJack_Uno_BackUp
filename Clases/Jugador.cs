using System;

namespace BlackJack_Uno_BackUp.Clases;

abstract class Jugador
{
    protected string _nombreJugador;
    public abstract string NombreJugador { get; set; }

    protected int _puntos;
    public abstract int Puntos{ get; set; }
    protected Mano _manoJugador;
    public Mano ManoJugador{ get; set; }
    
    public Jugador(string Nombre)
    {
        NombreJugador = Nombre;
        Puntos = 0;
    }
}
