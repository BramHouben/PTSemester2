using System.ComponentModel.DataAnnotations;

namespace ProjectinternDB.Models
{
    public class VoorkeurViewModel
    {
        public string TrajectId { get; set; }

        [Required]
        public string TrajectNaam { get; set; }

        [Required]
        public int Prioriteit { get; set; }

        public string Semester { get; set; }

        public string Taak_info { get; set; }
    }
}