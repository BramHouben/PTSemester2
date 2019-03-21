using System;
using System.Collections.Generic;
using Model;
namespace Data
{

    public interface IVoorkeurContext
    {
        List<Voorkeur>VoorkeurenOphalen();

        void VoorkeurToevoegen(Voorkeur voorkeur);
    }

}