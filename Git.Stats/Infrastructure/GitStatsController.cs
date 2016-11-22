using Command.Infrastructure;
using Git.Stats.Infrastructure.Services.Interfaces;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure
{
    public sealed class GitStatsController : ICommandController
    {
        private readonly IGitStatisticService statisticService;
        private readonly IBetweenService betweenService;

        public GitStatsController(IGitStatisticService statisticService, IBetweenService betweenService)
        {
            this.statisticService = statisticService;
            this.betweenService = betweenService;
        }

        public string Cd(_Command command)
        {
            return statisticService.Execute(command);
        }

        public string Between(_Command command)
        {
            return betweenService.GetStatisticBetween(command);
        }
    }
}
