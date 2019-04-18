using System.Collections.Generic;
using Data.Context;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

namespace Data
{
    public class TaakRepository
    {
        private ITaakContext taakContext;

        public TaakRepository()
        {
            taakContext = new TaakMemoryContext();
        }

        public void TaakOpslaan(Taak taak)
        {
            taakContext.TaakToevoegen(taak);
        }

        public List<Taak> VacaturesOphalen()
        {
            return taakContext.TakenOphalen();
        }
    }
}