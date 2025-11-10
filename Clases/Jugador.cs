using System;

namespace BlackJack_Uno_BackUp.Clases;

abstract class Jugador
{
    protected string _nombreJugador;
    public abstract string NombreJugador { get; set; }
    
    protected Mano ManoJugador { get;}
    
    public Jugador(string Nombre)
    {
        NombreJugador = Nombre;
    }
}
