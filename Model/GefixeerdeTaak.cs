using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class GefixeerdeTaak
    {
        public int Fix_id { get; set; }
        public int DocentID { get; set; }
        public string DocentNaam { get; set; }
        public int Taak_id { get; set; }
        public string TaakNaam { get; set; }

        public GefixeerdeTaak()
        {

        }
    }
}
