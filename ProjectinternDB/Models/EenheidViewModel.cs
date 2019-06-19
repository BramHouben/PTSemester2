using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectinternDB.Models
{
    public class EenheidViewModel
    {
        [Range(Int32.MinValue, 0, ErrorMessage = "Mag niet lager als nul")]
        public int ECTS { get; set; }

        [Range(Int32.MinValue, 0, ErrorMessage = "Mag niet lager als nul")]
        public int AantalKlassen { get; set; }
    }
}
