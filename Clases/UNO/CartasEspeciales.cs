using System;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class CartaReversa : CartaEspecialUNO
{
    public CartaReversa(Colores color, int valor) : base(color, valor) { }

    public override void EfectoCarta(JuegoUNO juego)
    {
        juego.Sentido = !juego.Sentido;
    }
}

