using System;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class BarajaDescarteUNO:Baraja
{
    public override List<Carta> BarajaCartas
    {
        get { return _barajaCartas; }
        set{ _barajaCartas = value; }
    }
}
