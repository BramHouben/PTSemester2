using System;
using System.Collections.Generic;
using System.Text;

namespace Model.AlgoritmeMap
{
  public  class ATaak
    {
        public int TaakID { get; set; }
        public string TaakNaam { get; set; }
        public int Prioriteit { get; set; }
        public int AantalKeerGekozen { get; set; }
        public int BenodigdeUren { get; set; }
        public int AantalKlassen { get; set; }

        public List<Docent> IngedeeldeDocent { get; set; }
        public ATaak()
        {
            IngedeeldeDocent = new List<Docent>();
        }
    }
}
