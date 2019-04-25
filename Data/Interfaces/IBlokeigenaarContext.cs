using System.Collections.Generic;
using Model.Onderwijsdelen;

namespace Data.Interfaces
{
    public interface IBlokeigenaarContext
    {
        List<Taak> TakenOphalen();

        void TaakToevoegen(Taak taak);
        
        void TaakVerwijderen(int taakId);

        List<Traject> GetTrajecten();

        List<Eenheid> GetEenhedenByTrajectId(int trajectId);

        List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId);

        List<Taak> GetTakenByOnderdeelId(int onderdeelId);
    }
}