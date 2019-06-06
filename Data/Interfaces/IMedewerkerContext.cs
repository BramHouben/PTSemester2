using Model;
using Model.Onderwijsdelen;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IMedewerkerContext
    {
        Medewerker GetMedewerkerId(string id);
        List<Eenheid> KrijgAlleEenheden();
        void WijzigEenheid(Eenheid eenheid);
    }
}
