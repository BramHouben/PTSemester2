using Model;
using Model.AlgoritmeMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IAlgoritmeContext
    {
        List<Algoritme> ActiverenSysteem();
        void DeleteTabel();
     
        List<ATaak> TakenOphalen();
        List<ADocent> InzetbareDocenten(int taakID);
        void ZetinDbNull(int taakID);
        void ZetinDb(int docentID, int taakID);
        void VerwijderVoorkeur(int docentID, int iD);
        void GefixeerdeLijstDoorvoeren(int docentID, int TaakID);
        List<int> OphalenGefixeerdeTakenID();
        List<int> DocentenIDsOphalenMetTaakID(int i);
    }
}
