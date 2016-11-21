using Git.Stats.Infrastructure.Services.Implementations;
using _Command = Command.Infrastructure.Models.Command;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IGitStatisticService : IService
    {
        string Execute(_Command command);
    }
}
