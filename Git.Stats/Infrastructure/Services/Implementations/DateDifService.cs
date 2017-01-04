using System;
using System.Collections.Generic;
using System.Linq;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;
using Git.Stats.Report;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    class DateDifService : IDateDifService
    {
        private readonly IDateDifReportBuilder reportBuilder;
        private readonly IStatisticStorage storage;

        public DateDifService(IDateDifReportBuilder reportBuilder, IStatisticStorage storage)
        {
            this.reportBuilder = reportBuilder;
            this.storage = storage;
        }

        public string GetDateDifStatistic(Command.Infrastructure.Models.Command command)
        {
            var stringDates = command.Args.Split(' ');
              
            var statistic = storage.Get();
            if (CheckArgs(stringDates, statistic) != null)
                return CheckArgs(stringDates, statistic);

            var dates = ParseDates(stringDates);
            var actualCommits = statistic.Commits
                .Where(x => x.Date.Date >= dates[0] && x.Date.Date <= dates[1])
                .ToList();
            var newStatistic = new StatisticCalculationHelper(actualCommits).Calculte();
            return reportBuilder.BuildReport(newStatistic, dates[0], dates[1]);
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
