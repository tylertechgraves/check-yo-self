using System;

namespace paycheck_calculator_web.Server.Entities.Config
{
    public class ConfigurationServer
    {
        public string Uri { get; set; }
        public int RetryCount { get; set; }
        public int RetryIntervalSec { get; set; }
    }
}