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
       private IOnderwijsContext _iOnderwijsContext;

       public OnderwijsRepository()
       {
            _iOnderwijsContext = new OnderwijsSQLContext();
       }

       public OnderwijsRepository(IOnderwijsContext context)
       {
           _iOnderwijsContext = context;
       }

       public string OnderwijstaakNaam(int id)
       {
           return _iOnderwijsContext.OnderwijstaakNaam(id);
       }

       public List<Taak> TakenOphalen(string blokeigenaarID)
       {
           return _iOnderwijsContext.TakenOphalen(blokeigenaarID);
       }

       public void TaakToevoegen(Taak taak)
       {
           _iOnderwijsContext.TaakToevoegen(taak);
       }

       public void TaakVerwijderen(int taakId)
       {
           _iOnderwijsContext.TaakVerwijderen(taakId);
       }

       public void TaakOpslaan(Taak taak)
       {
           _iOnderwijsContext.TaakToevoegen(taak);
       }

       public void UpdateTaak(Taak taak)
       {
           _iOnderwijsContext.UpdateTaak(taak);
       }

        public Taak TaakOphalen(int id)
       {
          return _iOnderwijsContext.TaakOphalen(id);
       }

       public List<Traject> GetTrajecten() => _iOnderwijsContext.GetTrajecten();

       public List<Eenheid> GetEenhedenByTrajectId(int trajectId) => _iOnderwijsContext.GetEenhedenByTrajectId(trajectId);

       public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId) => _iOnderwijsContext.GetOnderdelenByEenheidId(eenheidId);

       public List<Taak> GetTakenByOnderdeelId(int onderdeelId) => _iOnderwijsContext.GetTakenByOnderdeelId(onderdeelId);

       public List<Eenheid> OphalenEenhedenBlokeigenaar(string blokeigenaarId) => _iOnderwijsContext.OphalenEenhedenBlokeigenaar(blokeigenaarId);
       
   }
}
