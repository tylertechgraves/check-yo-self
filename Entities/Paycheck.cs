using System;

namespace check_yo_self.Server.Entities;

public class Paycheck
{
    public Paycheck()
    { }

    public DateTime PaymentDate { get; set; }
    public int MonthSubordinal { get; set; }
    public int Year { get; set; }
}
