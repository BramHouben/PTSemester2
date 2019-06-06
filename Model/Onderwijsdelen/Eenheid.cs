using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Eenheid
    {
        [Key]
        public int EenheidId { get; set; }
        public string EenheidNaam { get; set; }
        public int TrajectId { get; set; }
        public string TrajectNaam { get; set; }
        public int ECTS { get; set; }
        public int AantalKlassen { get; set; }

        public Eenheid()
        {

        }

        public Eenheid(int eenheidId, string eenheidNaam, int trajectId)
        {
            EenheidId = eenheidId;
            EenheidNaam = eenheidNaam;
            TrajectId = trajectId;
        }

        public Eenheid(int eenheidId, string eenheidNaam, string trajectNaam, int ects, int aantal)
        {
            EenheidId = eenheidId;
            EenheidNaam = eenheidNaam;
            TrajectNaam = trajectNaam;
            ECTS = ects;
            AantalKlassen = aantal;
        }
    }
}
