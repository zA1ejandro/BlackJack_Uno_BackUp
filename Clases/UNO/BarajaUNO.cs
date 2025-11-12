using System;
using System.ComponentModel;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class BarajaUNO:Baraja,ICrearBaraja
{
    public override List<Carta> BarajaCartas
    {
        get { return _barajaCartas; }
        set
        {
            if (value.Count > 108)
            {
                throw new Exception("La baraja de UNO no puede ser de mas de 108 cartas");
            }
            _barajaCartas = value;
        }
    }

    private static readonly Carta.Colores[] ColoresCartasNormales =
    {
        Carta.Colores.Azul,Carta.Colores.Verde,Carta.Colores.Amarillo,Carta.Colores.Rojo
    };

    public List<Carta> CrearBaraja()
    {
        List<Carta> BarajaCreada = new List<Carta>();
        foreach (var Colores in ColoresCartasNormales)
        {
            BarajaCreada.Add(new CartaUNO(Colores, 0));
            for (int i = 1; i < 10; i++)
            {
                BarajaCreada.Add(new CartaUNO(Colores, i));
                BarajaCreada.Add(new CartaUNO(Colores, i));
            }

            BarajaCreada.Add(new CartaReversa(Colores));
            BarajaCreada.Add(new CartaReversa(Colores));
            BarajaCreada.Add(new CartaNoJuegas(Colores));
            BarajaCreada.Add(new CartaNoJuegas(Colores));
            BarajaCreada.Add(new CartaComeDos(Colores));
            BarajaCreada.Add(new CartaComeDos(Colores));
        }
        for(int i = 0; i < 4; i++)
        {
            BarajaCreada.Add(new CartaComodin(Carta.Colores.Negro));
            BarajaCreada.Add(new CartaCome4(Carta.Colores.Negro));
        }
        return BarajaCreada;
    }
    
    public BarajaUNO()
    {
        BarajaCartas = CrearBaraja();
    }
}
