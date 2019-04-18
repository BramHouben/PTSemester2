using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Voorkeur
    {
        public Voorkeur()
        {

        }

        public Voorkeur(string trajectNaam, string eenheidNaam, string onderdeelNaam, string taakNaam, int prioriteit)
        {
            TrajectNaam = trajectNaam;
            EenheidNaam = eenheidNaam;
            OnderdeelNaam = onderdeelNaam;
            TaakNaam = taakNaam;
            Prioriteit = prioriteit;
         
        }

        public Voorkeur(int id, string vak_naam, int prioriteit)
        {
            Id = id;
            TaakNaam = vak_naam;
            Prioriteit = prioriteit;
        }

        public int Id { get; set; }

        public string TrajectNaam { get; set; }
      
        public string TaakNaam { get; set; }

        public string OnderdeelNaam { get; set; }

        public string EenheidNaam { get; set; }

        public int Prioriteit { get; set; }

        public string Taak_info { get; set; }
    }
}