using Data.Context;
using Data.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class FixerenRepository
    {
        private readonly IFixerenContext FixerenContext;
        public FixerenRepository()
        {
            FixerenContext = new FixerenSQLContext();
        }
        public void TaakFixerenMetDocentID(int docentID, int taakID) => FixerenContext.TaakFixerenMetDocentID(docentID, taakID);
        public void VerwijderGefixeerdeTaak(int fid) => FixerenContext.VerwijderGefixeerdeTaak(fid);
        public void VeranderGefixeerdeTaak(int taakID, int docentID) => FixerenContext.VeranderGefixeerdeTaak(taakID, docentID);
        public List<GefixeerdeTaak> HaalAlleGefixeerdeTakenOp() => FixerenContext.HaalAlleGefixeerdeTakenOp();
        public GefixeerdeTaak HaalGefixeerdeTaakOpMetID(int Fix_id) => FixerenContext.HaalGefixeerdeTaakOpMetID(Fix_id);
    }
}
