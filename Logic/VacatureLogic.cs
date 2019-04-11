using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Model;

namespace Logic
{
    public class VacatureLogic
    {
        private VacatureRepository vacatureRepository;
        public VacatureLogic()
        {
            vacatureRepository = new VacatureRepository();
        }
        public void VacatureOpslaan(Vacature vacature)
        {
            // correcties voor correcte database input
            if (vacature.Naam == "")
            {
                vacature.Naam = null;
            }
            if (vacature.Omschrijving == "")
            {
                vacature.Omschrijving = null;
            }
            vacatureRepository.VacatureOpslaan(vacature);
        }
    }
}
