using System;
using System.Collections.Generic;

namespace Domaine.MyEntities
{
    public class TirageLoto //Cf. toutes les explications... dans classe Numero.
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        // public IList<Numero> Numeros { get; set; } // Eh NON !!
        public IList<NumeroTirageLoto> NumerosTiragesLoto { get; set; } //Bonne façon de faire.
    }
}
