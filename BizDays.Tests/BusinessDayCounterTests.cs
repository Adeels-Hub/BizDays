using BizDays.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace BizDays.Tests
{

    public class BusinessDayCounterTests
    {
        private static readonly List<IHolidayRule> NswPublicHolidays = new List<IHolidayRule>
    {
        new WeekendAdjustedHoliday(1, 1),  // New Year's Day
        new FixedDateHoliday(1, 26),       // Australia Day
        new FixedDateHoliday(4, 7),        // Good Friday 2023
        new FixedDateHoliday(4, 8),        // Easter Saturday 2023
        new FixedDateHoliday(4, 9),        // Easter Sunday 2023
        new FixedDateHoliday(4, 10),       // Easter Monday 2023
        new FixedDateHoliday(4, 25),       // Anzac Day
        new DayOccurrenceHoliday(6, DayOfWeek.Monday, 2),  // King's Birthday
        new DayOccurrenceHoliday(10, DayOfWeek.Monday, 1), // Labour Day
        new FixedDateHoliday(12, 25),      // Christmas Day
        new FixedDateHoliday(12, 26),      // Boxing Day

        // Add 2024 holidays
        new WeekendAdjustedHoliday(1, 1),  // New Year's Day 2024
        new FixedDateHoliday(1, 26),       // Australia Day 2024
        new FixedDateHoliday(3, 29),       // Good Friday 2024
        new FixedDateHoliday(3, 30),       // Easter Saturday 2024
        new FixedDateHoliday(3, 31),       // Easter Sunday 2024
        new FixedDateHoliday(4, 1),        // Easter Monday 2024
        new FixedDateHoliday(4, 25),       // Anzac Day 2024
        new DayOccurrenceHoliday(6, DayOfWeek.Monday, 2),  // King's Birthday 2024
        new DayOccurrenceHoliday(10, DayOfWeek.Monday, 1), // Labour Day 2024
        new FixedDateHoliday(12, 25),      // Christmas Day 2024
        new FixedDateHoliday(12, 26)       // Boxing Day 2024
    };

        [Theory]
        [InlineData("2023-01-01", "2023-01-10", 6)]  // Range with New Year's Day (adjusted for weekend)
        [InlineData("2023-04-01", "2023-04-30", 18)] // April 2023, Easter holidays
        [InlineData("2023-12-20", "2024-01-10", 13)] // Cross-year range
        [InlineData("2024-03-25", "2024-04-05", 9)]  // Pre-Easter and Easter period
        public void BusinessDaysBetweenTwoDates_WithNswHolidays_ReturnsCorrectCount(
    string start, string end, int expected)
        {
            // Arrange
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            // Act
            int businessDays = AdvancedBusinessDayCounter.BusinessDaysBetweenTwoDates(
                startDate, endDate, NswPublicHolidays);

            // Assert
            Assert.Equal(expected, businessDays);
        }

        [Theory]
        [InlineData("2013-10-07", "2013-10-09", 1)] // 7th Oct to 9th Oct 2013: 1 weekday
        [InlineData("2013-10-05", "2013-10-14", 5)] // 5th Oct to 14th Oct 2013: 5 weekdays
        [InlineData("2013-10-07", "2014-01-01", 61)] // 7th Oct to 1st Jan 2014: 61 weekdays
        [InlineData("2013-10-07", "2013-10-05", 0)] // 7th Oct to 5th Oct 2013: 0 weekdays
        public void WeekdaysBetweenTwoDates_ReturnsCorrectCount(string start, string end, int expected)
        {
            // Arrange
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            // Act
            int result = BusinessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
