using BizDays.Abstractions.Domain;

namespace BizDays.Implementation.Domain
{
    public class AdvancedBusinessDayCounter : BusinessDayCounter
    {
        /// <summary>
        /// Provides advanced functionality for calculating business days between two dates
        /// using dynamic holiday rules.
        /// 
        /// This class builds on the core functionality provided by <see cref="BusinessDayCounter"/>
        /// by introducing support for custom holiday rules through the <see cref="IHolidayRule"/> interface.
        /// It is designed to handle complex scenarios, such as regional holidays, recurring holidays,
        /// and weekend-adjusted holidays.
        /// 
        /// Keeping this class separate ensures that the core logic in <see cref="BusinessDayCounter"/> 
        /// remains reusable and lightweight, while advanced features are modular and easily extendable.
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
