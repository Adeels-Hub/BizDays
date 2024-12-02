namespace BizDays.Domain
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate)
                return 0;

            // Start the count from the day after `firstDate` and end before `secondDate`
            int totalDays = (secondDate - firstDate).Days - 1;
            int weekdays = 0;

            for (int i = 1; i <= totalDays; i++)
            {
                DateTime currentDate = firstDate.AddDays(i);
                if (IsWeekday(currentDate))
                    weekdays++;
            }

            return weekdays;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            if (secondDate <= firstDate)
                return 0;

            // Normalize public holidays
            var holidaySet = new HashSet<DateTime>(publicHolidays.Select(d => d.Date));

            // Start the count from the day after `firstDate` and end before `secondDate`
            int totalDays = (secondDate - firstDate).Days - 1;
            int businessDays = 0;

            for (int i = 1; i <= totalDays; i++)
            {
                DateTime currentDate = firstDate.AddDays(i);
                if (IsWeekday(currentDate) && !holidaySet.Contains(currentDate))
                    businessDays++;
            }

            return businessDays;
        }

        protected static bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday;
        }
    }
}
