using System;
using System.Collections.Generic;
using System.Linq;

namespace BizDays.Domain
{
    public class AdvancedBusinessDayCounter : BusinessDayCounter
    {
        /// <summary>
        /// Calculates the number of business days between two dates using custom holiday rules.
        /// </summary>
        /// <param name="firstDate">The start date (exclusive).</param>
        /// <param name="secondDate">The end date (exclusive).</param>
        /// <param name="holidayRules">A list of holiday rules to determine holidays dynamically.</param>
        /// <returns>The count of business days between the two dates.</returns>
        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IHolidayRule> holidayRules)
        {
            if (secondDate <= firstDate)
                return 0;

            // Correct the range: Exclude both `firstDate` and `secondDate`
            int businessDays = Enumerable
                .Range(1, (secondDate - firstDate).Days - 1) // Subtract 1 to exclude the end date
                .Select(offset => firstDate.AddDays(offset))
                .Count(date => IsWeekday(date) && !holidayRules.Any(rule => rule.IsHoliday(date)));

            return businessDays;
        }

    }
}
