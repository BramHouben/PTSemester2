using Data;
using System;
using Model;
using System.Collections.Generic;

namespace Logic
{
    public class VoorkeurLogic
    {
        private VoorkeurRepository VoorkeurRepo = new VoorkeurRepository();

        public void AddVoorkeur(string vak_naam, int prioriteit)
        {
            if (prioriteit > 5 || prioriteit < 0)
            {
                throw new ArgumentException("Iets Fout met het proces, Probeer opnieuw");
                    
            }
            VoorkeurRepo.AddVoorkeur(new Voorkeur(vak_naam, prioriteit));
        }



        public List<Voorkeur> OphalenVoorkeur()
        {
            return VoorkeurRepo.vkmodelList;
        }
    }
}