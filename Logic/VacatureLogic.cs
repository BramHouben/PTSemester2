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
        private VacatureRepository _vacatureRepository;
        private OnderwijsLogic _onderwijsLogic;
        public VacatureLogic()
        {
            _vacatureRepository = new VacatureRepository();
            _onderwijsLogic = new OnderwijsLogic();
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
            _vacatureRepository.VacatureOpslaan(vacature);
        }
        public List<Vacature> VacaturesOphalen()
        {
            List<Vacature> vacatures = _vacatureRepository.VacaturesOphalen();
            foreach (Vacature vacature in vacatures)
            {
                vacature.OnderwijsTaakNaam = _onderwijsLogic.OnderwijstaakNaam(vacature.OnderwijstaakID);
            }
            return vacatures;
        }

        public void DeleteVacature(int id)
        {
          _vacatureRepository.DeleteVacature(id);
        }

        public Vacature VacatureOphalen(int id)
        {
            Vacature vacature = _vacatureRepository.VacatureOphalen(id);
            vacature.OnderwijsTaakNaam = _onderwijsLogic.OnderwijstaakNaam(vacature.OnderwijstaakID);
            return vacature;
        }

        public void UpdateVacature(Vacature vac)
        {
            _vacatureRepository.UpdateVacature(vac);
        }
    }
}
