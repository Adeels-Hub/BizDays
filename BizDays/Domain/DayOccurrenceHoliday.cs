namespace BizDays.Domain
{
    public class DayOccurrenceHoliday : IHolidayRule
    {
        private readonly int _month;
        private readonly DayOfWeek _dayOfWeek;
        private readonly int _occurrence;

        public DayOccurrenceHoliday(int month, DayOfWeek dayOfWeek, int occurrence)
        {
            _month = month;
            _dayOfWeek = dayOfWeek;
            _occurrence = occurrence;
        }

        public bool IsHoliday(DateTime date)
        {
            if (date.Month != _month)
                return false;

            DateTime firstDayOfMonth = new DateTime(date.Year, _month, 1, 0, 0, 0, DateTimeKind.Utc);
            int daysOffset = (_dayOfWeek - firstDayOfMonth.DayOfWeek + 7) % 7;
            DateTime firstOccurrence = firstDayOfMonth.AddDays(daysOffset);
            DateTime nthOccurrence = firstOccurrence.AddDays((_occurrence - 1) * 7);

            return date.Date == nthOccurrence.Date;
        }
    }
}
