using Command.Infrastructure;
using Git.Stats.Infrastructure.Services.Interfaces;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure
{
    public sealed class GitStatsController : ICommandController
    {
        private readonly IGitStatisticService statisticService;
        private readonly IBetweenService betweenService;
        private readonly IMergeService mergeService;
        private readonly IDateDifService dateDifService;

        public GitStatsController(
            IGitStatisticService statisticService, 
            IBetweenService betweenService, 
            IMergeService mergeService, 
            IDateDifService dateDifService)
        {
            this.statisticService = statisticService;
            this.betweenService = betweenService;
            this.mergeService = mergeService;
            this.dateDifService = dateDifService;
        }

        public string Cd(_Command command)
        {
            return statisticService.Execute(command);
        }

        public string Between(_Command command)
        {
            return betweenService.GetStatisticBetween(command);
        }

        public string Merge(_Command command)
        {
            return mergeService.Merge(command);
        }

        public string DateDif(_Command command)
        {
            return dateDifService.GetDateDifStatistic(command);
        }
    }
}
