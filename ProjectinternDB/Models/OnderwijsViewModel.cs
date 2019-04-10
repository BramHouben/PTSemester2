using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectinternDB.Models
{
    public class OnderwijsViewModel
    {
        [Required]
        public List<Docent> Docenten { get; set; }

        [Required]
        public List<Onderwijstraject> OnderwijsTrajecten { get; set; }

        [Required]
        public List<Onderwijseenheid> OnderwijsEenheden { get; set; }

        [Required]
        public List<Onderwijsonderdeel> OnderwijsOnderdelen { get; set; }

        [Required]
        public List<Onderwijstaak> OnderwijsTaken { get; set; }
    }
}
