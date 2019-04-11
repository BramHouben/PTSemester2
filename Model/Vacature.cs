using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
  public  class Vacature
    {
        public Vacature()
        {

        }
        public int VactureID { get; set; }
        [Required]
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public int OnderwijstaakID { get; set; }
    }
}
