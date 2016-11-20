using Git.Stats.Models.Statistics;
using Git.Stats.Report;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure
{
    public sealed class GitStatisticService
    {
        private readonly PowerShellExecutor powerShellExecutor;
        private const string statsCommand = "git log --shortstat";
        private readonly IReportBuilder reportBuilder;

        public GitStatisticService()
        {
            this.powerShellExecutor = new PowerShellExecutor();
            reportBuilder = new PlaiTextReportBuilder();
        }

        public string Execute(_Command command)
        {
            var statistic = GetStatistic(command);
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
