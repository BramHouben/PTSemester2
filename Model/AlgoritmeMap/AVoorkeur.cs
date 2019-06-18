using System;
using System.Collections.Generic;
using System.Text;

namespace Model.AlgoritmeMap
{
   public class AVoorkeur
    {
        public int VoorkeurID { get; set; }
        public int TaakID { get; set; }
        public string TaakNaam { get; set; }
        public int Prioriteit { get; set; }

        public bool Ingedeeld { get; set; }
        public double Score { get; set; }
        public int BenodigdeUren { get; set; }
        public int BenodigdeDocenten { get; set; }

        public AVoorkeur()
        {

        }
    }
}
