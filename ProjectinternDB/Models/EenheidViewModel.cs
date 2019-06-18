using System;
using System.ComponentModel.DataAnnotations;

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
