using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    public sealed class StatisticStorage : IStatisticStorage
    {
        private Statistic _statistic;

        public void Save(Statistic statistic)
        {
            this._statistic = statistic;
        }

        public Statistic Get()
        {
            return _statistic;
        }
    }
}