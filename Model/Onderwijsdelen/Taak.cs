using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Taak
    {
        [Key]
        public int TaakId { get; set; }
        public string TaakNaam { get; set; }
        public int OnderdeelId { get; set; }
        public string Taak_info { get; set; }
    }
}
