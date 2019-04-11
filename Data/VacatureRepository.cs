using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Data.Interfaces;
using Model;

namespace Data
{
   public class VacatureRepository
    {
        private IVacatureContext vacatureContext;

       public VacatureRepository()
        {
            vacatureContext = new VacatureSQLContext();
        }
        public void VacatureOpslaan(Vacature vacature)
        {
            vacatureContext.VacatureOpslaan(vacature);
        }
    }
}
