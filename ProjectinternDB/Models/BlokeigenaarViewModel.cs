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
        /*[Required]
        public List<Traject> OnderwijsTrajecten { get; set; }

        [Required]
        public List<Eenheid> OnderwijsEenheden { get; set; }*/

        [Required]
        public string TaakNaam { get; set; }

        [Required]
        public string OnderdeelNaam { get; set; }

        [Required]
        public string Omschrijving { get; set; }
        [Required]
        public Taak Taak { get; set; }


        [Required]
        public List<Onderdeel> OnderwijsOnderdelen { get; set; }

        [Required]
        public string TrajectNaam { get; set; }

        public string TrajectId { get; set; }
    }
}
