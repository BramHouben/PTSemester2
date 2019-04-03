using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Docent : Medewerker
    {
        public Docent(int docentId, int teamId, int ruimteVoorInzet)
        {
            DocentId = docentId;
            TeamId = teamId;
            RuimteVoorInzet = ruimteVoorInzet;
        }

        public int DocentId { get; set; }
        public int TeamId { get; set; }
        public int RuimteVoorInzet { get; set; }
    }
}
