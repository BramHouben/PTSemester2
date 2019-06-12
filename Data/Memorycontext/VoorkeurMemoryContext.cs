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
        private List<Docent> docenten = new List<Docent>();

        private List<Traject> trajecten;
        private List<Eenheid> eenheden;
        private List<Onderdeel> onderdelen;
        private List<Taak> taken;
        private List<Voorkeur> voorkeuren;
        public VoorkeurMemoryContext()
        {
            Docent docent1 = new Docent(1, 1, 400, "User1") { MedewerkerId = "User1" };
            docenten.Add(docent1);
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
            taken = new List<Taak>()
            {
                new Taak(1, "TaakTest",1,"TaakTestOmschrijving","TrajectTest","OnderdeelTest","EenheidTest")
            };
            voorkeuren = new List<Voorkeur>()
            {
                new Voorkeur(1, "TestVoorkeur")
            };

        }


        public List<Voorkeur> VoorkeurenOphalen(string id)
        {
            return voorkeuren;
        }

        public void VoorkeurToevoegen(Voorkeur voorkeur, string id)
        {
            voorkeuren.Add(voorkeur);
        }

        public void DeleteVoorkeur(int id)
        {
            voorkeuren.RemoveAll(X => X.Id == id);
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
            List<Taak> TakenList = new List<Taak>();

            foreach (Taak taak in taken)
            {
                if (taak.OnderdeelId == onderdeelId)
                {
                    TakenList.Add(taak);
                }
            }
            return TakenList;
        }

        public string GetTaakInfo(int taakId)
        {

            foreach (Taak taak in taken)
            {
                if (taak.TaakId == taakId)
                {
                    return taak.Omschrijving;
                }
            }
            return null;
        }

        public List<Medewerker> GetDocentenList(string user_id)
        {
            List<Medewerker> medewerkers = new List<Medewerker>()
            {
                new Medewerker("1", "testMedewerker")
            };
            return medewerkers;
        }

        public bool KijkVoorDubbel(Voorkeur voorkeur, string id)
        {
            //todo verbeteren
            foreach (Voorkeur voorkeur1 in voorkeuren)
            {
                if (voorkeur.Id == voorkeur.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Traject> GetTrajectenInzetbaar(string user_id)
        {
            // inzetbaarheid trajecten wordt niet in traject opgeslagen
            return null;
        }

        public void InvoegenTaakVoorkeur(int id, int prioriteit, string User_id)
        {
            Voorkeur voorkeur = new Voorkeur();
            voorkeur.Id = id;
            // todo userid en prio?
          
            if (!KijkVoorDubbel(new Voorkeur(), User_id))
            {
               voorkeuren.Add(new Voorkeur());
            }
        }

        public Voorkeur GetVoorkeurInfo(int id)
        {
            foreach (Voorkeur voorkeur in voorkeuren)
            {
                if (voorkeur.Id == id)
                {
                    return voorkeur;
                }
            }

            return null;
        }

        public int GetTaakTijd(int taakId)
        {
            // Taak heeft geen tijd
            return 0;
        }

        public Traject GetTrajectByID(int id)
        {
            throw new NotImplementedException();
        }
    }
    // TODO afmaken voorkeur Memory Context
}
