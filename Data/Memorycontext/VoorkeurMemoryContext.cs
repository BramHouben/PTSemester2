using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.Onderwijsdelen;

namespace Data.Context
{
    class VoorkeurMemoryContext : IVoorkeurContext
    {
        private static List<Voorkeur> voorkeuren = new List<Voorkeur>();

        //public VoorkeurMemoryContext()
        //{
        //    if (voorkeuren.Count == 0)
        //    {
        //        voorkeuren.Add(new Voorkeur(1, "LP", 3));
        //        voorkeuren.Add(new Voorkeur(2, "Vak2", 3));
        //        voorkeuren.Add(new Voorkeur(3, "Vak3", 3));
        //        voorkeuren.Add(new Voorkeur(4, "Vak4", 3));
        //    }
        //}

        public List<Voorkeur> VoorkeurenOphalen(string id)
        {
            return voorkeuren;
        }

        public void VoorkeurToevoegen(Voorkeur voorkeur, string id)
        {
            voorkeuren.Add(voorkeur);
        }

        //public void TestDataAanmaken()
        //{
        //    voorkeuren.Add(new Voorkeur(1, "LP", 3));
        //    voorkeuren.Add(new Voorkeur(2, "Vak2", 3));
        //    voorkeuren.Add(new Voorkeur(3, "Vak3", 3));
        //    voorkeuren.Add(new Voorkeur(4, "Vak4", 3));
        //}

        public void DeleteVoorkeur(int id)
        {
            throw new NotImplementedException();
        }

        public List<Traject> GetTrajecten()
        {
            throw new NotImplementedException();
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            throw new NotImplementedException();
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int onderdeelId)
        {
            throw new NotImplementedException();
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            throw new NotImplementedException();
        }

        string IVoorkeurContext.GetTaakInfo(int taakId)
        {
            throw new NotImplementedException();
        }

        public List<Medewerker> GetDocentenList(string user_id)
        {
            throw new NotImplementedException();
        }

        public bool KijkVoorDubbel(Voorkeur voorkeur, string id)
        {
            throw new NotImplementedException();
        }

        public List<Traject> GetTrajectenInzetbaar(string user_id)
        {
            throw new NotImplementedException();
        }

        public void InvoegenTaakVoorkeur(int id, int prioriteit, string User_id)
        {
            throw new NotImplementedException();
        }

        public Voorkeur GetVoorkeurInfo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
