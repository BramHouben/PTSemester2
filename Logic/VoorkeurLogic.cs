using Data;
using System;
using Model;
using System.Collections.Generic;
using Data.Context;

namespace Logic
{
    public class VoorkeurLogic
    {
        private VoorkeurRepository VoorkeurRepo = new VoorkeurRepository();
        //private medewerker inlogmedewerker;
        private MedewerkerSQL sqlMedewerker = new MedewerkerSQL();

        public void AddVoorkeur(string vak_naam, int prioriteit, string id)
        {
            if (prioriteit > 5 || prioriteit < 0)
            {
                throw new ArgumentException("Iets Fout met het proces, Probeer opnieuw");
                    
            }
            VoorkeurRepo.AddVoorkeur(new Voorkeur(vak_naam, prioriteit),id);
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
    }
}