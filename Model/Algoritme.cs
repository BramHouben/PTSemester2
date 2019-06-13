using Model;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Model
{
    public class Algoritme
    {
     public int AlgoritmeId { get; set; }

      public Taak Taak { get; set; }

      public Docent Docent { get; set; }
    }
}
