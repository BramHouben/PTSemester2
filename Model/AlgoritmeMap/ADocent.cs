using System;
using System.Collections.Generic;
using System.Text;

namespace Model.AlgoritmeMap
{
  public  class ADocent
    {
        public int docentID { get; set; }
        public int aantalkeuzes { get; set; }
        public List<AVoorkeur> voorkeuren { get; set; }
        public int InzetbareUren { get; set; }
        public double Score { get; set; }

        public ADocent()
        {
            voorkeuren = new List<AVoorkeur>();
            aantalkeuzes = voorkeuren.Count;

        }
    }
}
