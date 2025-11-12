using System;
using BlackJack_Uno_BackUp.Clases;

namespace BlackJack_Uno_BackUp.Interfaces;
interface IJugadorUNO
{
    public Carta TomarDecision();
    public Carta AgarraCarta(Baraja barajaJuego);
}
