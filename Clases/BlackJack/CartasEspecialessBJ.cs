using System;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class CartasEspecialessBJ : CartaBJ
{
    

    public override int Valor
    {
        get { return _valor; }
        set
        {
            if (value < 10 || value > 11)
            {
                throw new Exception("Una carta de blackjack especial debe valer 10 o 11");
            }
            _valor = value;
        }
    }
    
    public enum TipoFigura
    {
        Reina, Rey, Jack, As
    }


    protected CartasEspecialessBJ.TipoFigura _FiguraCarta;
    private CartaBJ.Figuras palo;


    public CartasEspecialessBJ.TipoFigura FiguraSuit
    
    {
        get { return _FiguraCarta; }
        set{ _FiguraCarta = value; }
    }

    
    public CartasEspecialessBJ(int ValorCarta, Colores colorCarta, CartaBJ.Figuras palo) : base(ValorCarta, colorCarta, palo)
    {
        
    }

}