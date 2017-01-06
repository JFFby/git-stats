using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Stats.Infrastructure.Services.Arguments;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;
using Git.Stats.Report;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    class DateDifService : IDateDifService
    {
        private readonly IDateDifReportBuilder reportBuilder;
        private readonly IStatisticStorage storage;
        private readonly IGroupingFactory groupingFactory;

        public DateDifService(
            IDateDifReportBuilder reportBuilder,
            IStatisticStorage storage,
            IGroupingFactory groupingFactory)
        {
            this.reportBuilder = reportBuilder;
            this.storage = storage;
            this.groupingFactory = groupingFactory;
        }

        public string GetDateDifStatistic(Command.Infrastructure.Models.Command command)
        {
            var args = command.Args.Split(' ').Select(x => x.Trim()).ToList();
            var stringDates = args.Take(2).ToArray();
            var statistic = storage.Get();
            if (CheckArgs(stringDates, statistic) != null)
                return CheckArgs(stringDates, statistic);

            var dates = ParseDates(stringDates);
            var actualCommits = statistic.Commits
                .Where(x => x.Date.Date >= dates[0] && x.Date.Date <= dates[1])
                .ToList();
            var groupingArguments = new GroupingArgs(actualCommits, GetMethod(args), dates[0], dates[1]);
            var groups = groupingFactory.GroupBy(groupingArguments);

            return BuildReport(groups);
        }

        private string GetMethod(IList<string> args)
        {
            return args.Count > 2
                ? args[2]
                : string.Empty;
        }

        private string BuildReport(IList<GroupedStatistic> groups)
        {
            var report = new StringBuilder();
            foreach (var group in groups)
            {
                var groupReport = reportBuilder.BuildReport(group.Statistc, group.From, group.To);
                report.Append(groupReport);
                report.Append(Environment.NewLine);
            }

            return report.ToString();
        }

        private string CheckArgs(string[] dates, Statistic statistic)
        {
            if (dates.Length != 2)
            {
                return "Specify two dates";
            }

            try
            {
                var parsedDates = ParseDates(dates);
            }
            catch (Exception)
            {
                return "Can't parse dates";
            }

            return statistic != null ? null : BetweenService.RepoNotSelected;
        }

        private IList<DateTime> ParseDates(string[] dates)
        {
            return dates.Select(x => DateTime.Parse(x.Trim()))
                   .ToList();
        }
    }
}
