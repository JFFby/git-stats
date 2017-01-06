using System.Collections.Generic;
using System.Linq;
using Git.Stats.Infrastructure.Services.Arguments;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    public sealed class GroupingFactory : IGroupingFactory
    {
        private readonly IGroupingService groupingService;

        public GroupingFactory(IGroupingService groupingService)
        {
            this.groupingService = groupingService;
        }

        public IList<GroupedStatistic> GroupBy(GroupingArgs args)
        {
            if (groupingService.GroupBy == args.Method)
            {
                return groupingService.Group(args.Commits, args.From, args.To);
            }

            var statistic = new StatisticCalculationHelper(args.Commits).Calculte();
            return new List<GroupedStatistic> {new GroupedStatistic(statistic, args.From, args.To)};
        }
    }
}
