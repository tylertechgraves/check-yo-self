using System.Net.Http;

namespace paycheck_calculator_web.Server
{
    public interface IHttpClientAccessor 
    {
        HttpClient Client { get; }
    }
}