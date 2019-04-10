using Data;
using System;
using Model;
using System.Collections.Generic;
using Data.Context;
using Model.Onderwijsdelen;

namespace Logic
{
    public class VoorkeurLogic
    {
        private VoorkeurRepository VoorkeurRepo;
        private MedewerkerSQL sqlMedewerker = new MedewerkerSQL();

        public VoorkeurLogic(IVoorkeurContext context)
        {
            VoorkeurRepo = new VoorkeurRepository(context);
        }

        public void AddVoorkeur(int traject_naam, int prioriteit, int onderdeel_naam, int taak_naam, string id)
        {
            if (prioriteit > 5 || prioriteit < 0)
            {
                throw new ArgumentException("Iets Fout met het proces, Probeer opnieuw");
                    
            }
            VoorkeurRepo.AddVoorkeur(new Voorkeur(traject_naam, prioriteit,onderdeel_naam,taak_naam),id);
        }

        public void DeleteVoorkeur(int id)
        {
            VoorkeurRepo.DeleteVoorkeur(id);
        }
        public Medewerker krijgUser_id(string user_id)
        {

            
            return sqlMedewerker.getmedewerkerid(user_id);
        }

        public List<Voorkeur> OphalenVoorkeur(string id)
        {
            return VoorkeurRepo.vkmodelList(id);
        }
        public string KrijgStringId(string id)
        {
            return id;
        }

        public List<Traject> GetTrajecten() => VoorkeurRepo.GetTrajecten();

        public List<Onderdeel> GetOnderdelenByTrajectId(int trajectId) => VoorkeurRepo.GetOnderdelenByTrajectId(trajectId);

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId) => VoorkeurRepo.GetTakenByOnderdeelId(onderdeelId);
    }
}