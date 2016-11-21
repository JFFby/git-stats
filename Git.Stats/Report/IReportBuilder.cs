using Git.Stats.Infrastructure.Services.Implementations;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Report
{
    public interface IReportBuilder : IService
    {
        string BuildReport(Statistic statistic);
    }
}
