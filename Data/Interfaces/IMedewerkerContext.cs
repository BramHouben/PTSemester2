using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IMedewerkerContext
    {
        Medewerker GetMedewerkerId(string id);
    }
}
