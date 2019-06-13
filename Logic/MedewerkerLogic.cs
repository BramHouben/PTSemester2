using Data;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class MedewerkerLogic
    {
        private MedewerkerRepository MedewerkerRepo = new MedewerkerRepository();

        public List<Eenheid> KrijgAlleEenheden()
        {
            return MedewerkerRepo.KrijgAlleEenheden();
        }

        public void WijzigEenheid(Eenheid eenheid)
        {
            MedewerkerRepo.WijzigEenheid(eenheid);
        }

        public void ActiverenSysteen()
        {
            MedewerkerRepo.ActiverenSysteem();
        }
    }
}
