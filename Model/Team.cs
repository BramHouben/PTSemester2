﻿using System;
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
            Id = teamID;
        }

        public Team(int teamID, int teamleiderID, int curriculumEigenaarID)
        {
            Id = teamID;
            TeamleiderID = teamleiderID;
            CurriculumEigenaarID = curriculumEigenaarID;
        }

        public int Id { get; set; }
        public int TeamleiderID { get; set; }
        public int CurriculumEigenaarID { get; set; }

    }
}
