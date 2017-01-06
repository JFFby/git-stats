using System.Collections.Generic;
using Git.Stats.Infrastructure.Services.Arguments;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IGroupingFactory : IService
    {
        IList<GroupedStatistic> GroupBy(GroupingArgs args);
    }
}
