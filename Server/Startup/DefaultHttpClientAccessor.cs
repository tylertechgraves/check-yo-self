using System.Net.Http;

namespace paycheck_calculator_web.Server
{
    public class DefaultHttpClientAccessor : IHttpClientAccessor
    {
        public HttpClient Client { get; }

        public DefaultHttpClientAccessor()
        {
            Client = new HttpClient();
        }
    }
}