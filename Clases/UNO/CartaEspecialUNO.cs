using System;

namespace BlackJack_Uno_BackUp.Clases.UNO;

abstract class CartaEspecialUNO:CartaUNO
{
    public abstract void EfectoCarta(JuegoUNO juego);
    public CartaEspecialUNO(Colores Color,int valor):base(Color,valor){}
}
