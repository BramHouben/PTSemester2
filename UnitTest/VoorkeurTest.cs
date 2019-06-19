using Data.Context;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Onderwijsdelen;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class VoorkeurTest
    {
        private VoorkeurLogic voorkeurLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            voorkeurLogic = new VoorkeurLogic(new VoorkeurMemoryContext());
        }

        [TestMethod]
        public void VoorkeurAanmakenOphalen()
        {
            //Arrange
            Voorkeur presetVoorkeur = new Voorkeur()
            {
                TrajectNaam = "Software",
                EenheidNaam = "Semester 1",
                OnderdeelNaam = "Proftaak",
                TaakNaam = "Proftaak Begeleider",
                Id = 1,
            };
            Voorkeur voorkeur = new Voorkeur();
            voorkeurLogic.AddVoorkeur(presetVoorkeur.TrajectNaam, presetVoorkeur.EenheidNaam, presetVoorkeur.OnderdeelNaam, presetVoorkeur.TaakNaam, "User1");
            //Act
            foreach (Voorkeur voorkeurtemp in voorkeurLogic.OphalenVoorkeur("User1"))
            {
                if (voorkeurtemp.TaakNaam == presetVoorkeur.TaakNaam)
                {
                    voorkeur = voorkeurtemp;
                }
            }

            //Assert
            Assert.AreEqual(presetVoorkeur.EenheidNaam, voorkeur.EenheidNaam);
            Assert.AreEqual(presetVoorkeur.OnderdeelNaam, voorkeur.OnderdeelNaam);
            Assert.AreEqual(presetVoorkeur.TaakNaam, voorkeur.TaakNaam);
            Assert.AreEqual(presetVoorkeur.TrajectNaam, voorkeur.TrajectNaam);
        }

        [TestMethod]
        public void DeleteVoorkeur()
        {
            //Arrange
            Voorkeur presetVoorkeur = new Voorkeur()
            {
                TrajectNaam = "Software",
                EenheidNaam = "Semester 1",
                OnderdeelNaam = "Proftaak",
                TaakNaam = "Proftaak Begeleider",
                Id = 1,
            };
            voorkeurLogic.AddVoorkeur(presetVoorkeur.TrajectNaam, presetVoorkeur.EenheidNaam, presetVoorkeur.OnderdeelNaam, presetVoorkeur.TaakNaam, "User1");

            //Act
            voorkeurLogic.DeleteVoorkeur(1);
            List<Voorkeur> voorkeuren = voorkeurLogic.OphalenVoorkeur("User1");
            bool value = false;
            foreach (Voorkeur voorkeur in voorkeuren)
            {
                if (voorkeur.Id == 1)
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(false, value);
        }

        [TestMethod]
        public void OnderdelenByEenheidId()
        {
            //Arrange

            //Act
            List<Onderdeel> onderdelen = voorkeurLogic.GetOnderdelenByEenheidId(1);
            bool value = false;
            foreach (Onderdeel onderdeel in onderdelen)
            {
                if (onderdeel.EenheidId == 1)
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void EenhedenByTrajectId()
        {
            //Arrange

            //Act
            List<Eenheid> onderdelen = voorkeurLogic.GetEenhedenByTrajectId(1);
            bool value = false;
            foreach (Eenheid eenheid in onderdelen)
            {
                if (eenheid.TrajectId == 1)
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void TakenByOnderdeelId()
        {
            //Arrange

            //Act
            List<Taak> taken = voorkeurLogic.GetTakenByOnderdeelId(1);
            bool value = false;
            foreach (Taak taak in taken)
            {
                if (taak.TaakId == 1)
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void TaakInfo()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(voorkeurLogic.GetTaakInfo(1));
        }

        [TestMethod]
        public void DocentenList()
        {
            //Arrange

            //Act
            List<Medewerker> medewerkers = voorkeurLogic.GetDocentenList("1");
            bool value = false;
            foreach (Medewerker medewerker in medewerkers)
            {
                if (medewerker.MedewerkerId == "1")
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void KijkVoorDubbel()
        {
            Voorkeur voorkeur = new Voorkeur(343, "TestDubbel");
            voorkeurLogic.AddVoorkeur("Testdubbel", "TestDubbel", "TestDubbel", "TestDubbel", "343");
            Assert.IsTrue(voorkeurLogic.KijkenVoorDubbel("Testdubbel", "TestDubbel", "TestDubbel", "TestDubbel", "343"));
        }

        // TODO afmaken VoorkeurTests
    }
}