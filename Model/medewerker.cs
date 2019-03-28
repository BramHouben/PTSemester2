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
        public Medewerker(string medewerkerId, string naam)
        {
            MedewerkerId = medewerkerId;
            Naam = naam;
        }

        public string MedewerkerId { get; set; }
        public string Naam { get; set; }
        public string role_id { get; set; }

        public string email { get; set; }
    }
}
