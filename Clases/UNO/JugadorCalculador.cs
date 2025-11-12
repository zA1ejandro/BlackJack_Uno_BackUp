using System;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class JugadorCalculador : Jugador, IDealer, IJugadorUNO
{
    public override string NombreJugador
    {
        get { return _nombreJugador; }
        set { _nombreJugador = value; }
    }

    public override int Puntos
    {
        get { return _puntos; }
        set { _puntos = value; }
    }

    public JugadorCalculador(string nombre) : base(nombre)
    {
        ManoJugador = new ManoUNO();
    }

    public void Barajear(Baraja baraja)
    {
        Random random = new Random();
        int n = baraja.BarajaCartas.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Carta carta = baraja.BarajaCartas[k];
            baraja.BarajaCartas[k] = baraja.BarajaCartas[n];
            baraja.BarajaCartas[n] = carta;
        }
    }

    public void RepartirCarta(List<Jugador> jugadores, Baraja baraja, int cantidadCartas)
    {
        for (int i = 0; i < cantidadCartas; i++)
        {
            foreach (var jugador in jugadores)
            {
                if (baraja.BarajaCartas.Count == 0)
                {
                    throw new Exception("No hay suficientes cartas en la baraja para repartir");
                }
                Carta carta = AgarraCarta(baraja);
                jugador.ManoJugador.ManoCartas.Add(carta);
            }
        }
    }

    public Carta AgarraCarta(Baraja baraja)
    {
        if (baraja.BarajaCartas.Count == 0)
        {
            throw new Exception("No hay cartas en la baraja");
        }
        Carta carta = baraja.BarajaCartas[0];
        baraja.BarajaCartas.RemoveAt(0);
        return carta;
    }

    public Carta TomarDecision(Carta cartaEnJuego)
    {
        
        List<Carta> cartasNormalesJugables = new List<Carta>();
        List<Carta> cartasEspecialesJugables = new List<Carta>();

        foreach (var carta in ManoJugador.ManoCartas)
        {
            IJugable jugable = (IJugable)carta;
            if (jugable.EsJugable(cartaEnJuego))
            {
                if (carta is CartaEspecialUNO)
                {
                    cartasEspecialesJugables.Add(carta);
                }
                else
                {
                    cartasNormalesJugables.Add(carta);
                }
            }
        }
        if (cartasNormalesJugables.Count > 0)
        {
            return cartasNormalesJugables[0];
        }
        if (cartasEspecialesJugables.Count > 0)
        {
            foreach (var carta in cartasEspecialesJugables)
            {
                if (carta is CartaCome4)
                {
                    return carta;
                }
            }

            foreach (var carta in cartasEspecialesJugables)
            {
                if (carta is CartaComeDos)
                {
                    return carta;
                }
            }
            return cartasEspecialesJugables[0];
        }

        return null;
    }

    public void RecibirCartas(List<Jugador> jugadores)
    {
        throw new NotImplementedException();
    }
}