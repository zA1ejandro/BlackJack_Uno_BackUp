using System;

namespace BlackJack_Uno_BackUp.Clases;

abstract class Juego
{
    protected int _numeroRondas;
    public int NumeroRondas
    {
        get { return _numeroRondas; }
        set
        {
            if (value < 0)
            {
                throw new Exception("El numero de rondas no puede ser menor que 0");
            }
            _numeroRondas = value;
        }
    }

    protected int _numeroRondasJugadas;
    public int NumeroRondasJugadas
    {
        get { return _numeroRondasJugadas; }
        set
        {
            if (value < 0 || value > NumeroRondas)
            {
                throw new Exception("El numero de rondas jugadas no puede ser negativo o mayor al numero de rondas totales.");
            }
            _numeroRondasJugadas = value;
        }
    }

    protected List<Jugador> _jugadores = new List<Jugador>();
    public List<Jugador> Jugadores
    {
        get { return _jugadores; }
        set{ _jugadores = value; }
    }

    protected Baraja _barajaJuego{ get; set; }
    protected Baraja _barajaDescartasdas{ get; set;}
    
    public Juego(int rondas)
    {
        NumeroRondas = rondas;
        NumeroRondasJugadas = 0;
    }
}
