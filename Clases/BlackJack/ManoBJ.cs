using System;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class ManoBJ:Mano, IMano
{
    public override List<Carta> ManoCartas
    {
        get { return _ManoCartas; }
        set { _ManoCartas = value; }
    }
    
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

    public List<Carta> VaciarMano()
    {
        List<Carta> cartasADevolver = new List<Carta>(ManoCartas);
        ManoCartas.Clear();
        return cartasADevolver;
    }
}
