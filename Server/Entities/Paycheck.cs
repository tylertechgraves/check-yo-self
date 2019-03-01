using System;

namespace paycheck_calculator_web.Server.Entities
{
    public class Paycheck
    {
        public Paycheck()
        {}

        public DateTime PaymentDate { get; set; }
        public int MonthSubordinal { get; set; }
    }
}