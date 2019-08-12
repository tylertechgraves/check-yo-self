using Newtonsoft.Json;
using paycheck_calculator_web.Server;

namespace paycheck_calculator_web.Server.Entities.Config
{
    public class Elasticsearch
    {
        private string _uri;
        public string Uri {
            get { return UrlAdjuster.ReplaceHostAndSetTrailingSlash(this._uri); }
            set { this._uri = value; }
        }

        [JsonProperty("username")]
        public string Username {get; set;}

        [JsonProperty("password")]
        public string Password {get; set;}

        [JsonProperty("indexName")]
        public string IndexName { get; set; }

        [JsonProperty("maxBulkInsertCount")]
        public int MaxBulkInsertCount { get; set; }

        [JsonProperty("numberOfReplicas")]
        public int NumberOfReplicas { get; set; }

        [JsonProperty("numberOfShards")]
        public int NumberOfShards { get; set; }

        [JsonProperty("useAuthentication")]
        public bool UseAuthentication { get; set; }
    }
}