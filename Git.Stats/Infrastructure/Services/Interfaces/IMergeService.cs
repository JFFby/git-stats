using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IMergeService : IService
    {
        string Merge(_Command command);
    }
}
