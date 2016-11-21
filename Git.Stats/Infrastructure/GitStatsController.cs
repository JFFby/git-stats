using Command.Infrastructure;
using Git.Stats.Infrastructure.Services.Interfaces;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure
{
    public sealed class GitStatsController : ICommandController
    {
        private readonly IGitStatisticService statisticService;

        public GitStatsController(IGitStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        public string Cd(_Command command)
        {
            return statisticService.Execute(command);
        }
    }
}
