using System.Collections.Generic;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

namespace Data.Context
{
    public class BlokeigenaarMemoryContext : IBlokeigenaarContext
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
            _taken.Remove(TakenOphalen()[id]);
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