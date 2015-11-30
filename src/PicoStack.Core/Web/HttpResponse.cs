using System.Collections.Generic;
using System.Text;

namespace PicoStack.Core.Web
{
    public class HttpResponse
    {
        public readonly HttpStatusCode StatusCode;

        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Headers = new List<HttpHeader>();
        }

        public HttpResponse(HttpStatusCode statusCode, byte[] body = null) : this(statusCode)
        {
            Body = body;
        }

        public HttpResponse(HttpStatusCode statusCode, string body) : this(statusCode)
        {
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