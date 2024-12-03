namespace BizDays.Abstractions.Domain
{
    public interface IHolidayRule
    {
        bool IsHoliday(DateTime date);
    }
}
