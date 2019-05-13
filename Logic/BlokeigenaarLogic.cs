﻿using System.Collections.Generic;
using Data;
using Model.Onderwijsdelen;

namespace Logic
{
    public class BlokeigenaarLogic
    {
        private static OnderwijsRepository OnderwijsRepos = new OnderwijsRepository();

        public void TaakAanmaken(Taak taak)
        {
            OnderwijsRepos.TaakOpslaan(taak);
        }

        public List<Taak> TakenOphalen()
        {
            return OnderwijsRepos.TakenOphalen();
        }

        public Taak TaakOphalen(int id)
        {
            Taak taak = OnderwijsRepos.TaakOphalen(id);
            return taak;
        }

        public void TaakVerwijderen(int id)
        {
            OnderwijsRepos.TaakVerwijderen(id);
        }

        public void UpdateTaak(Taak taak)
        {
            OnderwijsRepos.UpdateTaak(taak);
        }

        public List<Traject> GetTrajecten() => OnderwijsRepos.GetTrajecten();

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId) => OnderwijsRepos.GetEenhedenByTrajectId(trajectId);

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId) => OnderwijsRepos.GetOnderdelenByEenheidId(eenheidId);

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId) => OnderwijsRepos.GetTakenByOnderdeelId(onderdeelId);

        //public string GetTaakInfo(int taakId) => OnderwijsRepos.GetTaakInfo(taakId);
    }
}