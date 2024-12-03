using BizDays.Abstractions.Domain;
using BizDays.Abstractions.Services;
using BizDays.Implementation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizDays.Implementation.Services
{
    public class BusinessDayService : IBusinessDayService
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            return BusinessDayCounter.WeekdaysBetweenTwoDates(firstDate, secondDate);
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var counter = new BusinessDayCounter();
            return counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IHolidayRule> holidayRules)
        {
            return AdvancedBusinessDayCounter.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidayRules);
        }
    }
}
