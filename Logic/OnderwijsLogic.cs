using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Logic
{
    public class OnderwijsLogic
    {
        private OnderwijsRepository _onderwijsRepository;

        public OnderwijsLogic()
        {
            _onderwijsRepository = new OnderwijsRepository();
        }
        public OnderwijsLogic(IOnderwijsContext context)
        {
            _onderwijsRepository = new OnderwijsRepository(context);
        }
        public string OnderwijstaakNaam(int id)
        {
            return _onderwijsRepository.OnderwijstaakNaam(id);
        }

        public List<Taak> TakenOphalen()
        {
            return _onderwijsRepository.TakenOphalen();
        }

        public void TaakAanmaken(Taak taak)
        {
            _onderwijsRepository.TaakOpslaan(taak);
        }

      

        public Taak TaakOphalen(int id)
        {
            Taak taak = _onderwijsRepository.TaakOphalen(id);
            return taak;
        }

        public void TaakVerwijderen(int id)
        {
            _onderwijsRepository.TaakVerwijderen(id);
        }
    }
}
