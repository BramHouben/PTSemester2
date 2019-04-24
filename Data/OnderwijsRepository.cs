using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Data.Interfaces;

namespace Data
{
   public class OnderwijsRepository
    {
        private IOnderwijsContext _onderwijsContext;

       public OnderwijsRepository()
        {
            _onderwijsContext = new OnderwijsSQLContext();
        }

       public string OnderwijstaakNaam(int id)
       {
           return _onderwijsContext.OnderwijstaakNaam(id);
       }

    }
}
