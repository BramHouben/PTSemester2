using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.Onderwijsdelen;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Data.Context
{
    public class VoorkeurMemoryContext : IVoorkeurContext
    {
        private List<Docent> docenten;
        private List<Traject> trajecten;
        private List<Eenheid> eenheden;
        private List<Onderdeel> onderdelen;
        public VoorkeurMemoryContext()
        {
            trajecten = new List<Traject>();
            docenten = new List<Docent>();
            eenheden = new List<Eenheid>()
            {
                new Eenheid(1, "Eenheid1", 1)
            };
            onderdelen = new List<Onderdeel>()
            {
                new Onderdeel(1, "OnderdeelTest", 1)
            };
           
            Docent docent1 = new Docent(1, 1, 400, "User1");
            docent1.MedewerkerId = "User1";
            docenten.Add(docent1);
        }


        public List<Voorkeur> VoorkeurenOphalen(string id)
        {
            foreach (Docent docent in docenten)
            {
                if (docent.MedewerkerId == id)
                {
                    return docent.Voorkeuren;
                }
            }
            return null;
        }

        public void VoorkeurToevoegen(Voorkeur voorkeur, string id)
        {
            foreach (Docent docent in docenten)
            {
                if (docent.MedewerkerId == id)
                {
                    docent.Voorkeuren.Add(voorkeur);
                }
            }
        }

        public void DeleteVoorkeur(int id)
        {
            foreach (Docent docent in docenten)
            {
                docent.Voorkeuren.RemoveAll(X => X.Id == id);
            }
        }

        public List<Traject> GetTrajecten()
        {
            return trajecten;
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            List<Eenheid> eenhedenList = new List<Eenheid>();
            foreach (Eenheid eenheid in eenheden)
            {
                if (eenheid.TrajectId == trajectId)
                {
                   eenhedenList.Add(eenheid);
                }
            }
            return eenhedenList;
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
        {
            List<Onderdeel> OnderdelenList = new List<Onderdeel>();
            foreach (Onderdeel onderdeel in onderdelen)
            {
                if (onderdeel.EenheidId == eenheidId)
                {
                    OnderdelenList.Add(onderdeel);
                }
            }
            return OnderdelenList;
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            throw new NotImplementedException();
        }

        public string GetTaakInfo(int taakId)
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
