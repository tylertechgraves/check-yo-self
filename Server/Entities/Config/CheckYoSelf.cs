using Newtonsoft.Json;

namespace paycheck_calculator_web.Server.Entities.Config
{
  public class CheckYoSelf
  {
    public string MonthsApiEndpoint { get; set; }
    public string ChecksApiEndpoint { get; set; }
    public bool EmployeesButtonVisible { get; set; }
    public string ListEmployeesEndpoint { get; set; }
    public string EmployeesApiBaseUrl { get; set; }
    public string QueryForEmployeesByFullNameEndpoint { get; set; }
  }
}