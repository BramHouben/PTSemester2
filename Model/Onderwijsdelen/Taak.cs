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
        public string Omschrijving { get; set; }
        public int BenodigdeUren { get; set; }
        public int AantalKlassen { get; set; }
        public int AantalBekwaam { get; set; }
        public string TrajectNaam { get; set; }
        public string OnderdeelNaam { get; set; }
        public string EenheidNaam { get; set; }


        public Taak()
        {
        }

        public Taak(int taakId, string taakNaam)
        {
            TaakId = taakId;
            TaakNaam = taakNaam;
        }
        public Taak(int taakId, string taakNaam, int onderdeelId, string trajectNaam, string onderdeelNaam, string eenheidNaam)
        {
            TaakId = taakId;
            TaakNaam = taakNaam;
            OnderdeelId = onderdeelId;
            TrajectNaam = trajectNaam;
            OnderdeelNaam = onderdeelNaam;
            EenheidNaam = eenheidNaam;
        }
        public Taak(int taakId, string taakNaam, int onderdeelId, string omschrijving, string trajectNaam, string onderdeelNaam, string eenheidNaam)
        {
            TaakId = taakId;
            TaakNaam = taakNaam;
            OnderdeelId = onderdeelId;
            Omschrijving = omschrijving;
            TrajectNaam = trajectNaam;
            OnderdeelNaam = onderdeelNaam;
            EenheidNaam = eenheidNaam;
        }

        public override string ToString()
        {
            return TaakNaam;
        }
    }
}
