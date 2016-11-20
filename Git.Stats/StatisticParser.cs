using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;

namespace Git.Stats
{
    public sealed class StatisticParser
    {
        private const string CommitTemplate = "commit", MergeTempalte = "Merge";

        private readonly List<Commit> commits = new List<Commit>();
        private readonly IList<string> results;
        private readonly RegexHelper regexHelper;

        public StatisticParser(IList<string> results)
        {
            this.results = results;
            regexHelper = new RegexHelper();
        }

        public Statistic Parse()
        {
            ParseCommits();
            return CalculateStatistics();
        }

        private Statistic CalculateStatistics()
        {
            var authors = commits.Select(x => x.Author).Distinct();

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

        private void ParseCommits()
        {
            var step = 5;
            for (int i = 0; i < results.Count; i += step)
            {
                if (results[i].StartsWith(CommitTemplate))
                {
                    AddCommit(i);
                    step = 5;
                }
                else
                {
                    step = 1;
                }
            }
        }

        private void AddCommit(int i)
        {
            if(results[i + 1].StartsWith(MergeTempalte)) 
                return;

            var commitname = regexHelper.GetName(results[i]);
            var author = regexHelper.GetAuthor(results[i + 1]);

            if (author == null)
            {
                throw new EventSourceException();
            }

            var insertions = regexHelper.GetInsertions(results[i + 4]);
            var deletions = regexHelper.GetDeletions(results[i + 4]);
            var commit = new Commit(commitname, author, insertions, deletions);
            commits.Add(commit);
        }
    }
}
