using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IOnderwijsContext
    {
        IEnumerable<Onderwijstaak> OnderwijsTaakOphalen();

        IEnumerable<Onderwijstraject> OnderwijsTrajectOphalen();

        IEnumerable<Onderwijsonderdeel> OnderwijsOnderdeelOphalen();

        IEnumerable<Onderwijseenheid> OnderwijsEenheidOphalen();
    }
}
