using System;
using System.Collections.Generic;
using Model;
namespace Data
{

    public interface IVoorkeurContext
    {
        List<Voorkeur>VoorkeurenOphalen(string id);

        void VoorkeurToevoegen(Voorkeur voorkeur, string id);

        void DeleteVoorkeur(int id);
    }

}