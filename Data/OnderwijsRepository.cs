using Data.Context;
using Data.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class OnderwijsRepository
    {
        private readonly IOnderwijsContext IonderwijsContext;

        public OnderwijsRepository()
        {
            IonderwijsContext = new OnderwijsSQLContext();
        }

        public IEnumerable<Onderwijstaak> onderwijstaak() => IonderwijsContext.OnderwijsTaakOphalen();

        public IEnumerable<Onderwijstraject> onderwijstraject() => IonderwijsContext.OnderwijsTrajectOphalen();

        public IEnumerable<Onderwijsonderdeel> onderwijsonderdeel() => IonderwijsContext.OnderwijsOnderdeelOphalen();

        public IEnumerable<Onderwijseenheid> onderwijseenheid() => IonderwijsContext.OnderwijsEenheidOphalen();

    }
}
