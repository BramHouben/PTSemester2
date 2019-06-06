using Data.Context;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class MedewerkerRepository
    {
        private readonly IMedewerkerContext MedewerkerContext;

        public MedewerkerRepository()
        {
            MedewerkerContext = new MedewerkerSQLContext();
        }

        public Medewerker GetMedewerkerId(string id) => MedewerkerContext.GetMedewerkerId(id);
        public List<Eenheid> KrijgAlleEenheden() => MedewerkerContext.KrijgAlleEenheden();
        public void WijzigEenheid(Eenheid eenheid) => MedewerkerContext.WijzigEenheid(eenheid);
    }
}
