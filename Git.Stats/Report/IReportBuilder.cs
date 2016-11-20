using Git.Stats.Models.Statistics;

namespace Git.Stats.Report
{
    public interface IReportBuilder
    {
        string BuildReport(Statistic statistic);
    }
}
