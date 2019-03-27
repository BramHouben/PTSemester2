using System.ComponentModel.DataAnnotations;

namespace ProjectinternDB.Models
{
    public class VoorkeurViewModel
    {

        [Required]
        public string Vak_naam { get; set; }
        [Required]
        public int Prioriteit { get; set; }
    }
}