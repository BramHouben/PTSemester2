using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectinternDB.Models
{
    public class VoorkeurViewModel
    {
        //[Required(ErrorMessage ="Selecteer een Traject")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Selecteer een Traject")]
        public string TrajectId { get; set; }

        [Required]
        public string TrajectNaam { get; set; }

        [Required]
        public int Prioriteit { get; set; }

        public string Semester { get; set; }

        public string Taak_info { get; set; }

        [Required(ErrorMessage = "Selecteer een misdaad!")]
        public List<Medewerker> MedewerkerList { get; set; }

    }
}