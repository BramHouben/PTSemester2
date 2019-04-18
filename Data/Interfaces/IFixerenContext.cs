using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IFixerenContext
    {
        void TaakFixerenMetDocentID(int docentID, int taakID);
        void VerwijderGefixeerdeTaak(int taakID);
        void VeranderGefixeerdeTaak(int taakID, int docentID);
        List<GefixeerdeTaak> HaalAlleGefixeerdeTakenOp();
        GefixeerdeTaak HaalGefixeerdeTaakOpMetID(int Fix_id);
    }
}
