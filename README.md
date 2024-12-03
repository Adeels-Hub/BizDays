
# BizDays Solution

The `BizDays` solution is a modular and scalable library designed for calculating business days and weekdays between two dates. It provides flexibility for handling complex holiday rules through abstractions and implementations, making it adaptable for diverse use cases, such as regional holiday calculations.

---

## Table of Contents
- [Overview](#overview)
- [Projects](#projects)
  - [BizDays.Abstractions](#bizdaysabstractions)
  - [BizDays.Implementation](#bizdaysimplementation)
  - [BizDays.Tests](#bizdaystests)
- [Features](#features)
- [Design Decisions](#design-decisions)
- [How to Use](#how-to-use)
- [Running Tests](#running-tests)
- [Future Enhancements](#future-enhancements)

---

## Overview

The `BizDays` solution is built with the following key principles:
1. **Separation of Concerns:** Interfaces, implementations, and tests are kept in separate projects for modularity and maintainability.
2. **Scalability:** Advanced business day calculation rules, such as weekend adjustments and regional holidays, can be easily extended.
3. **Flexibility:** Support for dynamic holiday rules using the `IHolidayRule` interface.

---

## Projects

### 1. **BizDays.Abstractions**
Contains abstractions and interfaces that define the contract for holiday rules:
- **Namespace:** `BizDays.Abstractions.Domain`
- **Key Interface:**
  - `IHolidayRule`: Defines a method `bool IsHoliday(DateTime date)` for determining if a date is a holiday.
  - `IBusinessDayService`: Exposes the public API for calculating weekdays and business days.

### 2. **BizDays.Implementation**
Contains concrete implementations of business day counters and holiday rules:
- **Namespace:** `BizDays.Implementation.Domain`
- **Key Classes:**
  - `BusinessDayCounter`: Provides core functionality for calculating weekdays and business days.
  - `AdvancedBusinessDayCounter`: Extends `BusinessDayCounter` to support custom holiday rules via `IHolidayRule`.
  - `FixedDateHoliday`: Implements `IHolidayRule` for holidays on fixed dates.
  - `DayOccurrenceHoliday`: Implements `IHolidayRule` for holidays based on occurrences (e.g., second Monday in June).
  - `WeekendAdjustedHoliday`: Implements `IHolidayRule` for holidays that adjust for weekends.
- **Namespace:** `BizDays.Implementation.Services`
  - `BusinessDayService`: Implements `IBusinessDayService` to provide a unified entry point for consumers.

### 3. **BizDays.Tests**
Contains unit tests for validating the functionality of the `BusinessDayService`:
- **Key Test Class:**
  - `BusinessDayServiceTests`: Validates all scenarios for weekdays and business days using dynamic holiday rules.

---

## Features

- **Weekday Calculation:**
  - Calculate the number of weekdays between two dates.
- **Business Day Calculation:**
  - Calculate business days while excluding holidays.
- **Advanced Holiday Rules:**
  - Handle fixed-date holidays.
  - Support holidays based on day occurrences (e.g., first Monday in October).
  - Adjust holidays falling on weekends to the next weekday.
- **Public API via Service:**
  - Consumers interact with the `IBusinessDayService` interface for streamlined functionality.

---

## Design Decisions

### Why `BusinessDayCounter` and `AdvancedBusinessDayCounter` Are Public

We have intentionally kept the `BusinessDayCounter` and `AdvancedBusinessDayCounter` classes **public** to allow advanced users to subclass and extend these core functionalities directly if needed. This design decision ensures the library remains flexible for power users with custom requirements.

### Best Practice Recommendation

As a best practice, these classes can be made **internal** to restrict direct access from outside the library. This approach would ensure:
1. **Encapsulation:** Only the `BusinessDayService` and `IBusinessDayService` serve as the public API.
2. **Ease of Maintenance:** Changes to internal classes do not risk breaking external consumers.
3. **Clear API:** Provides a focused and simplified interface for consumers.

By keeping these classes public, we balance flexibility with usability, empowering users while following clean code principles.

---

## How to Use

### Step 1: Add References
Include references to both `BizDays.Abstractions` and `BizDays.Implementation` in your project.

### Step 2: Use `BusinessDayService`
Inject or create an instance of `BusinessDayService` to perform calculations.

```csharp
// Create service instance
IBusinessDayService service = new BusinessDayService();

// Calculate weekdays
int weekdays = service.WeekdaysBetweenTwoDates(startDate, endDate);

// Calculate business days with public holidays
var publicHolidays = new List<DateTime> { new DateTime(2023, 12, 25), new DateTime(2023, 12, 26) };
int businessDays = service.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays);

// Calculate business days with dynamic holiday rules
var holidayRules = new List<IHolidayRule> { new FixedDateHoliday(12, 25), new WeekendAdjustedHoliday(1, 1) };
int advancedBusinessDays = service.BusinessDaysBetweenTwoDates(startDate, endDate, holidayRules);
```

---

## Running Tests

### Prerequisites
Ensure you have a test runner, such as the built-in Visual Studio test runner or `dotnet test`.

### Execute Tests
Run the following command in the terminal:
```bash
dotnet test
```

The test coverage includes:
- Weekday calculations.
- Business day calculations.
- Advanced holiday rule scenarios using NSW (New South Wales) holidays for 2023 and 2024.

---

## Future Enhancements

1. Add support for regional holiday data from external APIs or databases.
2. Introduce caching mechanisms for holiday calculations to improve performance.
3. Create a sample application demonstrating real-world use cases.

---

## Contributors

Developed and maintained by [Your Name]. Contributions are welcome! Please feel free to submit pull requests or raise issues.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

Thank you for using BizDays!
