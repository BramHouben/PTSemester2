using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Onderdeel
    {
        [Key]
        public int OnderdeelId { get; set; }
        public string OnderdeelNaam { get; set; }
        public int TrajectId { get; set; }
    }
}
