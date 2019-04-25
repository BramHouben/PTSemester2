using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Data
{
   public class OnderwijsRepository
    {
        private IOnderwijsContext _onderwijsContext;

       public OnderwijsRepository()
        {
            _onderwijsContext = new OnderwijsMemoryContext();
        }

       public string OnderwijstaakNaam(int id)
       {
           return _onderwijsContext.OnderwijstaakNaam(id);
       }

       public List<Taak> TakenOphalen()
       {
           return _onderwijsContext.TakenOphalen();
       }

       public void TaakToevoegen(Taak taak)
       {
           _onderwijsContext.TaakToevoegen(taak);
       }

       public void TaakVerwijderen(int taakId)
       {
           _onderwijsContext.TaakVerwijderen(taakId);
       }

       public void TaakOpslaan(Taak taak)
       {
           _onderwijsContext.TaakToevoegen(taak);
       }

       public Taak TaakOphalen(int id)
       {
          return _onderwijsContext.TaakOphalen(id);
       }

       public List<Traject> GetTrajecten()
       {
         return  _onderwijsContext.GetTrajecten();
       }

       public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
       {
         return  GetEenhedenByTrajectId(trajectId);
       }

       public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
       {
         return  GetOnderdelenByEenheidId(eenheidId);
       }

       public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
       {
         return  GetTakenByOnderdeelId(onderdeelId);
       }
    }
}
