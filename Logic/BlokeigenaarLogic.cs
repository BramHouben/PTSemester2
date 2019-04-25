using System.Collections.Generic;
using Data;
using Model.Onderwijsdelen;

namespace Logic
{
    public class BlokeigenaarLogic
    {
        private static OnderwijsRepository OnderwijsRepos = new OnderwijsRepository();

        public void TaakAanmaken(Taak taak)
        {
            OnderwijsRepos.TaakOpslaan(taak);
        }

        public List<Taak> TakenOphalen()
        {
            return OnderwijsRepos.TakenOphalen();
        }

        public Taak TaakOphalen(int id)
        {
            Taak taak = OnderwijsRepos.TaakOphalen(id);
            return taak;
        }

        public void TaakVerwijderen(int id)
        {
            OnderwijsRepos.TaakVerwijderen(id);
        }
    }
}