using System;
using System.Linq;
using System.Collections.Generic;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class JuegoBJ : Juego, IJuego
{
    public JugadorDealer Dealear { get; set; } // puede 

    public Juego InicializarJuego()
    {
        var juego = new JuegoBJ();
        juego.Dealear = new JugadorDealer();
        juego.Jugadores.Add(juego.Dealear);

        Console.WriteLine("¿Cuantas rondas desea jugar?");
        try
        {
            string rondas = Console.ReadLine();
            juego.NumeroRondas = int.Parse(rondas);
        }
        catch (Exception)
        {
            Console.WriteLine("Entrada no valida, se asignara 1 ronda por defecto.");
            juego.NumeroRondas = 1;
        }
        Console.WriteLine("¿Cuantos jugadores (adicionales al dealer) participaran?");
        try
        {
            string input = Console.ReadLine();
            int numJugadores = int.Parse(input);
        }
        catch (Exception)
        {
            Console.WriteLine("Entrada no valida, se asignara 1 jugador por defecto.");
            numJugadores = 1;
        }


        for (int i = 0; i < numJugadores; i++)
        {
            Console.WriteLine($"Nombre del jugador {i + 1}:");
            string nombre = Console.ReadLine();

            Console.WriteLine($"Que tipo de jugador es {nombre}? (1: Cauteloso, 2: Temerario)");
            string tipo = Console.ReadLine();
            
            IJugadorBJ nuevoJugador;
            switch (tipo)
            {
                case "1":
                    nuevoJugador = new JugadorCauteloso(nombre);
                    break;
                case "2":
                    nuevoJugador = new JugadorTemerario(nombre);
                    break;
                default:
                    Console.WriteLine("Tipo de jugador no valido, se asignara como Cauteloso.");
                    nuevoJugador = new JugadorCauteloso(nombre);
                    break;
            }
            juego.Jugadores.Add((Jugador)nuevoJugador);
        }

        juego._barajaJuego = new BarajaBJ();
        return juego;
    }

    public JugadorDealer dealer;
    private int numJugadores;

    public void Jugar()
    {
        for (int i = 0; i < NumeroRondas; i++)
        {
            Console.WriteLine($"--- Ronda {i + 1} ---");
            RepartirCartas();

            foreach (var jugador in Jugadores)
            {
                if (jugador is IJugadorBJ jugadorBJ && jugador != dealer)
                {
                    TurnoJugador(jugadorBJ);
                }
            }

            TurnoDealer();
            DeterminarGanador();
            LimpiarMesa();
        }
    }

    public void RepartirCartas() // checar
    {
        dealer.Barajear(_barajaJuego);
        dealer.RepartirCartas(Jugadores, _barajaJuego);
    }

    public void TurnoJugador(IJugadorBJ jugador)
    {
        while (jugador.TomarDecision())
        {
            Console.WriteLine($"{((Jugador)jugador).NombreJugador} pide una carta.");
            Carta carta = _barajaJuego.BarajaCartas[0];
            _barajaJuego.BarajaCartas.RemoveAt(0);
            jugador.RecibirCarta(carta);
            
            int puntos = jugador.CalcularPuntos();
            Console.WriteLine($"Puntos de {((Jugador)jugador).NombreJugador}: {puntos}");
            if (puntos > 21)
            {
                Console.WriteLine($"{((Jugador)jugador).NombreJugador} se ha pasado de 21.");
                break;
            }
        }
    }

    private void TurnoDealer()
    {
        while (dealer.TomarDecision())
        {
            Console.WriteLine("El dealer pide una carta.");
            Carta carta = _barajaJuego.BarajaCartas[0];
            _barajaJuego.BarajaCartas.RemoveAt(0);
            dealer.RecibirCarta(carta);
        }
    }

    private void DeterminarGanador()
    {

        int puntosDealer = dealer.CalcularPuntos();
        Console.WriteLine($"\nPuntos del Dealer: {puntosDealer}");
        foreach (var jugador in Jugadores)
        {

            if (jugador != dealer && jugador is IJugadorBJ jugadorBJ)
            {

                int puntosJugador = jugadorBJ.CalcularPuntos();
                Console.WriteLine($"Puntos de {jugador.NombreJugador}: {puntosJugador}");

                if (puntosJugador > 21)
                {
                    Console.WriteLine($"{jugador.NombreJugador} pierde.");
                }
                else if (puntosDealer > 21 || puntosJugador > puntosDealer)
                {
                    Console.WriteLine($"{jugador.NombreJugador} gana.");
                }
                else if (puntosJugador < puntosDealer)
                {
                    Console.WriteLine($"{jugador.NombreJugador} pierde.");
                }
                else
                {
                    Console.WriteLine($"{jugador.NombreJugador} empata.");
                }
            }
        }
    }

    private void LimpiarMesa()
    {
        foreach (var jugador in Jugadores)
        {
            jugador.ManoJugador.ManoCartas.Clear();
        }
        _barajaJuego = new BarajaBJ();
    }

    public JuegoBJ() : base()
    {
        dealer = new JugadorDealer();
        Jugadores.Add(dealer);
    }
}