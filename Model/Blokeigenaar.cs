using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Blokeigenaar : Medewerker
    {
        public Blokeigenaar(int blokeigenaarId)
        {
            BlokeigenaarId = blokeigenaarId;
        }

        public Blokeigenaar()
        {

        }

        public int BlokeigenaarId { get; set; }
    }
}
