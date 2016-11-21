using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;
using Git.Stats.Report;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure.Services.Implementations  
{
    public sealed class GitStatisticService : IGitStatisticService
    {
        private const string statsCommand = "git log --shortstat";

        private readonly IStatisticStorage statisticStorage;
        private readonly PowerShellExecutor powerShellExecutor;
        private readonly IReportBuilder reportBuilder;

        public GitStatisticService(IStatisticStorage statisticStorage, IReportBuilder reportBuilder)
        {
            this.statisticStorage = statisticStorage;
            this.reportBuilder = reportBuilder;
            this.powerShellExecutor = new PowerShellExecutor();
        }

        public string Execute(_Command command)
        {
            var statistic = GetStatistic(command);
            statisticStorage.Save(statistic);
            return reportBuilder.BuildReport(statistic);
        }

        private Statistic GetStatistic(_Command command)
        {
            var pathCommand = $"{command.ExecutedCommand} {command.Args}";
            var commits = powerShellExecutor.Execute(pathCommand, statsCommand);
            var statisticParser = new StatisticParser(commits);
            return statisticParser.Parse();
        } 
    }
}
