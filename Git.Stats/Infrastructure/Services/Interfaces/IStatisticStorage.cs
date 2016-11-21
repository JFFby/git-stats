using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IStatisticStorage
    {
        void Save(Statistic statistic);

        Statistic Get();
    }
}
