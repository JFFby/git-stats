using Command.Infrastructure;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure
{
    public sealed class GitStatsController : ICommandController
    {
        private readonly GitStatisticService cdService;

        public GitStatsController()
        {
            this.cdService = new GitStatisticService();
        }

        public string Cd(_Command command)
        {
            return cdService.Execute(command);
        }
    }
}
