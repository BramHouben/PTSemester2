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

        public List<Voorkeur> vkmodelList => IvoorkeurContext.VoorkeurenOphalen();
        public void AddVoorkeur(Voorkeur VkModel) => IvoorkeurContext.VoorkeurToevoegen(VkModel);
    }
}