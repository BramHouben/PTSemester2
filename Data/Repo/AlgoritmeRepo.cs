using Data.Context;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AlgoritmeRepo
    {
        private AlgoritmeSQLContext algoritmeSQLContext;
        public List<Algoritme> ActiverenSysteem()
        {
          return  algoritmeSQLContext.ActiverenSysteem();
        }
    }
}
