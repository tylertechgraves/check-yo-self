using System;

namespace paycheck_calculator_web.Server.Entities.Config
{
    public class ApplicationInsights
    {
        public bool TelemetryEnabled
        {
            get
            {
                return this.InstrumentationKey != Guid.Empty;
            }
        }
        public Guid InstrumentationKey { get; set; }
    }
}