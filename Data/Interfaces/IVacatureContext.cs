using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Data.Interfaces
{
    interface IVacatureContext
    {
        List<Vacature> VacaturesOphalen();
        void VacatureOpslaan(Vacature vac);

    }
}