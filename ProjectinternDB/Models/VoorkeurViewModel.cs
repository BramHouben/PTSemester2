using Model.Onderwijsdelen;
using System.ComponentModel.DataAnnotations;

namespace ProjectinternDB.Models
{
    public class VoorkeurViewModel
    {
        public string TrajectId { get; set; }

        public string TrajectNaam { get; set; }

        [Required]
        public int Prioriteit { get; set; }

        //public Onderdeel Onderdeel { get; set; }

        //public Taak Taak { get; set; }
    }
}