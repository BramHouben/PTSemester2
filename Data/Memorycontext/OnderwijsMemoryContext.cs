using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Data.Context
{
    public class OnderwijsMemoryContext : IOnderwijsContext
    {
        public string OnderwijstaakNaam(int id)
        {
            throw new NotImplementedException();
        }

        public List<Taak> TakenOphalen()
        {
            throw new NotImplementedException();
        }
    }
}
