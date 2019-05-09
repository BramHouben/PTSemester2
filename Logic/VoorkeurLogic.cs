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

        public void AddVoorkeur(string traject, string eenheid, string onderdeel, string taak, string id)
        {
            //if (prioriteit > 5 || prioriteit < 0)
            //{
            //    throw new ArgumentException("Iets Fout met het proces, Probeer opnieuw"); //Melding sturen naar de gebruiker!                   
            //}
            VoorkeurRepo.AddVoorkeur(new Voorkeur(traject, eenheid, onderdeel, taak),id);
        }

        public void DeleteVoorkeur(int id)
        {
            VoorkeurRepo.DeleteVoorkeur(id);
        }
        public Medewerker KrijgUser_id(string user_id)
        {

            
            return sqlMedewerker.Getmedewerkerid(user_id);
        }

        public List<Medewerker> GetDocentenList(string user_id)
        {
            return VoorkeurRepo.GetDocentenList(user_id);
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

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId) => VoorkeurRepo.GetEenhedenByTrajectId(trajectId);

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId) => VoorkeurRepo.GetOnderdelenByEenheidId(eenheidId);

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId) => VoorkeurRepo.GetTakenByOnderdeelId(onderdeelId);

        public string GetTaakInfo(int taakId) => VoorkeurRepo.GetTaakInfo(taakId);

        public bool KijkenVoorDubbel(string trajectId, string eenheid, string onderdeel, string taak, string id)
        {
         return   VoorkeurRepo.KijkenVoorDubbel(new Voorkeur(trajectId, eenheid, onderdeel, taak), id);
        }

        public List<Traject> GetTrajectenInzetbaar(string user_id) => VoorkeurRepo.GetTrajectenInzetbaar(user_id);

    }
}