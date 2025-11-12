using System;
using BlackJack_Uno_BackUp.Clases;

namespace BlackJack_Uno_BackUp.Interfaces;

interface IJugadorBJ
{
    public bool TomarDecision();
    public void RecibirCarta(Carta carta);
    public int CalcularPuntos();
}
