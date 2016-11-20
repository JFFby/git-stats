using Command.Infrastructure;

namespace Git.Stats.Infrastructure
{
    public sealed class GetStatsCommandHandler
    {
        private readonly CommandHandler CommandHandler;

        public GetStatsCommandHandler(CommandHandler commandHandler)
        {
            CommandHandler = commandHandler;
        }

        public string Execute(string command)
        {
            return CommandHandler.Execute(command);
        }
    }
}
