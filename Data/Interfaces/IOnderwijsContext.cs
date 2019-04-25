using System;
using System.Collections.Generic;
using System.Text;
using Model.Onderwijsdelen;

namespace Data.Interfaces
{
    interface IOnderwijsContext
    {
       string OnderwijstaakNaam(int id);
       List<Taak> TakenOphalen();
    }
}
