using System;

using BlackJack_Uno_BackUp.Clases;
namespace BlackJack_Uno_BackUp.Interfaces;

interface IJugable
{
    public bool EsJugable(Carta CartaVerificar);
}
