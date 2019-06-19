using Data.Context;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTest
{
    [TestClass]
    public class TeamTest
    {
        private TeamLogic teamLogic;

        [TestInitialize]
        public void Start()
        {
            teamLogic = new TeamLogic(new TeamMemoryContext());
        }

        [TestMethod]
        public void TeamsOphalen()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(teamLogic.TeamsOphalen());
        }

        [TestMethod]
        public void TeamAanmakenEnDocentInTeamOphalen()
        {
            //Arrange
            // TODO Team Aanmaken en code aanvullen
            teamLogic.VoegDocentToeAanTeam(1, 1);
            bool value = false;
            //Act
            foreach (Team team in teamLogic.TeamsOphalen())
            {
                if (team.TeamId == 1)
                {
                    foreach (Docent docent in team.Docenten)
                    {
                        if (docent.DocentId == 1)
                        {
                            value = true;
                        }
                    }
                }
            }

            //Assert
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void TeamleiderNaamMetTeamleiderId()
        {
            //Arrange

            //Act
            string naam = teamLogic.TeamleiderNaamMetTeamleiderId(1);
            //Assert
            Assert.AreEqual("Jantje de Boer", naam);
        }

        [TestMethod]
        public void CurriculumEigenaarNaamMetCurriculumEigenaarId()
        {
            //Arrange

            //Act
            string naam = teamLogic.CurriculumEigenaarNaamMetCurriculumEigenaarId(1);
            //Assert
            Assert.AreEqual("Hans Klok", naam);
        }

        [TestMethod]
        public void TeamOphalenMetID()
        {
            //Arrange
            Team team;
            //Act
            team = teamLogic.TeamOphalenMetID(1);
            //Assert
            Assert.IsNotNull(team);
        }

        [TestMethod]
        public void HaalDocentenZonderTeamOp()
        {
            //Arrange
            Team team;
            //Act
            team = teamLogic.TeamOphalenMetID(1);
            //Assert
            Assert.IsNotNull(team);
        }

        [TestMethod]
        public void HaalDocentOpMetID()
        {
            //Arrange
            Docent docent;
            //Act
            docent = teamLogic.HaalDocentOpMetID(1);
            //Assert
            Assert.AreEqual("Jan", docent.Naam);
        }
    }
}