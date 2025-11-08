using System;

namespace BlackJack_Uno_BackUp.Clases;

abstract class Baraja
{
    protected List<Carta> _barajaCartas = new List<Carta>();
    public abstract List<Carta> BarajaCartas{ get; set; }
}
