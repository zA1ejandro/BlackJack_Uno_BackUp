using System;
using BlackJack_Uno_BackUp.Interfaces;
using BlackJack_Uno_BackUp.Clases;
namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class JuegoBJ : Juego, IJuego
{
    public void Jugar()
    {

    }


    public Juego InicializarJuego()
    {
        var juegoCreado = new JuegoBJ();
        Console.WriteLine("¿Cuantas rondas desea jugar?");
        string rondas = Console.ReadLine();
        int rondasInt = int.Parse(rondas);
        int Rondas = rondasInt;

        return juegoCreado;


    }

    public JuegoBJ() : base()
    {
        

    }

}
// JUGADORES ESPECIALE cARTAS ESPECIALES Y FLUJO DEL JUEGO DE UNO Y BJ
// QUE TENEMOs DEF CARTAS NORMALES BARJAS MANOS,CLASEE BASE JUGADOR Y CLASE BASE DEL JUEGO
// QUE VAMOS A HACER  1- HACER JUGADORES ESPECIALES , 2.- CARTAS ESPECIALES ( TIE4MPO ESTIMADO 2 HRS) 3.- FLUJO DEL JUEGO DE CADA UNO Y CHECar QUE TODO FUNCIONE ()