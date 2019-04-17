using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Onderwijsdelen
{
    public class Taak
    {
        [Key]
        public int TaakId { get; set; }
        public string TaakNaam { get; set; }
        public int OnderdeelId { get; set; }
    }
}
