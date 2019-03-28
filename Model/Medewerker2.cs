using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Medewerker
    {
        public Medewerker()
        {

        }
        public Medewerker(int medewerkerId, string naam)
        {
            MedewerkerId = medewerkerId;
            Naam = naam;
        }

        public int MedewerkerId { get; set; }
        public string Naam { get; set; }
    }
}
