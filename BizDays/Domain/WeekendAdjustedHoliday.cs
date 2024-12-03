using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizDays.Abstractions.Domain;

namespace BizDays.Implementation.Domain
{
    public class WeekendAdjustedHoliday : IHolidayRule
    {
        private readonly int _month;
        private readonly int _day;

        public WeekendAdjustedHoliday(int month, int day)
        {
            _month = month;
            _day = day;
        }

        public bool IsHoliday(DateTime date)
        {
            // If the holiday falls on a weekend, it shifts to the next Monday
            if (date.Month == _month && date.Day == _day &&
                (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
            {
                return true;
            }
            return false;
        }

    }
}
