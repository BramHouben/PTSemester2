using Data;
using Model;
using System.Collections.Generic;

namespace Logic
{
    public class OnderwijsLogic
    {
        private OnderwijsRepository OnderwijsRepo = new OnderwijsRepository();

        public IEnumerable<Onderwijstaak> onderwijstaak()
        {
            return OnderwijsRepo.onderwijstaak();
        }

        public IEnumerable<Onderwijstraject> onderwijstraject()
        {
            return OnderwijsRepo.onderwijstraject();
        }

        public IEnumerable<Onderwijsonderdeel> onderwijsonderdeel()
        {
            return OnderwijsRepo.onderwijsonderdeel();
        }

        public IEnumerable<Onderwijseenheid> onderwijseenheid()
        {
            return OnderwijsRepo.onderwijseenheid();
        }
    }
}
