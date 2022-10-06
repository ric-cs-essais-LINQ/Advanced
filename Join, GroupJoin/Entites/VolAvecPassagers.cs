using System.Collections.Generic;

namespace Entites
{
    public class VolAvecPassagers
    {
        public string RefVol { get; set; }

        public string DateProchainDepart { get; set; }

        public IList<Passager> Passagers { get; set; }
    }
}
