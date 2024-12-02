namespace BizDays.Domain
{
    public class BusinessDayCounter
    {
        /// <summary>
        /// Calculates the number of weekdays between two dates, excluding the start and end dates.
        /// </summary>
        /// <param name="firstDate">The start date (exclusive).</param>
        /// <param name="secondDate">The end date (exclusive).</param>
        /// <returns>The count of weekdays between the two dates.</returns>
        public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate)
                return 0;

            // Start counting from the day after `firstDate` and end before `secondDate`
            int weekdays = Enumerable
                .Range(1, (secondDate - firstDate).Days - 1) // Generate day offsets
                .Select(offset => firstDate.AddDays(offset)) // Calculate the dates
                .Count(date => IsWeekday(date)); // Count weekdays

            return weekdays;
        }

        /// <summary>
        /// Calculates the number of business days between two dates, excluding holidays.
        /// </summary>
        /// <param name="firstDate">The start date (exclusive).</param>
        /// <param name="secondDate">The end date (exclusive).</param>
        /// <param name="publicHolidays">A list of public holidays.</param>
        /// <returns>The count of business days between the two dates.</returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            if (secondDate <= firstDate)
                return 0;

            // Normalize public holidays to ensure only dates (no time) are considered
            var holidaySet = new HashSet<DateTime>(publicHolidays.Select(d => d.Date));

            // Start counting from the day after `firstDate` and end before `secondDate`
            int businessDays = Enumerable
                .Range(1, (secondDate - firstDate).Days - 1) // Generate day offsets
                .Select(offset => firstDate.AddDays(offset)) // Calculate the dates
                .Count(date => IsWeekday(date) && !holidaySet.Contains(date)); // Count business days

            return businessDays;
        }

        /// <summary>
        /// Determines if a given date is a weekday (Monday through Friday).
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>True if the date is a weekday; otherwise, false.</returns>
        protected static bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday;
        }
    }
}
