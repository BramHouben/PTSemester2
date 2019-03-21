using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Voorkeur
    {
        public Voorkeur()
        {

        }

        public Voorkeur(string vak_naam, int prioriteit)
        {
            Vak_naam = vak_naam;
            Prioriteit = prioriteit;
        }

        public Voorkeur(int id, string vak_naam, int prioriteit)
        {
            Id = id;
            Vak_naam = vak_naam;
            Prioriteit = prioriteit;
        }

        public int Id { get; set; }

        [Required]
        public string Vak_naam { get; set; }

        public int Prioriteit { get; set; }
    }
}