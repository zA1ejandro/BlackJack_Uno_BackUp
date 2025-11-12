using System;
using BlackJack_Uno_BackUp.Clases;
using BlackJack_Uno_BackUp.Clases.UNO;
using BlackJack_Uno_BackUp.Clases.BlackJack;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=====Bienvenido al simulador de juegos :(=====");
        Console.WriteLine("Eliga el juego que desee simular:");
        Console.WriteLine("1.UNO");
        Console.WriteLine("2.Black Jack");
        Console.WriteLine("Eliga:");
        string? input = Console.ReadLine();
        int opcion = int.Parse(input);
        switch (opcion)
        {
            case 1:
                JuegoUNO juego = new JuegoUNO();
                Juego juegoConfigurado=juego.InicializarJuego();
                ((IJuego)juegoConfigurado).Jugar();
                Console.WriteLine("Gracias por simular XD");
                break;
            case 2:
                JuegoBJ juego2 = new JuegoBJ();
                juego2.InicializarJuego();
                juego2.Jugar();
                Console.WriteLine("Gracias por simular XD");
                break;
            default:
                Console.WriteLine("uuuuuuu esta mal, esta mal en algo");
                break;
        }
    }
}