using BizDays.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace BizDays.Tests
{
    public class AdvancedBusinessDayCounterTests
    {
        [Theory]
        [InlineData("2013-10-07", "2013-10-09", 1)]  // 7th Oct to 9th Oct 2013: 1 business day
        [InlineData("2013-12-24", "2013-12-27", 0)]  // 24th Dec to 27th Dec 2013: 0 business days (holidays)
        [InlineData("2013-10-07", "2014-01-01", 59)] // 7th Oct to 1st Jan 2014: 59 business days
        [InlineData("2013-12-24", "2013-12-15", 0)]  // 24th Dec to 27th Dec 2013: 0 business days (holidays)
        public void BusinessDaysBetweenTwoDates_ReturnsCorrectCount_WithPublicHolidays(
            string start, string end, int expected)
        {
            // Arrange
            DateTime startDate = DateTime.Parse(start);
            DateTime endDate = DateTime.Parse(end);

            var publicHolidays = new List<IHolidayRule>
            {
                new FixedDateHoliday(12, 25), // 25th December (Christmas)
                new FixedDateHoliday(12, 26), // 26th December (Boxing Day)
                new FixedDateHoliday(1, 1)   // 1st January (New Year)
            };

            // Act
            int result = AdvancedBusinessDayCounter.BusinessDaysBetweenTwoDates(
                startDate, endDate, publicHolidays);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
