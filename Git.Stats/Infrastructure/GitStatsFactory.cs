using Command.Infrastructure;

namespace Git.Stats.Infrastructure
{
    public sealed class GitStatsFactory
    {
        public static GetStatsCommandHandler ResolveHandler()
        {
            return new GetStatsCommandHandler(new CommandHandler(new GitStatsController()));
        }
    }
}