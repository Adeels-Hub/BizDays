using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizDays.Domain
{
    public class AdvancedBusinessDayCounter : BusinessDayCounter
    {
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IHolidayRule> holidayRules)
        {
            if (secondDate <= firstDate)
                return 0;

            int totalDays = (secondDate - firstDate).Days - 1;
            int businessDays = 0;

            for (int i = 1; i <= totalDays; i++)
            {
                DateTime currentDate = firstDate.AddDays(i);
                if (IsWeekday(currentDate) && !holidayRules.Any(rule => rule.IsHoliday(currentDate)))
                    businessDays++;
            }

            return businessDays;
        }
    }
}
