using Model.Onderwijsdelen;
using System.ComponentModel.DataAnnotations;

namespace ProjectinternDB.Models
{
    public class VoorkeurViewModel
    {

        [Required]
        public Traject traject { get; set; }
        [Required]
        public Onderdeel onderdeel { get; set; }
        [Required]
        public Taak taak { get; set; }
        [Required]
        public int Prioriteit { get; set; }
    }
}