using System;
using System.Globalization;
using Git.Stats.Infrastructure.Services.Interfaces;

namespace Git.Stats.Infrastructure.Services.Implementations
{
    public sealed class WeekDatesService : IWeekDatesService
    {
        private const int LastYearWeekNumber = 51;
        private const int FirstYearWeekNumber = 1;
        private readonly CultureInfo culture =  new CultureInfo("en-US");
        
        public DateTime WeekStartDate(DateTime date)
        {
            var weekOfYear = GetWeekNumber(date);
            var januaryFirst = new DateTime(date.Year, 1, 1);
            var daysOffset = (int)culture.DateTimeFormat.FirstDayOfWeek - (int)januaryFirst.DayOfWeek;
            var firstWeekDay = januaryFirst.AddDays(daysOffset);
            var firstWeek = culture.Calendar.GetWeekOfYear(
                januaryFirst, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= FirstYearWeekNumber || firstWeek >= LastYearWeekNumber)
            {
                weekOfYear -= 1;
            }

            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        private int GetWeekNumber(DateTime date)
        {
            return culture.Calendar.GetWeekOfYear(
                    date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }
    }
}
