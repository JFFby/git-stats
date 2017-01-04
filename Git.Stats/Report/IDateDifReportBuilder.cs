using System;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Report
{
    public interface IDateDifReportBuilder : IService
    {
        string BuildReport(Statistic statistic, DateTime from, DateTime to);
    }
}
