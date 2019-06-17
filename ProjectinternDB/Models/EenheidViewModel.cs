using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectinternDB.Models
{
    public class EenheidViewModel
    {
        [FromQuery(Name = "ECTS")]
        public int ECTS { get; set; }

        [FromQuery(Name = "AantalKlassen")]
        public int AantalKlassen { get; set; }
    }
}
