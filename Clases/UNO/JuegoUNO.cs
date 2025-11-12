using System;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class JuegoUNO:Juego, IJuego
{
    public bool Sentido { get; set; }
    public int CartasRobar { get; set; }
    public bool SaltoTurno{ get; set; }
    public void Jugar()
    {
        _barajaJuego = new BarajaUNO();
        _barajaDescartasdas = new BarajaDescarteUNO();

        Random random = new Random();
        int dealer = random.Next(0, Jugadores.Count);
        Jugadores[dealer].Barajear(_barajaJuego);
        Jugadores[dealer].RepartirCarta(Jugadores, _barajaJuego, 4);
        var cartaInicial = Jugadores[dealer].AgarrarCarta(_barajaJuego);
        _barajaDescartasdas.BarajaCartas.Add(cartaInicial);

        Console.WriteLine("Empieza el juego");
        while (NumeroRondasJugadas < NumeroRondas)
        {
            Console.WriteLine($"Ronda {NumeroRondasJugadas}");
            
        }


    }

    public Juego InicializarJuego()
    {
        bool inputValido = false;
        int inputRondas = 0;
        var JuegoCreado = new JuegoUNO();
        Console.WriteLine("¿Cuantas rondas quieres jugar?");
        string input = Console.ReadLine();
        while (!inputValido)
        {
            if (int.TryParse(input, out inputRondas))
            {
                inputValido = true;
            }
            else
            {
                Console.WriteLine("El valor no es valido, intente de nuevo");
                input = Console.ReadLine();
            }
        }
        JuegoCreado.NumeroRondas = inputRondas;
        inputValido = false;
        Console.WriteLine("¿Cuantos jugadores aleatorios quieres? (Maximo 10)");
        int JugadoresAleatorios = 0;
        input = Console.ReadLine();
        while (!inputValido)
        {
            if (int.TryParse(input, out JugadoresAleatorios) && JugadoresAleatorios >= 0 && JugadoresAleatorios <= 10)
            {
                inputValido = true;
            }
            else
            {
                Console.WriteLine("El valor no es valido, intente de nuevo");
                input = Console.ReadLine();
            }
        }
    
        inputValido = false;
        int maxCalculadores = 10 - JugadoresAleatorios;
        Console.WriteLine($"¿Cuantos jugadores calculadores quieres? (Maximo {maxCalculadores})");
        int JugadoresCalculadores = 0;
        input = Console.ReadLine();
        while (!inputValido)
        {
            if (int.TryParse(input, out JugadoresCalculadores) && JugadoresCalculadores >= 0 && JugadoresCalculadores <= maxCalculadores)
            {
                inputValido = true;
            }
            else
            {
                Console.WriteLine("El valor no es valido, intente de nuevo");
                input = Console.ReadLine();
            }
        }
    
        int JugadoresTotales = JugadoresAleatorios + JugadoresCalculadores;

        if (JugadoresTotales < 2)
        {
            Console.WriteLine("Necesitas al menos 2 jugadores para jugar. Se crearán 2 jugadores por defecto.");
            JugadoresAleatorios = 2;
            JugadoresCalculadores=0;
        }
        
        for (int i = 0; i < JugadoresAleatorios; i++)
        {
            JuegoCreado.Jugadores.Add(new JugadorAleatorio($"Aleatorio {i + 1}"));
        }
    
        for (int i = 0; i < JugadoresCalculadores; i++)
        {
            JuegoCreado.Jugadores.Add(new JugadorCalculador($"Calculador {i + 1}"));
        }
        return JuegoCreado;
    }
}
