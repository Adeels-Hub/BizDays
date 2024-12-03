using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizDays.Abstractions.Domain;

namespace BizDays.Implementation.Domain
{
    public class RecurringHoliday : IHolidayRule
    {
        private readonly int _month;
        private readonly DayOfWeek _dayOfWeek;
        private readonly int _occurrence;

        public RecurringHoliday(int month, DayOfWeek dayOfWeek, int occurrence)
        {
            _month = month;
            _dayOfWeek = dayOfWeek;
            _occurrence = occurrence;
        }

        public bool IsHoliday(DateTime date)
        {
            if (date.Month != _month)
                return false;

            // Get the first day of the month
            DateTime firstDayOfMonth = new DateTime(date.Year, _month, 1, 0, 0, 0, DateTimeKind.Utc);

            // Find the first occurrence of the day of the week
            int daysToAdd = ((_dayOfWeek - firstDayOfMonth.DayOfWeek + 7) % 7);
            DateTime firstOccurrence = firstDayOfMonth.AddDays(daysToAdd);

            // Calculate the nth occurrence
            DateTime nthOccurrence = firstOccurrence.AddDays(7 * (_occurrence - 1));

            return date.Date == nthOccurrence.Date;
        }
    }
}
