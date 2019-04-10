using Model;
using Model.Onderwijsdelen;
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

        public List<Onderdeel> GetOnderdelenByTrajectId(int trajectId) => IvoorkeurContext.GetOnderdelenByTrajectId(trajectId);

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId) => IvoorkeurContext.GetTakenByOnderdeelId(onderdeelId);

    }
}