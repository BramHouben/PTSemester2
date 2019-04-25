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
        public int VacatureID { get; set; }
        [Required]
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public int TaakID { get; set; }
        public string OnderwijsTaakNaam { get; set; }
    }
}
