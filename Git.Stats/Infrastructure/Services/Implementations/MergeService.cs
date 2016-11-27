using System;
using System.Linq;
using Git.Stats.Infrastructure.Services.Interfaces;
using Git.Stats.Infrastructure.Services.Models;
using Git.Stats.Models;
using Git.Stats.Models.Statistics;
using Git.Stats.Report;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    public sealed class MergeService : IMergeService
    {
        private readonly IReportBuilder ReportBuilder;
        private readonly IStatisticStorage storage;

        public MergeService(IReportBuilder reportBuilder,
            IStatisticStorage storage)
        {
            ReportBuilder = reportBuilder;
            this.storage = storage;
        }

        public string Merge(Command.Infrastructure.Models.Command command)
        {
            var authors = command.Args.Split(' ');
            var statistic = storage.Get();
            if (CheckAuthors(authors, statistic) != null)
                return CheckAuthors(authors, statistic);

            var mergeModel = ParseMergeModel(authors, statistic);
            if (mergeModel.ErrorMessage != null)
                return mergeModel.ErrorMessage;

            var commits = statistic.Commits
                .Select(commit =>
                    mergeModel.Duplicates.Contains(commit.Author.Id)
                        ? new Commit(commit.Name, mergeModel.MainAuthor, commit.Insertions, commit.Deletions)
                        : commit).ToList();

            var newStatistic = new StatisticCalculationHelper(commits).Calculte();
            storage.Save(newStatistic);

            return ReportBuilder.BuildReport(newStatistic);
        }

        private MergeAuthorsModel ParseMergeModel(string[] authors, Statistic statistic)
        {
            try
            {
                var mainAuthorId = int.Parse(authors[0]);
                var duplicates = authors.Where(x => x != authors[0])
                    .Select(int.Parse)
                    .ToList();

                var mainAuthor = statistic.AuhorStatistic.FirstOrDefault(x => x.Author.Id == mainAuthorId);

                return MergeAuthorsModel.Success(mainAuthor.Author, duplicates);
            }
            catch (Exception)
            {
                return MergeAuthorsModel.Error("Can't parse authors");
            }
        }

        private string CheckAuthors(string[] authors, Statistic statistic)
        {
            if (authors.Length < 2)
            {
                return "Specify two or more authors";
            }

            return statistic != null ? null : BetweenService.RepoNotSelected;
        }
    }
}
