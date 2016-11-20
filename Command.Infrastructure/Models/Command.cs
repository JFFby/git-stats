namespace Command.Infrastructure.Models
{
    public sealed class Command
    {
        public Command(string executedCommand, string args)
        {
            ExecutedCommand = executedCommand;
            Args = args;
        }

        public string ExecutedCommand { get; }

        public string Args { get; }
    }
}