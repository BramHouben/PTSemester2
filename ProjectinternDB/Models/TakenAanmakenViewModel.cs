using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Model;
using Model.Onderwijsdelen;

namespace ProjectinternDB.Models
{
    public class TakenAanmakenViewModel
    {

        [Required]
        public List<Traject> OnderwijsTrajecten { get; set; }

        [Required]
        public List<Eenheid> OnderwijsEenheden { get; set; }

        [Required]
        public List<Onderdeel> OnderwijsOnderdelen { get; set; }

        public List<Taak> OnderwijsTaken { get; set; }
    }
}
