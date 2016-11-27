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

        public GitStatsController(
            IGitStatisticService statisticService, 
            IBetweenService betweenService, 
            IMergeService mergeService)
        {
            this.statisticService = statisticService;
            this.betweenService = betweenService;
            this.mergeService = mergeService;
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
    }
}
