using System;

namespace BlackJack_Uno_BackUp.Clases;

abstract class Carta
{
    protected int _valor;
    public abstract int Valor { get; set; }

    public enum Colores
    {
        Azul, Verde, Amarillo, Rojo, Negro
    }
    protected Colores _color;
    public abstract Colores Color { get; set; }

    protected Carta(Colores colorCarta)
    {
        Color = colorCarta;
    }
}
