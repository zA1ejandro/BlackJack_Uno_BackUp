using System;
using BlackJack_Uno_BackUp.Clases;
namespace BlackJack_Uno_BackUp.Interfaces;

interface IDealer
{
    public void Barajear(Baraja barajaBarajear);
    // cambiar los parametros opcion baraja baraja
    public void RepartirCarta(List<Jugador> jugadores,Baraja barajaRepartir,int numeroCartas);
    // cambiar los parametros opcion baraja baraja y lista de jugadores
}
