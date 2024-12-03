using BizDays.Abstractions.Domain;

namespace BizDays.Abstractions.Services
{
    public interface IBusinessDayService
    {
        int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);
        int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays);
        int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IHolidayRule> holidayRules);
    }
}
