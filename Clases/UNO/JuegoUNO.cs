using System;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class JuegoUNO:Juego, IJuego
{
    public bool Sentido { get; set; }
    public int CartasRobar { get; set; }
    public bool SaltoTurno { get; set; }
    
    public void Jugar()
    {
        _barajaJuego = new BarajaUNO();
        _barajaDescartasdas = new BarajaDescarteUNO();

        Random random = new Random();
        int indiceDealer = random.Next(Jugadores.Count);
        IDealer dealerActual = (IDealer)Jugadores[indiceDealer];

        Console.WriteLine($"El jugador: {Jugadores[indiceDealer].NombreJugador} reparte");
        dealerActual.Barajear(_barajaJuego);
        dealerActual.RepartirCarta(Jugadores, _barajaJuego, 4);

        Carta primeraCartaDescarte = ((IJugadorUNO)dealerActual).AgarraCarta(_barajaJuego);
        _barajaDescartasdas.BarajaCartas.Add(primeraCartaDescarte);

        Console.WriteLine("=== EMPIEZA EL JUEGO ===");

        bool sentidoInvertido = false;
        int cartasARobar = 0;
        bool saltarSiguiente = false;

        while (NumeroRondasJugadas < NumeroRondas)
        {
            bool partidaTerminada = false;
            
            while (!partidaTerminada)
            {
                for (int i = 0; i < Jugadores.Count; i++)
                {
                    int indice = sentidoInvertido ? (Jugadores.Count - 1 - i) : i;
                    Jugador jugadorActual = Jugadores[indice];
                    IJugadorUNO jugadorUNO = (IJugadorUNO)jugadorActual;

                    if (saltarSiguiente)
                    {
                        Console.WriteLine($"{jugadorActual.NombreJugador} pierde su turno (Nojuegas)");
                        Console.WriteLine(); 
                        saltarSiguiente = false;
                        continue;
                    }

                    Console.WriteLine($"Turno de: {jugadorActual.NombreJugador}");
                    Console.WriteLine();
                    Console.WriteLine($"Cartas en mano: {jugadorActual.ManoJugador.ManoCartas.Count}");

                    Carta cartaEnJuego = _barajaDescartasdas.BarajaCartas[_barajaDescartasdas.BarajaCartas.Count - 1];
                    Console.WriteLine($"Carta en juego: {cartaEnJuego.Color} {cartaEnJuego.Valor}");

                    if (cartasARobar > 0)
                    {
                        Console.WriteLine($"→ Debe robar {cartasARobar} cartas");
                        for (int j = 0; j < cartasARobar; j++)
                        {
                            if (_barajaJuego.BarajaCartas.Count == 0)
                            {
                                Carta ultimaCartaDescarte = _barajaDescartasdas.BarajaCartas[_barajaDescartasdas.BarajaCartas.Count - 1];
                                _barajaDescartasdas.BarajaCartas.RemoveAt(_barajaDescartasdas.BarajaCartas.Count - 1);
                                _barajaJuego.BarajaCartas = new List<Carta>(_barajaDescartasdas.BarajaCartas);
                                _barajaDescartasdas.BarajaCartas.Clear();
                                _barajaDescartasdas.BarajaCartas.Add(ultimaCartaDescarte);
                                IDealer dealer = (IDealer)jugadorActual;
                                dealer.Barajear(_barajaJuego);
                            }
                            Carta cartaRobada = jugadorUNO.AgarraCarta(_barajaJuego);
                            jugadorActual.ManoJugador.ManoCartas.Add(cartaRobada);
                        }
                        cartasARobar = 0;
                        continue;
                    }

                    Carta cartaElegida = jugadorUNO.TomarDecision(cartaEnJuego);
                    
                    bool jugoCarta = false;
                    Carta? cartaJugada = null;

                    if (cartaElegida != null)
                    {
                        if (cartaElegida is IJugable)
                        {
                            IJugable jugable = (IJugable)cartaElegida;
                            if (jugable.EsJugable(cartaEnJuego))
                            {
                                cartaJugada = cartaElegida;
                                jugoCarta = true;
                            }
                        }
                    }

                    if (!jugoCarta)
                    {
                        foreach (var carta in jugadorActual.ManoJugador.ManoCartas.ToList())
                        {
                            if (carta is IJugable)
                            {
                                IJugable jugable = (IJugable)carta;
                                if (jugable.EsJugable(cartaEnJuego))
                                {
                                    cartaJugada = carta;
                                    jugoCarta = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (!jugoCarta)
                    {
                        Console.WriteLine("→ No tiene cartas válidas, comienza a robar...");

                        while (!jugoCarta)
                        {
                            if (_barajaJuego.BarajaCartas.Count == 0)
                            {
                                Carta ultimaCartaDescarte = _barajaDescartasdas.BarajaCartas[_barajaDescartasdas.BarajaCartas.Count - 1];
                                _barajaDescartasdas.BarajaCartas.RemoveAt(_barajaDescartasdas.BarajaCartas.Count - 1);
                                _barajaJuego.BarajaCartas = new List<Carta>(_barajaDescartasdas.BarajaCartas);
                                _barajaDescartasdas.BarajaCartas.Clear();
                                _barajaDescartasdas.BarajaCartas.Add(ultimaCartaDescarte);
                                IDealer dealer = (IDealer)jugadorActual;
                                dealer.Barajear(_barajaJuego);
                            }

                            Carta cartaRobada = jugadorUNO.AgarraCarta(_barajaJuego);
                            jugadorActual.ManoJugador.ManoCartas.Add(cartaRobada);
                            Console.WriteLine($"→ Robó: {cartaRobada.Color} {cartaRobada.Valor}");

                            if (cartaRobada is IJugable)
                            {
                                IJugable jugable = (IJugable)cartaRobada;
                                if (jugable.EsJugable(cartaEnJuego))
                                {
                                    cartaJugada = cartaRobada;
                                    jugoCarta = true;
                                }
                            }
                        }
                    }

                    jugadorActual.ManoJugador.ManoCartas.Remove(cartaJugada);
                    _barajaDescartasdas.BarajaCartas.Add(cartaJugada);
                    Console.WriteLine($"→ Jugó: {cartaJugada.Color} {cartaJugada.Valor}");
                    Console.WriteLine();
                    if (jugadorActual.ManoJugador.ManoCartas.Count == 1)
                    {
                        Console.WriteLine($"→ {jugadorActual.NombreJugador}: ¡UNO!");
                    }

                    if (cartaJugada is CartaEspecialUNO)
                    {
                        CartaEspecialUNO especial = (CartaEspecialUNO)cartaJugada;
                        if (especial is CartaReversa)
                        {
                            sentidoInvertido = !sentidoInvertido;
                            Console.WriteLine("→ Efecto: REVERSA");
                            Console.WriteLine();
                        }
                        else if (especial is CartaNoJuegas)
                        {
                            saltarSiguiente = true;
                            Console.WriteLine("→ Efecto: SALTO");
                            Console.WriteLine();
                        }
                        else if (especial is CartaComeDos)
                        {
                            cartasARobar += 2;
                            Console.WriteLine("→ Efecto: +2");
                            Console.WriteLine();
                        }
                        else if (especial is CartaCome4)
                        {
                            cartasARobar += 4;
                            especial.EfectoCarta(this);
                            Console.WriteLine($"→ Efecto: +4 (Nuevo color: {especial.Color})");
                            Console.WriteLine();
                        }
                        else if (especial is CartaComodin)
                        {
                            especial.EfectoCarta(this);
                            Console.WriteLine($"→ Efecto: Comodín (Nuevo color: {especial.Color})");
                            Console.WriteLine();
                        }
                    }

                    if (jugadorActual.ManoJugador.ManoCartas.Count == 0)
                    {
                        jugadorActual.Puntos++;
                        Console.WriteLine($"{jugadorActual.NombreJugador} gano la ronda");
                        Console.WriteLine();

                        NumeroRondasJugadas++;
                        partidaTerminada = true;

                        if (NumeroRondasJugadas < NumeroRondas)
                        {
                            Console.WriteLine($"RONDA {NumeroRondasJugadas + 1}");
                            
                            foreach (var jugador in Jugadores)
                            {
                                jugador.ManoJugador.ManoCartas.Clear();
                            }

                            _barajaJuego = new BarajaUNO();
                            _barajaDescartasdas = new BarajaDescarteUNO();

                            int nuevoIndiceDealer = random.Next(Jugadores.Count);
                            IDealer nuevoDealer = (IDealer)Jugadores[nuevoIndiceDealer];
                            nuevoDealer.Barajear(_barajaJuego);
                            nuevoDealer.RepartirCarta(Jugadores, _barajaJuego, 7);

                            Carta nuevaCartaDescarte = ((IJugadorUNO)nuevoDealer).AgarraCarta(_barajaJuego);
                            _barajaDescartasdas.BarajaCartas.Add(nuevaCartaDescarte);

                            sentidoInvertido = false;
                            cartasARobar = 0;
                            saltarSiguiente = false;
                        }
                        break;
                    }
                }
            }
        }
        
        Console.WriteLine("\n=== JUEGO TERMINADO ===");
        Jugador ganador = Jugadores[0];
        for (int i = 1; i < Jugadores.Count; i++)
        {
            if (Jugadores[i].Puntos > ganador.Puntos)
            {
                ganador = Jugadores[i];
            }
        }
        Console.WriteLine($"GANADOR: {ganador.NombreJugador} con {ganador.Puntos} puntos");
    }

    public Juego InicializarJuego()
    {
        bool inputValido = false;
        int inputRondas = 0;
        var JuegoCreado = new JuegoUNO();
        Console.WriteLine("¿Cuantas rondas quieres jugar?");
        string? input = Console.ReadLine();
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
