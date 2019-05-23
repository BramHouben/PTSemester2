using System;
using System.Collections.Generic;
using System.Text;
using Model.Onderwijsdelen;

namespace Data.Interfaces
{
    interface IOnderwijsContext
    {
       string OnderwijstaakNaam(int id);
       List<Taak> TakenOphalen();

       void TaakToevoegen(Taak taak);

       void TaakVerwijderen(int taakId);

       Taak TaakOphalen(int id);

       void UpdateTaak(Taak taak);

       List<Traject> GetTrajecten();

       List<Eenheid> GetEenhedenByTrajectId(int trajectId);

       List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId);

       List<Taak> GetTakenByOnderdeelId(int onderdeelId);

       List<Eenheid> OphalenEenhedenBlokeigenaar(string blokeigenaarId);
    }
}
