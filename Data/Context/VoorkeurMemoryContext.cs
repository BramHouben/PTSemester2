using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Data.Context
{
    class VoorkeurMemoryContext : IVoorkeurContext
    {
        private static List<Voorkeur> voorkeuren = new List<Voorkeur>();

        public VoorkeurMemoryContext()
        {
            if (voorkeuren.Count == 0)
            {
                voorkeuren.Add(new Voorkeur(1, "LP", 3));
                voorkeuren.Add(new Voorkeur(2, "Vak2", 3));
                voorkeuren.Add(new Voorkeur(3, "Vak3", 3));
                voorkeuren.Add(new Voorkeur(4, "Vak4", 3));
            }
        }

        public List<Voorkeur> VoorkeurenOphalen()
        {
            return voorkeuren;
        }

        public void VoorkeurToevoegen(Voorkeur voorkeur)
        {
            voorkeuren.Add(voorkeur);
        }

        public void TestDataAanmaken()
        {
            voorkeuren.Add(new Voorkeur(1, "LP", 3));
            voorkeuren.Add(new Voorkeur(2, "Vak2", 3));
            voorkeuren.Add(new Voorkeur(3, "Vak3", 3));
            voorkeuren.Add(new Voorkeur(4, "Vak4", 3));
        }
    }
}
