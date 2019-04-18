using System;
using System.Collections.Generic;
using Model;
using Model.Onderwijsdelen;

namespace Data
{
    public interface IVoorkeurContext
    {
        List<Voorkeur> VoorkeurenOphalen(string id);

        void VoorkeurToevoegen(Voorkeur voorkeur, string id);

        void DeleteVoorkeur(int id);

        List<Traject> GetTrajecten();

        List<Eenheid> GetEenhedenByTrajectId(int trajectId);

        List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId);

        List<Taak> GetTakenByOnderdeelId(int onderdeelId);

        string GetTaakInfo(int taakId);
    }
}   