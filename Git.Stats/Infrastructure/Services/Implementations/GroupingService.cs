using System;
using System.Collections.Generic;
using System.Linq;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    public sealed class GroupingService : IGroupingService
    {
        private readonly IWeekDatesService weekDatesService;

        public GroupingService(IWeekDatesService weekDatesService)
        {
            this.weekDatesService = weekDatesService;
        }

        public IList<GroupedStatistic> Group(IList<Commit> commits, DateTime from, DateTime to)
        {
            var weekStart = weekDatesService.WeekStartDate(from);
            var result = new List<GroupedStatistic>();
            DateTime weekEnd;
            do
            {
                weekEnd = weekStart.AddDays(7);
                var suitableCommits = commits
                    .Where(x => x.Date.Date >= weekStart.Date && x.Date.Date < weekEnd.Date)
                    .ToList();

                if (suitableCommits.Any())
                {
                    var statistic = new StatisticCalculationHelper(suitableCommits).Calculte();
                    var groupedStatistic = new GroupedStatistic(statistic, weekStart, weekEnd);
                    result.Add(groupedStatistic);
                }
                
                weekStart = weekEnd;
            } while (weekEnd.Date < to.Date);

            return result;
        }

        public string GroupBy => "WEEKLY";
    }
}
