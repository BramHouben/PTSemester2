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
        private List<Onderdeel> _onderdelen = new List<Onderdeel>();
        private List<Taak> _taken = new List<Taak>() {
            new Taak(1,"Test-taak1"),
            new Taak(2,"Test-taak2"),
            new Taak(3,"Test-taak3"),
            new Taak(4,"Test-taak4")
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
                Console.WriteLine("Oopsie");
            }
        }

        public Taak TaakOphalen(int id)
        {
            throw new NotImplementedException();
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
