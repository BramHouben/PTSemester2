using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Data.Context
{
    public class OnderwijsMemoryContext : IOnderwijsContext
    {
        private List<Traject> _trajecten = new List<Traject>();
        private List<Eenheid> _eenheden = new List<Eenheid>();
        private List<Onderdeel> _onderdelen = new List<Onderdeel>()
        {
            new Onderdeel(1,"LP"),
            new Onderdeel(2, "Proftaak"),
            new Onderdeel(3, "OIB"),
            new Onderdeel(4, "OIS")
        };
        private List<Taak> _taken = new List<Taak>() {
            new Taak(1,"LP-Coach", 1, "Het Coachen bij een LP project", "SoftWare", "LP", "Semester 2"),
            new Taak(2,"Proftaak Begeleider",2, "Stantaard begeleiding van een proftaak", "SoftWare", "Proftaak", "Semester 2"),
            new Taak(3,"Docent OIB",3, "Les geven over business", "Business", "OIB", "Semester 1"),
            new Taak(4,"Docent OIS",4, "Les geven over software", "SoftWare", "OIS", "Semester 1"),
            new Taak(5, "LP-Begeleider",1, "SoftWare", "LP", "Semester 2"),
            new Taak(6, "Business begeleider",3, "Onderstuining geven over business lessen", "Business", "OIB", "Semester 2")
        };

        public string OnderwijstaakNaam(int id)
        {
            throw new NotImplementedException();
        }

        public List<Taak> TakenOphalen()
        {
            return _taken;
        }

        public void TaakToevoegen(Taak taak)
        {
            _taken.Add(taak);
        }

        public void TaakVerwijderen(int id)
        {
            List<Taak> taken1 = _taken;
            try
            {
                for (int i = 0; i < taken1.Count; i++)
                {
                    if (taken1[i].TaakId == id)
                    {
                        taken1.RemoveAt(i);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Iets ging verkeerd tijdens een taak verwijderen!");
            }
        }

        public Taak TaakOphalen(int id)
        {
            //List<Taak> taken1 = _taken;

            //switch (id)
            //{
            //    case 1:
            //        return taken1[0];
            //    case 2:
            //        return taken1[1];
            //    case 3:
            //        return taken1[2];
            //    case 4:
            //        return taken1[3];
            //    case 5:
            //        return taken1[4];
            //    default:
            //        throw new NotImplementedException();
            //}

            Taak correcteTaak = null;
            foreach (Taak currentTaak in _taken)
            {
                if (currentTaak.TaakId == id)
                {
                    correcteTaak = currentTaak;
                }
            }

            return correcteTaak;
        }

        public void UpdateTaak(Taak taak)
        {
            Taak taakToUpdate = TaakOphalen(taak.TaakId);
            taakToUpdate.Omschrijving = taak.Omschrijving;
            taakToUpdate.TaakNaam = taak.TaakNaam;
            taakToUpdate.OnderdeelNaam = taak.OnderdeelNaam;
            // TODO: neem OnderwijsID over en sla op
        }

        public List<Traject> GetTrajecten()
        {
            return _trajecten;
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            return _eenheden;
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
        {
            return _onderdelen;
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            return _taken;
        }
    }
}
