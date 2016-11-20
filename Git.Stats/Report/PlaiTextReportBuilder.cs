using System;
using System.Text;
using Git.Stats.Models.Statistics;

namespace Git.Stats.Report
{
    internal sealed class PlaiTextReportBuilder : IReportBuilder
    {
        public string BuildReport(Statistic statistic)
        {
            var report = new StringBuilder();
            AppendCommonStatistic(report, statistic);
            AppendAuthorsStatistic(report, statistic);

            return report.ToString();
        }

        private string nl => Environment.NewLine;

        private string tab => "\t";

        private void AppendCommonStatistic(StringBuilder report, Statistic statistic)
        {
            var statsLine = 
                GetStatsLine(statistic.Commits.Count, statistic.TotalInserions, statistic.TotalDeletions);
            report.Append($"LoC: {statistic.TotalInserions - statistic.TotalDeletions};{nl}" +
                         $"{statsLine}{nl}");
        }

        private void AppendAuthorsStatistic(StringBuilder report, Statistic statistic)
        {
            report.Append(nl);
            report.Append(nl);
            foreach (var authorStatistic in statistic.AuhorStatistic)
            {
                var statsLine = 
                    GetStatsLine(authorStatistic.Commits, authorStatistic.TotalInsertions, authorStatistic.TotalDeletions);
                report.Append($"Author: {authorStatistic.Author.Name}:{nl}" +
                              $"{tab}{statsLine}{nl}");
                report.Append(nl);
            }
        }

        private string GetStatsLine(int commits, int insertions, int deletions)
        {
            return $"commits: {commits}; {insertions} insertions (+), {deletions} deletions (-)";
        }
    }
}
