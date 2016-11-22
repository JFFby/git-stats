using System.Collections.Generic;
using System.Linq;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;

namespace Git.Stats
{
    internal sealed class StatisticCalculationHelper
    {
        private readonly IList<Commit> commits;

        public StatisticCalculationHelper(IList<Commit> commits)
        {
            this.commits = commits;
        }

        public Statistic Calculte()
        {
            return new Statistic
            {
                TotalDeletions = commits.Sum(x => x.Deletions),
                TotalInserions = commits.Sum(x => x.Insertions),
                Commits = commits,
                AuhorStatistic = GetAuthorsStatistic()
            };
        }

        private IList<AuthorStatistic> GetAuthorsStatistic()
        {
            var commitsGruppedByAuthors = commits.GroupBy(x => x.Author);
            return commitsGruppedByAuthors
                .Select(x => new AuthorStatistic(
                    x.Key, x.Count(), x.Sum(s => s.Insertions), x.Sum(s => s.Deletions)))
                .ToList();
        }
    }
}
