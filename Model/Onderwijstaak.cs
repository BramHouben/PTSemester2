using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Onderwijstaak
    {
        public Onderwijstaak(int onderwijsTaakID, string omschrijving, List<Docent> inzetbaarheidDocenten)
        {
            OnderwijsTaakID = onderwijsTaakID;
            Omschrijving = omschrijving;
            InzetbaarheidDocenten = inzetbaarheidDocenten;
        }

        public Onderwijstaak()
        {

        }

        public int OnderwijsTaakID { get; set; }

        public string Omschrijving { get; set; }

        public List<Docent> InzetbaarheidDocenten { get; set; }
    }
}
