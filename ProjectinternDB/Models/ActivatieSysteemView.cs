using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectinternDB.Models
{
    public class ActivatieSysteemView
    {
        public List<Voorkeur> VoorkeurenDocent { get; set; }

        public int AantalInschrijvingen { get; set; }

        public int NogOpenInschrijvingen { get; set; }

    }
}
