using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Onderwijsonderdeel
    {
        public Onderwijsonderdeel(int onderwijsOnderdeelID, string omschrijving, List<Onderwijstaak> onderwijsTaken)
        {
            OnderwijsOnderdeelID = onderwijsOnderdeelID;
            Omschrijving = omschrijving;
            OnderwijsTaken = onderwijsTaken;
        }

        public Onderwijsonderdeel()
        {

        }

        public int OnderwijsOnderdeelID { get; set; }

        public string Omschrijving { get; set; }

        public List<Onderwijstaak> OnderwijsTaken { get; set; }       
    }
}
