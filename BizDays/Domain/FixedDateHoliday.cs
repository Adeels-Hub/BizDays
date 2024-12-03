using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizDays.Abstractions.Domain;

namespace BizDays.Implementation.Domain
{
    public class FixedDateHoliday : IHolidayRule
    {
        private readonly int _month;
        private readonly int _day;

        public FixedDateHoliday(int month, int day)
        {
            _month = month;
            _day = day;
        }

        public bool IsHoliday(DateTime date)
        {
            return date.Month == _month && date.Day == _day;
        }
    }
}
