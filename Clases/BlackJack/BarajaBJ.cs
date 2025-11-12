using System;
using System.Drawing;
using BlackJack_Uno_BackUp.Interfaces;

namespace BlackJack_Uno_BackUp.Clases.BlackJack;

class BarajaBJ:Baraja,ICrearBaraja
{
    public override List<Carta> BarajaCartas
    {
        get { return _barajaCartas; }
        set
        {
            if (value.Count > 52)
            {
                throw new Exception("La baraja de BlackJack solo puede tener 52 cartas");
            }
            _barajaCartas = value;
        }
    }

    private static readonly Carta.Colores[] ColoresPosibles =
    {
        Carta.Colores.Rojo,Carta.Colores.Negro
    };

    private static readonly CartaBJ.Figuras[] Figuras =
    {
        CartaBJ.Figuras.Corazon,CartaBJ.Figuras.Trebol,CartaBJ.Figuras.Diamante,CartaBJ.Figuras.Pica
    };

    public List<Carta> CrearBaraja()
    {
        List<Carta> BarajaCreada = new List<Carta>();
        int alternarColor = 0;
        foreach (var palo in Figuras)
        {
            for (int i = 2; i < 11; i++)
            {
                BarajaCreada.Add(new CartaBJ(i, ColoresPosibles[alternarColor], palo));
            }
            
            var jack = new CartasEspecialessBJ(10, ColoresPosibles[alternarColor], palo);
            jack.FiguraSuit = CartasEspecialessBJ.TipoFigura.Jack;
            BarajaCreada.Add(jack);
            
            var reina = new CartasEspecialessBJ(10, ColoresPosibles[alternarColor], palo);
            reina.FiguraSuit = CartasEspecialessBJ.TipoFigura.Reina;
            BarajaCreada.Add(reina);
            
            var rey = new CartasEspecialessBJ(10, ColoresPosibles[alternarColor], palo);
            rey.FiguraSuit = CartasEspecialessBJ.TipoFigura.Rey;
            BarajaCreada.Add(rey);
            
            var As = new CartasEspecialessBJ(11, ColoresPosibles[alternarColor], palo);
            As.FiguraSuit = CartasEspecialessBJ.TipoFigura.As;
            BarajaCreada.Add(As);

            
            alternarColor++;
            if (alternarColor > 1)
            {
                alternarColor = 0;
            }
        }
        return BarajaCreada;
    }
    public Carta Repartir ()
    {
        if (BarajaCartas.Count == 0)
        {
            throw new Exception("No hay mas cartas en la baraja");
        }
        Carta carta = BarajaCartas[0];
        BarajaCartas.RemoveAt(0);
        return carta;
    }

    

    public BarajaBJ()
    {
        BarajaCartas = CrearBaraja();
    }
}
