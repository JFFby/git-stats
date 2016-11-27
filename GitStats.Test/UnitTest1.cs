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

            Assert.AreEqual(BetweenService.RepoNotSelected, result);
        }

        [TestMethod]
        public void ShouldFindCommitsBetweenTwoSpecified()
        {
            var statistic = SetupStatistic();
            var first = statistic.Commits[7];
            var second = statistic.Commits[3];
            var command = new _Command("b", string.Join(" ", second.Name, first.Name));

            var result = betweenService.GetStatisticBetween(command);

            reportBuilder.Verify(x => x.BuildReport(It.Is<Statistic>(s => s.Commits.Count == 5)));
        }

        [TestMethod]
        public void ShouldFindCommitsBetweenTwoSpecified_2()
        {
            var statistic = SetupStatistic();
            var first = statistic.Commits[3];
            var second = statistic.Commits[3];
            var command = new _Command("b", string.Join(" ", second.Name, first.Name));

            var result = betweenService.GetStatisticBetween(command);

            reportBuilder.Verify(x => x.BuildReport(It.Is<Statistic>(s => s.Commits.Count == 1)));
        }

        [TestMethod]
        public void ShouldFindCommitsBetweenTwoSpecified_3()
        {
            var statistic = SetupStatistic();
            var first = statistic.Commits[3];
            var second = statistic.Commits[7];
            var command = new _Command("b", string.Join(" ", second.Name, first.Name));

            var result = betweenService.GetStatisticBetween(command);

            reportBuilder.Verify(x => x.BuildReport(It.Is<Statistic>(s => s.Commits.Count == 5)));
        }

        [TestMethod]
        public void ShouldFindCommitsBetweenTwoSpecified_4()
        {
            var statistic = SetupStatistic();
            var first = statistic.Commits[0];
            var second = statistic.Commits[9];
            var command = new _Command("b", string.Join(" ", second.Name, first.Name));

            var result = betweenService.GetStatisticBetween(command);

            reportBuilder.Verify(x => x.BuildReport(It.Is<Statistic>(s => s.Commits.Count == 10)));
        }

        private Statistic SetupStatistic()
        {
            var commits = fixture.CreateMany<Commit>(10).ToList();
            var statistic = fixture.Build<Statistic>()
                .With(x => x.Commits, commits)
                .Create();
            statisticStorage.Setup(x => x.Get()).Returns(() => statistic);
            return statistic;
        }
    }
}
