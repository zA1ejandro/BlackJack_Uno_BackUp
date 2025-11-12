using System;
using BlackJack_Uno_BackUp.Clases;
namespace BlackJack_Uno_BackUp.Interfaces;

interface IDealer
{
    public void Barajear(Baraja barajaBarajear);
    public void RepartirCarta(List<Jugador> jugadores,Baraja barajaRepartir,int numeroCartas);
    public void RecibirCartas(List<Jugador> jugadores);
}
