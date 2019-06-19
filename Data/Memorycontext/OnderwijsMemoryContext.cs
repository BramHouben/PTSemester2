using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Data.Context
{
    //TODO dubbel met Blokeigenaarlogic??
    public class OnderwijsMemoryContext : IOnderwijsContext
    {
        private List<Traject> _trajecten = new List<Traject>()
        {
            new Traject(1, "Software"),
            new Traject(2, "Media"),
            new Traject(3, "Business"),
            new Traject(4, "Technology"),
            new Traject(5, "Start Semester")
        };

        private List<Eenheid> _eenheden = new List<Eenheid>()
        {
            new Eenheid(1, "Semester 1", 1),
            new Eenheid(2, "Semester 2", 1),
            new Eenheid(3, "Semester 3", 1),
            new Eenheid(4, "Semester 4", 1),
            new Eenheid(5, "Semester 1", 2),
            new Eenheid(6, "Semester 2", 2),
            new Eenheid(7, "Semester 3", 2),
            new Eenheid(8, "Semester 4", 2),
            new Eenheid(9, "Semester 1", 3),
            new Eenheid(10, "Semester 2", 3),
            new Eenheid(11, "Semester 3", 3),
            new Eenheid(12, "Semester 4", 3),
            new Eenheid(13, "Semester 1", 4),
            new Eenheid(14, "Semester 2", 4),
            new Eenheid(15, "Semester 3", 4),
            new Eenheid(16, "Semester 4", 4),
            new Eenheid(17, "Semester 1", 5)
        };
        private List<Onderdeel> _onderdelen = new List<Onderdeel>()
        {
            new Onderdeel(1,"LP", 1),
            new Onderdeel(2, "Proftaak", 1),
            new Onderdeel(3, "OIB",17),
            new Onderdeel(4, "OIS",17)
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
            foreach (Taak taak in _taken)
            {
                if (taak.TaakId == id)
                {
                    return taak.TaakNaam;
                }
            }

            return null;
        }

        public List<Taak> TakenOphalen(string blokeigenaarID)
        {
            return _taken;
        }

        public void TaakToevoegen(Taak taak)
        {
            bool dubbeletaak = false;
            string correcteOnderdeelNaam = null;
            foreach (Onderdeel currentOnderdeel in _onderdelen)
            {
                if (currentOnderdeel.OnderdeelId == taak.OnderdeelId)
                {
                    correcteOnderdeelNaam = currentOnderdeel.OnderdeelNaam;
                    break;
                }
            }
            taak.OnderdeelNaam = correcteOnderdeelNaam;

            foreach (Taak taak1 in _taken)
            {
                if (taak1.TaakId == taak.TaakId)
                {
                    dubbeletaak = true;
                }
            }
    
            
            if (!dubbeletaak)
            {
                _taken.Add(taak);
            }
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
        if (taak.Omschrijving != null)
        {
            taakToUpdate.Omschrijving = taak.Omschrijving;
        }

        if (taak.TaakNaam != null)
        {
            taakToUpdate.TaakNaam = taak.TaakNaam;
        }

        if (taak.BenodigdeUren != 0)
        {
            taakToUpdate.BenodigdeUren = taak.BenodigdeUren;
        }

        if (taak.AantalKlassen != 0)
        {
            taakToUpdate.AantalKlassen = taak.AantalKlassen;
        }

        if (taak.OnderdeelNaam != null)
        {
            taakToUpdate.OnderdeelNaam = taak.OnderdeelNaam;
        }

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

        public List<Eenheid> OphalenEenhedenBlokeigenaar(string blokeigenaarId)
        {
            throw new NotImplementedException();
        }

        public string OphalenOnderdeelNaamVanTaak(int id)
        {
            throw new NotImplementedException();
        }

        public string OphalenEenheidNaamVanTaak(int id)
        {
            throw new NotImplementedException();
        }

        public List<Eenheid> OphalenEenhedenBlokeigenaar(int blokeigenaarId)
        {
            throw new NotImplementedException();
        }
    }
}
