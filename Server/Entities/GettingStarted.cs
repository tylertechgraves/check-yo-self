using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace paycheck_calculator_web.Server.Entities
{
    public class GettingStarted
    {
        [Key]
        [JsonIgnore]
        public string _id {get; set;}
        public string ApplicationName { get; set; }
    }
}