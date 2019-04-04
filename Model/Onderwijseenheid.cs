using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Onderwijseenheid
    {
        public Onderwijseenheid(int onderwijsEendheidID, string omschrijving, int eCTS, float aantalKlassen, List<Onderwijsonderdeel> onderwijsOnderdelen)
        {
            OnderwijsEendheidID = onderwijsEendheidID;
            Omschrijving = omschrijving;
            ECTS = eCTS;
            AantalKlassen = aantalKlassen;
            OnderwijsOnderdelen = onderwijsOnderdelen;
        }

        public Onderwijseenheid()
        {

        }

        public int OnderwijsEendheidID { get; set; }

        public string Omschrijving { get; set; }

        public int ECTS { get; set; }

        public float AantalKlassen { get; set; }

        public List<Onderwijsonderdeel> OnderwijsOnderdelen { get; set; }
    }
}
