using System;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class CartaBJ:Carta
{
    public override Colores Color
    {
        get { return _color; }
        set
        {
            if (value!=Colores.Negro && value!=Colores.Rojo)
            {
                throw new Exception("Las cartas de blackjack deben de ser rojas o negras");
            }
            _color = value;
        }
    }

    public override int Valor
    {
        get { return _valor; }
        set
        {
            if (value < 2 || value > 10)
            {
                throw new Exception("Una carta de blackjack normal debe valer entre 2 y 10");
            }
            _valor = value;
        }
    }

    public enum Figuras
    {
        Pica, Trebol, Diamante, Corazon
    }

    protected Figuras _figura;
    public Figuras Figura
    {
        get { return _figura; }
        set{ _figura = value; }
    }
    public CartaBJ(int valorCarta,Colores colorCarta,Figuras figuraCarta) : base(valorCarta, colorCarta)
    {
        Figura = figuraCarta;
    }
}
