using System;
using System.Linq;
using System.Collections.Generic;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class JuegoBJ : Juego, IJuego
{
    public Juego InicializarJuego()
    {
        Console.WriteLine("¿Cuantas rondas desea jugar?");
        string? rondas = Console.ReadLine();
        if (!int.TryParse(rondas, out int numRondas) || numRondas <= 0)
        {
            Console.WriteLine("Entrada no valida, se asignara 1 ronda por defecto.");
            NumeroRondas = 1;
        }
        else
        {
            NumeroRondas = numRondas;
        }

        Console.WriteLine("¿Cuantos jugadores (adicionales al dealer) participaran?");
        string? input = Console.ReadLine();
        int numJugadores;
        if (!int.TryParse(input, out numJugadores) || numJugadores <= 0)
        {
            Console.WriteLine("Entrada no valida, se asignara 1 jugador por defecto.");
            numJugadores = 1;
        }

        for (int i = 0; i < numJugadores; i++)
        {
            Console.WriteLine($"Nombre del jugador {i + 1}:");
            string? nombre = Console.ReadLine();
            
            // Validar que el nombre no sea null o vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                nombre = $"Jugador{i + 1}";
                Console.WriteLine($"Nombre no valido, se asignara '{nombre}'");
            }

            Console.WriteLine($"Que tipo de jugador es {nombre}? (1: Cauteloso, 2: Temerario)");
            string? tipo = Console.ReadLine();
            
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
            Jugadores.Add((Jugador)nuevoJugador);
        }

        _barajaJuego = new BarajaBJ();
        return this;
    }

    public JugadorDealer dealer;

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
        dealer.RepartirCarta(Jugadores, _barajaJuego, 2);
    }

    public void TurnoJugador(IJugadorBJ jugador)
    {
        Console.WriteLine();
        Console.WriteLine($"Cartas iniciales {((Jugador)jugador).NombreJugador}:");
        var mano = ((Jugador)jugador).ManoJugador.ManoCartas;
        for (int i = 0; i < mano.Count; i++)
        {
            Console.WriteLine($"  Carta {i + 1}: Valor {mano[i].Valor}");
        }
        
        Console.WriteLine($"Puntos iniciales: {jugador.CalcularPuntos()}");

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
                Console.WriteLine();
                break;
            }
        }
        if (jugador.CalcularPuntos() <= 21)
        {
            Console.WriteLine($"{((Jugador)jugador).NombreJugador} se planta con {jugador.CalcularPuntos()} puntos.");
            Console.WriteLine();
        }
    }

    private void TurnoDealer()
    {
        Console.WriteLine("Cartas iniciales Dealer:");
        var mano = ((Jugador)dealer).ManoJugador.ManoCartas;
        for (int i = 0; i < mano.Count; i++)
        {
            Console.WriteLine($"  Carta {i + 1}: Valor {mano[i].Valor}");
        }
        
        Console.WriteLine($"Puntos iniciales: {dealer.CalcularPuntos()}");
        while (dealer.TomarDecision())
        {
            Console.WriteLine("El dealer pide una carta.");
            Carta carta = _barajaJuego.BarajaCartas[0];
            _barajaJuego.BarajaCartas.RemoveAt(0);
            dealer.RecibirCarta(carta);
            int puntos = dealer.CalcularPuntos();
            Console.WriteLine($"Puntos del dealer: {puntos}");

            if (puntos > 21)
            {
                Console.WriteLine("El dealer se ha pasado de 21.");
                Console.WriteLine();
                break;
            }
        }
        if (dealer.CalcularPuntos() <= 21)
        {
            Console.WriteLine($"El dealer se planta con {dealer.CalcularPuntos()} puntos.");
            Console.WriteLine();
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