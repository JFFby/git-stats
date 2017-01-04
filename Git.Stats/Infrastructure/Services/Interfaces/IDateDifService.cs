namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IDateDifService : IService
    {
        string GetDateDifStatistic(Command.Infrastructure.Models.Command command);
    }
}
