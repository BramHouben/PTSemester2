using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Team
    {
        public Team()
        {

        }

        public Team(int teamID)
        {
            TeamId = teamID;
        }

        public Team(int teamID, int teamleiderID, int curriculumEigenaarID)
        {
            TeamId = teamID;
            TeamleiderID = teamleiderID;
            CurriculumEigenaarID = curriculumEigenaarID;
        }

        public int TeamId { get; set; }
        public int TeamleiderID { get; set; }
        public string TeamleiderNaam { get; set; }
        public int CurriculumEigenaarID { get; set; }
        public string CurriculumEigenaarNaam { get; set; }
        public IEnumerable<Docent> Docenten { get; set; }
    }
}