using System.Collections.Generic;
using System.Linq;

namespace BlackJack_Uno_BackUp.Clases;

interface IMano
{  
    public void AgregarCarta(Carta carta);
     public void QuitarCarta(Carta carta);
}