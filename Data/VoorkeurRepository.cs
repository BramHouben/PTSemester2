using Data.Context;
using Model;
using System.Collections.Generic;

namespace Data
{
    public class VoorkeurRepository
    {
        private readonly IVoorkeurContext IvoorkeurContext;

        public VoorkeurRepository()
        {
            IvoorkeurContext = new VoorkeurSQLContext();
        }

        public List<Voorkeur> vkmodelList(string id) => IvoorkeurContext.VoorkeurenOphalen(id);
        public void AddVoorkeur(Voorkeur VkModel, string id) => IvoorkeurContext.VoorkeurToevoegen(VkModel, id);

        public void DeleteVoorkeur(int id) => IvoorkeurContext.DeleteVoorkeur(id);
    }
}