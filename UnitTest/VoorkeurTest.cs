using System.Collections.Generic;
using Data;
using Data.Context;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Onderwijsdelen;

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
            
            voorkeurLogic.AddVoorkeur(presetVoorkeur.TrajectNaam, presetVoorkeur.EenheidNaam, presetVoorkeur.OnderdeelNaam, presetVoorkeur.TaakNaam, "User1");
            //Act
            Voorkeur voorkeur = voorkeurLogic.OphalenVoorkeur("User1")[0];
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
            List<Voorkeur> voorkeuren= voorkeurLogic.OphalenVoorkeur("User1");
            bool value = false;
            foreach (Voorkeur voorkeur in voorkeuren)
            {
                if (voorkeur.Id == 1)
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(false,value);
        }

        [TestMethod]
        public void OnderdelenByEenheidId()
        {
            //Arrange
            // new Onderdeel(1, "OnderdeelTest", 1)
            //Act
            List<Onderdeel> onderdelen = voorkeurLogic.GetOnderdelenByEenheidId(1);
            bool value = false;
            foreach (var onderdeel in onderdelen)
            {
                if (onderdeel.EenheidId == 1)
                {
                    value = true;
                }
            }
            //Assert
            Assert.AreEqual(true, value);
        }
    }
}
