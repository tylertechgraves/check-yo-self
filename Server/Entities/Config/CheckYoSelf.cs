using Newtonsoft.Json;

namespace paycheck_calculator_web.Server.Entities.Config
{
  public class CheckYoSelf
  {
    public string MonthsApiEndpoint { get; set; }
    public string ChecksApiEndpoint { get; set; }
  }
}