using System;
using System.Collections.Generic;
using System.Linq;

namespace AC_Cat.Services
{
    public interface IDateScheduleService
    {
        IEnumerable<DateTimeOffset> GetDates(DateTimeOffset timeOffset, int weeksHence, DayOfWeek dayOfWeek = DayOfWeek.Monday);
    }

    public class DateScheduleService : IDateScheduleService
    {
        public IEnumerable<DateTimeOffset> GetDates(DateTimeOffset timeOffset, int weeksHence, DayOfWeek dayOfWeek)
        {
            var firstPaymentDate = FindFirstDate(timeOffset, dayOfWeek);
            List<DateTimeOffset> dateTimes = new List<DateTimeOffset>();

            return Enumerable.Range(0, weeksHence).Select(i => firstPaymentDate.AddDays(7 * i));
        }

        private DateTimeOffset FindFirstDate(DateTimeOffset timeOffset, DayOfWeek dayOfWeek)
        {
            int daysToAdd = ((int)dayOfWeek - (int)timeOffset.DayOfWeek + 7) % 7;
            return timeOffset.AddDays(daysToAdd);
        }
    }
}