using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
            return new StatisticCalculationHelper(commits).Calculte();
        }

        private void ParseCommits()
        {
            var step = 5;
            for (int i = 0; i < results.Count; i += step)
            {
                if (results[i].StartsWith(CommitTemplate))
                {
                    step = AddCommit(i); ;
                }
                else
                {
                    step = 1;
                }
            }
        }

        private int AddCommit(int i)
        {
            if(results[i + 1].StartsWith(MergeTempalte)) 
                return 1;

            var commitname = regexHelper.GetName(results[i]);
            var author = regexHelper.GetAuthor(results[i + 1]);

            if (author == null)
            {
                throw new EventSourceException();
            }

            var date = regexHelper.GetDate(results[i + 2]);
            var paddingForLinesStatistic = GetLineStatisticPadding(i);

            var insertions = regexHelper.GetInsertions(results[i + paddingForLinesStatistic]);
            var deletions = regexHelper.GetDeletions(results[i + paddingForLinesStatistic]);
            var commit = new Commit(commitname, author, insertions, deletions, date);
            commits.Add(commit);
            return paddingForLinesStatistic + 1;
        }

        private int GetLineStatisticPadding(int i)
        {
            var defaultPadding = 3;
            return regexHelper.IsContainsLineStatistic(results[i + defaultPadding])
                ? defaultPadding
                : defaultPadding + 1;
        }
    }
}
