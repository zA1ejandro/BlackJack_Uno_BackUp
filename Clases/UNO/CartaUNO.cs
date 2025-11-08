using System;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class CartaUNO:Carta,IJugable
{
    public override Colores Color
    {
        get { return _color; }
        set { _color = value; }
    }
    
    public override int Valor
    {
        get { return _valor; }
        set
        {
            if (value < 0 || value > 9)
            {
                throw new Exception("Una carta normal no puede valer mas de 9 ni menos de 0");
            }
            _valor = value;
        }
    }

    public bool EsJugable(Carta cartaVerificar)
    {
        if (this.Color != cartaVerificar.Color && this.Valor != cartaVerificar.Valor)
        {
            return false;
        }
        return true;
    }

    public CartaUNO(Colores color, int valorCarta) : base(valorCarta, color){}
}
