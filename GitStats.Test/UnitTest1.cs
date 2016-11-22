using System.Linq;
using Git.Stats.Infrastructure.Services.Implementations;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;
using Git.Stats.Report;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using _Command = Command.Infrastructure.Models.Command;

namespace GitStats.Test
{
    [TestClass]
    public class BetweenServiceTest
    {
        private Mock<IReportBuilder> reportBuilder;
        private Mock<IStatisticStorage> statisticStorage;
        private IFixture fixture;

        private IBetweenService betweenService;

        [TestInitialize]
        public void TestInitialize()
        {
            reportBuilder = new Mock<IReportBuilder>();
            statisticStorage = new Mock<IStatisticStorage>();
            fixture = new Fixture();

            betweenService = new BetweenService(reportBuilder.Object, statisticStorage.Object);
        }

        [TestMethod]
        public void ShouldReturnError_WhenSpecifiedNot2Commits()
        {
            statisticStorage.Setup(x => x.Get()).Returns(fixture.Create<Statistic>());
            var command = fixture.Create<_Command>();

            var result = betweenService.GetStatisticBetween(command);

            Assert.AreEqual(BetweenService.WrongArguments, result);
        }

        [TestMethod]
        public void ShouldReturnError_RepoIsNotSelectedYet()
        {
            statisticStorage.Setup(x => x.Get()).Returns(() => null);
            var command = new _Command("b", string.Join(" ", fixture.CreateMany<string>(2)));

            var result = betweenService.GetStatisticBetween(command);

            Assert.AreEqual(BetweenService.WrongArguments, result);
        }

        [TestMethod]
        public void ShouldFindCommitsBetweenTwoSpecified()
        {
            statisticStorage.Setup(x => x.Get()).Returns(() => null);
            var command = new _Command("b", string.Join(" ", fixture.CreateMany<string>(2)));
            var commits = fixture.CreateMany<Commit>(10).ToList();
            var first = commits[7];
            var second = commits[3];

            var result = betweenService.GetStatisticBetween(command);

            Assert.AreEqual(BetweenService.WrongArguments, result);
        }
    }
}
