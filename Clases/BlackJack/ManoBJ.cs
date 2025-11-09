using System;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class ManoBJ:Mano, IMano
{
    public override List<Carta> ManoCartas
    public void AgregarCarta(Carta carta)
    {
        ManoCartas.Add(carta);
    }

    public Carta QuitarCarta(Carta carta)
    {
        if (!ManoCartas.Contains(carta))
        {
             throw new Exception("La carta no se encuentra en la mano");
        }
        
        ManoCartas.Remove(carta);
        return carta;
    }
   
}
