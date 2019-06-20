using Data;
using Data.Interfaces;
using Model;
using Model.AlgoritmeMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class AlgoritmeLogic
    {
        private static AlgoritmeRepo algoritmeRepo;
     private   List<ADocent> users = new List<ADocent>();
        public AlgoritmeLogic(IAlgoritmeContext algoritmeContext)
        {
            algoritmeRepo = new AlgoritmeRepo(algoritmeContext);
        }

        public List<Algoritme> ActiverenSysteen()
        {
            return algoritmeRepo.ActiverenSysteem();
        }

        private List<int> OphalenGefixeerdeTakenID()
        {
            return algoritmeRepo.OphalenGefixeerdeTakenID();
        }
        
        private void GefixeerdeDocentenToevoegen()
        {
            
            foreach(int TaakID in OphalenGefixeerdeTakenID())
            {
               foreach(int docentID in algoritmeRepo.DocentenIDsOphalenMetTaakID(TaakID))
               {
                    algoritmeRepo.GefixeerdeLijstDoorvoeren(docentID, TaakID);
               }
            }
        }

        private void GefixeerdeMensenInvoeren()
        {
            GefixeerdeDocentenToevoegen();
        }

        public void AlgoritmeStarten()
        {
            algoritmeRepo.DeleteTabel();
            Indelen();
        }

        public void Indelen()
        {
            GefixeerdeMensenInvoeren();
            foreach (var taak in algoritmeRepo.TakenOphalen())
            {
                List<ADocent> DocentenScores = new List<ADocent>();

                foreach (var docent in algoritmeRepo.InzetbareDocenten(taak.TaakID))
                {
                    int prioriteit = PrioriteitOphalen(docent.voorkeuren, taak.TaakID);

                    docent.Score = ScoreBerekenen(docent.InzetbareUren, taak.BenodigdeUren, taak.AantalKeerGekozen, taak.AantalKlassen, prioriteit);

                    DocentenScores.Add(docent);
                    //Console.WriteLine(taak.TaakNaam + "  " + taak.AantalKeerGekozen + "  " + taak.AantalKlassen);
                }

                List<ADocent> GesorteerdeDocentenScores = DocentenScores.OrderByDescending(o => o.Score).ToList();
                for (int klas = 0; klas < taak.AantalKlassen; klas++)
                {
                    if (GesorteerdeDocentenScores.Count == 0)
                    {
                        ZetinDbNull(taak.TaakID);
                    }
                    else
                    {
                        ZetinDb(GesorteerdeDocentenScores.First().docentID, taak.TaakID);
                        int ID = IdOphalen(GesorteerdeDocentenScores.First().voorkeuren, GesorteerdeDocentenScores.First().docentID);
                        VerwijderVoorkeur(GesorteerdeDocentenScores.First().docentID, ID);
                        GesorteerdeDocentenScores.Remove(GesorteerdeDocentenScores.First());
                    }
                }
            }
        }

        private void VerwijderVoorkeur(int docentID, int ID)
        {
            algoritmeRepo.VerwijderVoorkeur(docentID, ID);
        }

        private void ZetinDb(int docentID, int taakID)
        {
            algoritmeRepo.ZetinDb(docentID,taakID);
        }

        private void ZetinDbNull(int taakID)
        {
            algoritmeRepo.ZetinDbNull(taakID);
        }

        private int IdOphalen(List<AVoorkeur> voorkeuren, int docentID)
        {
            foreach (var voorkeur in voorkeuren)
            {
                return voorkeur.VoorkeurID;
            }
            throw new Exception();
        }

        private double ScoreBerekenen(int inzetbareUren, int benodigdeUren, int aantalKeerGekozen, int aantalKlassen, int prioriteit)
        {
            double score = 100;

            score -= (double)inzetbareUren / benodigdeUren * prioriteit;

            return score;
        }

        private int PrioriteitOphalen(List<AVoorkeur> voorkeuren, int taakID)
        {
            foreach (var voorkeur in voorkeuren)
            {
                if (voorkeur.TaakID == taakID)
                {
                    return voorkeur.Prioriteit;
                }
            }
            throw new Exception();
        }
    }
}