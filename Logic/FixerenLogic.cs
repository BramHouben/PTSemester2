using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Logic
{
    public class FixerenLogic
    {
        private FixerenRepository FixerenRepo = new FixerenRepository();
        

        public void TaakFixerenMetDocentID(int docentID, int taakID)
        {
            FixerenRepo.TaakFixerenMetDocentID(docentID, taakID);
        }

        public void VerwijderGefixeerdeTaak(int fid)
        {
            FixerenRepo.VerwijderGefixeerdeTaak(fid);
        }

        public void VeranderGefixeerdeTaak(int taakID, int docentID)
        {
            FixerenRepo.VeranderGefixeerdeTaak(taakID, docentID);
        }

        public List<GefixeerdeTaak> HaalAlleGefixeerdeTakenOp(string id)
        {
            return FixerenRepo.HaalAlleGefixeerdeTakenOp(id);
        }

        public GefixeerdeTaak HaalGefixeerdeTaakOpMetID(int Fix_id)
        {
            return FixerenRepo.HaalGefixeerdeTaakOpMetID(Fix_id);
        }
    }
}
