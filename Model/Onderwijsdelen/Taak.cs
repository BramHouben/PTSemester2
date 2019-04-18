using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Taak
    {
        [Key]
        public int TaakId { get; set; }
        public string TaakNaam { get; set; }
        public int OnderdeelId { get; set; }

        public Taak(int taakId, string taakNaam)
        {
            TaakId = taakId;
            TaakNaam = taakNaam;
        }

        public Taak()
        {

        }
    }
}
