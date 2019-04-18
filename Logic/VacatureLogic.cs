using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
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
        public List<Vacature> VacaturesOphalen()
        {
            return vacatureRepository.VacaturesOphalen();
        }

        public void DeleteVacature(int id)
        {
          vacatureRepository.DeleteVacature(id);
        }

        public Vacature VacatureOphalen(int id)
        {
            return vacatureRepository.VacatureOphalen(id);
        }
    }
}
