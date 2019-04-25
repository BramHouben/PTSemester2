using System.Collections.Generic;
using Data;
using Model.Onderwijsdelen;

namespace Logic
{
    public class BlokeigenaarLogic
    {
        private static BlokeigenaarRepository TaakRepos = new BlokeigenaarRepository();

        public void TaakAanmaken(Taak taak)
        {
            TaakRepos.TaakOpslaan(taak);
        }

        public List<Taak> TakenOphalen()
        {
            return TaakRepos.TakenOphalen();
        }
    }
}