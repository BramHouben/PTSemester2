using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Eenheid
    {
        [Key]
        public int EenheidId { get; set; }
        public string EenheidNaam { get; set; }
        public int TrajectId { get; set; }

        public Eenheid()
        {
        }
        public Eenheid(int eenheidId, string eenheidNaam, int trajectId)
        {
            EenheidId = eenheidId;
            EenheidNaam = eenheidNaam;
            TrajectId = trajectId;
        }
    }
}
