using Git.Stats.Models.Statistics;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IStatisticStorage : IService
    {
        void Save(Statistic statistic);

        Statistic Get();
    }
}
