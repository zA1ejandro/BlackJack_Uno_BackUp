using System;

namespace BlackJack_Uno_BackUp.Interfaces;

public interface IDealer
{
    public Baraja Barajear();
    // cambiar los parametros opcion baraja baraja
    public Carta RepartirCarta();
    // cambiar los parametros opcion baraja baraja y lista de jugadores
}
