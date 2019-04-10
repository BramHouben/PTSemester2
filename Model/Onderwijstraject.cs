using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Onderwijstraject
    {
        public Onderwijstraject(int onderwijsTrajectID, string omschrijving, List<Onderwijseenheid> onderwijsEenheden)
        {
            OnderwijsTrajectID = onderwijsTrajectID;
            Omschrijving = omschrijving;
            OnderwijsEenheden = onderwijsEenheden;
        }

        public Onderwijstraject()
        {

        }

        public int OnderwijsTrajectID { get; set; }

        public string Omschrijving { get; set; }

        public List<Onderwijseenheid> OnderwijsEenheden { get; set; }
    }
}
