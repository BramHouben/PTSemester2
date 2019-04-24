using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Logic
{
    class OnderwijsLogic
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
    }
}
