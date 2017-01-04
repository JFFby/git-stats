using System;
using System.Text;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Report
{
    public class DateDifReportBuilder : IDateDifReportBuilder
    {
        private readonly IReportBuilder reportBuilder;

        public DateDifReportBuilder(IReportBuilder reportBuilder)
        {
            this.reportBuilder = reportBuilder;
        }

        public string BuildReport(Statistic statistic, DateTime @from, DateTime to)
        {
            var result = new StringBuilder($"From: {FomratDate(from)} To: {FomratDate(to)}{Environment.NewLine}");
            result.Append(reportBuilder.BuildReport(statistic));
            return result.ToString();
        }

        private string FomratDate(DateTime date)
        {
            return date.ToString("d");
        }
    }
}
