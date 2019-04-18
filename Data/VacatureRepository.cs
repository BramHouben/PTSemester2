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

        public List<Vacature> VacaturesOphalen()
        {
            return vacatureContext.VacaturesOphalen();
        }
        public void DeleteVacature(int id)
        {
           vacatureContext.DeleteVacature(id);
        }
        public Vacature VacatureOphalen(int id)
        {
            return vacatureContext.VacatureOphalen(id);
        }
    }
}
