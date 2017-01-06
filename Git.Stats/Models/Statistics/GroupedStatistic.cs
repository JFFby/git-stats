using System;

namespace Git.Stats.Models.Statistics
{
    public sealed class GroupedStatistic
    {
        public GroupedStatistic(Statistic statistc, DateTime from, DateTime to)
        {
            Statistc = statistc;
            From = from;
            To = to;
        }

        public Statistic Statistc { get; }

        public DateTime From { get; }

        public DateTime To { get; }
    }
}
