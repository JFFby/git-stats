using System.Collections.Generic;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Infrastructure.Services.Models;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;
using Git.Stats.Report;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    public sealed class BetweenService : IBetweenService
    {
        private readonly IReportBuilder reportBuilder;
        private readonly IStatisticStorage statisticStorage;

        public BetweenService(IReportBuilder reportBuilder, IStatisticStorage statisticStorage)
        {
            this.reportBuilder = reportBuilder;
            this.statisticStorage = statisticStorage;
        }

        public string GetStatisticBetween(Command.Infrastructure.Models.Command command)
        {
            var commits = command.ExecutedCommand.Split(' ');
            var statistic = statisticStorage.Get();
            if (Check(commits, statistic) != null)
                return Check(commits, statistic);

            var searchResult = FindCommits(commits, statistic);
            if (searchResult.ErrorMessage != null)
                return searchResult.ErrorMessage;


            return string.Empty;
        }

        private SearchResult FindCommits(string[] commits, Statistic statistic)
        {
            var suitableCommits = new List<Commit>();
            var boundCommitsCount = 0;
            for (int i = statistic.Commits.Count - 1; i <= 0; i--)
            {
                var commit = statistic.Commits[i];
                foreach (var cn in commits)
                {
                    if (commit.Name.StartsWith(cn))
                    {
                        ++boundCommitsCount;
                        if (boundCommitsCount == 2)
                        {
                            suitableCommits.Add(commit);
                        }
                    }

                    if (boundCommitsCount == 1)
                    {
                        suitableCommits.Add(commit);
                    }
                }
            }

            if (CheckBounds(boundCommitsCount) != null)
                return SearchResult.Error(CheckBounds(boundCommitsCount));

            return SearchResult.Success(suitableCommits);
        }

        private string CheckBounds(int boundsCount)
        {
            if (boundsCount <= 1)
            {
                return "some commit wasn't found";
            }

            if (boundsCount > 2)
            {
                return $"{boundsCount} commits were found";
            }

            return null;
        }

        private string Check(string[] arguments, Statistic statistic)
        {
            if (arguments.Length != 2)
            {
                return "wrong arguments";
            }

            return statistic == null ? "choose git repo first of all" : null;
        }
    }
}
