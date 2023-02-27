using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using check_yo_self.Server.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace check_yo_self.Server.Controllers.api;

[Route("api/[controller]")]
[AllowAnonymous]
public class PaycheckCalculatorController : BaseController
{
    private const int NUMBER_OF_YEARS_TO_CHECK = 1;

    public PaycheckCalculatorController()
    {
    }

    [HttpGet]
    [Route("GetPaychecks")]
    public List<Paycheck> GetPaychecks([FromQuery] DateTime startDate, [FromQuery] int numberOfYearsToCheck = 1, [FromQuery] int payPeriodDays = 14)
    {
        var date = startDate;
        // We're only going to evaluate dates in the coming year.
        var endDate = date.AddDays(-1).AddYears(numberOfYearsToCheck);
        var result = new List<Paycheck>();

        while (date < endDate)
        {
            var paycheck = new Paycheck()
            {
                PaymentDate = date,
                MonthSubordinal = date.Month,
                Year = date.Year
            };
            result.Add(paycheck);

            date = date.AddDays(payPeriodDays);
        }

        return result;
    }

    [HttpGet]
    [Route("GetMonthsWithMultiplePaychecks")]
    public List<Month> GetMonthsWithMultiplePaychecks([FromQuery] DateTime startDate, [FromQuery] int numberOfChecksInAGivenMonth, [FromQuery] int payPeriodDays = 14)
    {
        var paychecks = GetPaychecks(startDate, payPeriodDays);

        var months = GetMonths(paychecks, numberOfChecksInAGivenMonth);

        return months;
    }

    private List<Paycheck> GetPaychecks(DateTime startDate, int payPeriodDays)
    {
        var endDate = startDate.AddDays(-1).AddYears(NUMBER_OF_YEARS_TO_CHECK);
        var result = new List<Paycheck>();

        while (startDate < endDate)
        {
            var paycheck = new Paycheck()
            {
                PaymentDate = startDate,
                MonthSubordinal = startDate.Month,
                Year = startDate.Year
            };

            result.Add(paycheck);

            startDate = startDate.AddDays(payPeriodDays);
        }

        return result;
    }

    private List<Month> GetMonths(List<Paycheck> paychecks, int numberOfChecksInAGivenMonth)
    {
        var result = new List<Month>();

        var monthGroupings = paychecks.GroupBy(p => new { p.Year, p.MonthSubordinal }).Where(g => g.Count() == numberOfChecksInAGivenMonth);

        foreach (var group in monthGroupings)
        {
            var year = group.Key.Year;

            var month = new Month()
            {
                Description = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.MonthSubordinal) + " " + year
            };

            result.Add(month);
        }

        return result;
    }
}
