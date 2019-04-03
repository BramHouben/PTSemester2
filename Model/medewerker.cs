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
        public string Role_id { get; set; }
        public string Email { get; set; }
    }
}
