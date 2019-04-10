using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model;

namespace ProjectinternDB.Models
{
    public class TeamViewModel
    {
        [Required]
        public List<Docent> Docenten { get; set; }
        public List<Team> Teams { get; set; }
        public int TeamleiderID { get; set; }
        public int CurriculumEigenaarID { get; set; }
        public string TeamleiderNaam { get; set; }
        public List<Docent> DocentenZonderTeam { get; set; }
        public int TeamID { get; set; }
    }
}