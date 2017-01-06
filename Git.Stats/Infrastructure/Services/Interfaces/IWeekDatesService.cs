using System;

namespace Git.Stats.Infrastructure.Services.Interfaces
{
    public interface IWeekDatesService : IService
    {
        DateTime WeekStartDate(DateTime date);
    }
}
