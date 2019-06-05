using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Model;
using Model.Onderwijsdelen;

namespace ProjectinternDB.Models
{
    public class BlokeigenaarViewModel
    {
        [Required]
        public string TaakNaam { get; set; }
        [Required]
        public string Omschrijving { get; set; }
        [Required]
        public Taak Taak { get; set; }
        
        public int AantalKlassen { get; set; }
        public int BenodigdeUren { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Selecteer een Eenheid")]
        public int EenheidId { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Selecteer een Onderdeel")]
        public int OnderdeelId { get; set; }

        public string EenheidNaam { get; set; }
        public string OnderdeelNaam { get; set; }
    }
}
