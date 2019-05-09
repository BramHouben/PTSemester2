using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Onderdeel
    {
        [Key]
        public int OnderdeelId { get; set; }
        public string OnderdeelNaam { get; set; }
        public int EenheidId { get; set; }
        public Onderdeel()
        {

        }

        public Onderdeel(int onderdeelId, string onderdeelNaam, int eenheidId)
        {
            OnderdeelId = onderdeelId;
            OnderdeelNaam = onderdeelNaam;
            EenheidId = eenheidId;
        }
    }
}
