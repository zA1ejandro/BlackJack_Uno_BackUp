using System;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class BarajaDescarteBJ:Baraja
{
    public override List<Carta> BarajaCartas
    {
        get { return _barajaCartas; }
        set{ _barajaCartas = value; }
    }
}
