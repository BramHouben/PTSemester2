using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class AlgoritmeLogic
    {
        private static AlgoritmeRepo algoritmeRepo = new AlgoritmeRepo();

        public void ActiverenSysteen()
        {
            foreach (Algoritme algoritme in collection)
            {

            }
            List<Algoritme> AlgoritmeUitslag = algoritmeRepo.ActiverenSysteem();
        }
    }
}
