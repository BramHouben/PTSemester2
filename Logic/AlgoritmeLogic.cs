using Data;
using Data.Interfaces;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class AlgoritmeLogic
    {
        private static AlgoritmeRepo algoritmeRepo;

        public AlgoritmeLogic(IAlgoritmeContext algoritmeContext)
        {
            algoritmeRepo = new AlgoritmeRepo(algoritmeContext);
        }

        public List<Algoritme> ActiverenSysteen()
        {
            return algoritmeRepo.ActiverenSysteem();
        }
    }
}