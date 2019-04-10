using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Voorkeur
    {
        public Voorkeur()
        {

        }

        public Voorkeur(int traject_naam, int prioriteit, int onderdeel_naam, int taak_naam)
        {
            Traject_naam = traject_naam;
            Onderdeel_naam = onderdeel_naam;
            Taak_naam = taak_naam;
            Prioriteit = prioriteit;
        }

        public Voorkeur(int id, int vak_naam, int prioriteit)
        {
            Id = id;
            Taak_naam = vak_naam;
            Prioriteit = prioriteit;
        }

        public int Id { get; set; }
        public int Traject_naam { get; set; }
      
        public int Taak_naam { get; set; }

        public int Onderdeel_naam { get; set; }
        public int Prioriteit { get; set; }
    }
}