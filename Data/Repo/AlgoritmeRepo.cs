using Data.Context;
using Data.Interfaces;
using Model;
using Model.AlgoritmeMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AlgoritmeRepo
    {
        private IAlgoritmeContext algoritmeContext;

        public AlgoritmeRepo(IAlgoritmeContext algoritmeContext)
        {
            this.algoritmeContext = algoritmeContext;
        }

        public List<Algoritme> ActiverenSysteem()
        {
          return algoritmeContext.ActiverenSysteem();
        }

        public void DeleteTabel()
        {
             algoritmeContext.DeleteTabel();
        }

   

        public List<ATaak> TakenOphalen()
        {
            return algoritmeContext.TakenOphalen();
        }



        public List<ADocent> InzetbareDocenten(int taakID)
        {
            return algoritmeContext.InzetbareDocenten(taakID);
        }

        public void ZetinDbNull(int taakID)
        {
            algoritmeContext.ZetinDbNull(taakID);
        }

        public void ZetinDb(int docentID, int taakID)
        {
            algoritmeContext.ZetinDb(docentID,taakID);
        }

        public void VerwijderVoorkeur(int docentID, int iD)
        {
            algoritmeContext.VerwijderVoorkeur(docentID,iD);
        }
    }
}
