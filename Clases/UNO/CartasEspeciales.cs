using System;

namespace BlackJack_Uno_BackUp.Clases.UNO;

class CartaReversa : CartaEspecialUNO
{
    public CartaReversa(Colores color) : base(color, 10) { }

    public override void EfectoCarta(JuegoUNO juego)
    {
        juego.Sentido = !juego.Sentido;
    }
}

class CartaNoJuegas : CartaEspecialUNO
{
    public CartaNoJuegas(Colores color) : base(color, 11) { }
    public override void EfectoCarta(JuegoUNO juego)
    {
        juego.SaltoTurno = true;
    }
}

class CartaComeDos : CartaEspecialUNO
{
    public CartaComeDos(Colores color) : base(color, 12) { }
    public override void EfectoCarta(JuegoUNO juego)
    {
        juego.CartasRobar = juego.CartasRobar + 2;
    }
}

class CartaComodin : CartaEspecialUNO
{
    public CartaComodin(Colores color) : base(color, 13) { }
    public override void EfectoCarta(JuegoUNO juego)
    {
        Random random = new Random();
        Colores[] coloresPosibles = new Colores[]
        {
            Colores.Azul,Colores.Verde,Colores.Amarillo,Colores.Rojo
        };
        Color = coloresPosibles[random.Next(0, coloresPosibles.Length)];
    }
}

class CartaCome4 : CartaEspecialUNO
{
    public CartaCome4(Colores color ) : base(color,14) { }
    public override void EfectoCarta(JuegoUNO juego)
    {
        Random random = new Random();
        Colores[] coloresPosibles = new Colores[]
        {
            Colores.Azul,Colores.Verde,Colores.Amarillo,Colores.Rojo
        };
        Color = coloresPosibles[random.Next(0, coloresPosibles.Length)];
        juego.CartasRobar = juego.CartasRobar + 4;
    }
}
