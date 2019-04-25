using System.Collections.Generic;
using Data.Context;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

namespace Data
{
    public class BlokeigenaarRepository
    {
        private IBlokeigenaarContext taakContext;

        public BlokeigenaarRepository()
        {
            taakContext = new BlokeigenaarMemoryContext();
        }

        public void TaakOpslaan(Taak taak)
        {
            taakContext.TaakToevoegen(taak);
        }

        public List<Taak> TakenOphalen()
        {
            return taakContext.TakenOphalen();
        }
    }
}