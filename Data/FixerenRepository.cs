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
        public FixerenRepository(IFixerenContext context)
        {
            FixerenContext = context;
        }
        public void TaakFixerenMetDocentID(int docentID, int taakID) => FixerenContext.TaakFixerenMetDocentID(docentID, taakID);
        public void VerwijderGefixeerdeTaak(int taakID) => FixerenContext.VerwijderGefixeerdeTaak(taakID);
        public void VeranderGefixeerdeTaak(int taakID, int docentID) => FixerenContext.VeranderGefixeerdeTaak(taakID, docentID);
        public List<GefixeerdeTaak> HaalAlleGefixeerdeTakenOp() => FixerenContext.HaalAlleGefixeerdeTakenOp();
        public GefixeerdeTaak HaalGefixeerdeTaakOpMetID(int Fix_id) => FixerenContext.HaalGefixeerdeTaakOpMetID(Fix_id);
    }
}
