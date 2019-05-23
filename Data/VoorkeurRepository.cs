using Model;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;

namespace Data
{
    public class VoorkeurRepository
    {
        private readonly IVoorkeurContext IvoorkeurContext;

        public VoorkeurRepository(IVoorkeurContext context)
        {
            IvoorkeurContext = context;
        }

        public List<Voorkeur> vkmodelList(string id) => IvoorkeurContext.VoorkeurenOphalen(id);

        public void AddVoorkeur(Voorkeur VkModel, string id) => IvoorkeurContext.VoorkeurToevoegen(VkModel, id);

        public void DeleteVoorkeur(int id) => IvoorkeurContext.DeleteVoorkeur(id);

        public List<Traject> GetTrajecten() => IvoorkeurContext.GetTrajecten();

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId) => IvoorkeurContext.GetEenhedenByTrajectId(trajectId);

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId) => IvoorkeurContext.GetOnderdelenByEenheidId(eenheidId);

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId) => IvoorkeurContext.GetTakenByOnderdeelId(onderdeelId);

        public string GetTaakInfo(int taakId) => IvoorkeurContext.GetTaakInfo(taakId);

        public List<Medewerker> GetDocentenList(string user_id)
        {
            return IvoorkeurContext.GetDocentenList(user_id);
        }

        public bool KijkenVoorDubbel(Voorkeur voorkeur, string id)
        {
            return IvoorkeurContext.KijkVoorDubbel( voorkeur, id);
        }

        public List<Traject> GetTrajectenInzetbaar(string user_id) => IvoorkeurContext.GetTrajectenInzetbaar(user_id);

        public void InvoegenTaakVoorkeur(int id, int prioriteit, string User_id) => IvoorkeurContext.InvoegenTaakVoorkeur(id, prioriteit, User_id);

        public Voorkeur GetVoorkeurInfo(int id) => IvoorkeurContext.GetVoorkeurInfo(id);

        public int getTaakTijd(int taakId) => IvoorkeurContext.GetTaakTijd(taakId);
    }
}