using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Model
{
    public class Algoritme
    {
     public int AlgoritmeId { get; set; }

      public int TaakID { get; set; }

        public List <Docent> docents { get; set; }
    }
}
