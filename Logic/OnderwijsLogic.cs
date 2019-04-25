using System;
using System.Collections.Generic;
using System.Text;
using Data;
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
        public string OnderwijstaakNaam(int id)
        {
            return _onderwijsRepository.OnderwijstaakNaam(id);
        }

        public List<Taak> TakenOphalen()
        {
            return _onderwijsRepository.TakenOphalen();
        }
    }
}
