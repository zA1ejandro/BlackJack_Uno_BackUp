using System;

namespace BlackJack_Uno_BackUp.Clases;

abstract class Mano
{
    protected List<Carta> _ManoCartas = new List<Carta>();
    public abstract List<Carta> ManoCartas{ get; set; }
}
