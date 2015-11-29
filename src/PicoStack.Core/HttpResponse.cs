using System.Collections.Generic;
using System.Text;

namespace PicoStack.Core
{
    public class HttpResponse
    {
        public readonly HttpStatusCode StatusCode;

        public HttpResponse(HttpStatusCode statusCode, string body = null)
        {
            StatusCode = statusCode;
            Headers = new List<HttpHeader>();

            if (body != null)
            {
                Body = Encoding.UTF8.GetBytes(body);
                AddHeader("Content-Length", Body.Length.ToString());
            }
        }

        public IList<HttpHeader> Headers { get; }
        public byte[] Body { get; }

        public void AddHeader(string name, string value)
        {
            var header = new HttpHeader(name, value);
            Headers.Add(header);
        }
    }
}