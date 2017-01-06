using System;
using System.Collections.Generic;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IGroupingService
    {
        IList<GroupedStatistic> Group(IList<Commit> commits, DateTime from, DateTime to );

        string GroupBy { get; }
    }
}
