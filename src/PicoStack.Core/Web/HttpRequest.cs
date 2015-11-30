namespace PicoStack.Core.Web
{
    public class HttpRequest
    {
        public readonly string Method;
        public readonly string Url;

        public HttpRequest(string method, string url)
        {
            Method = method;
            Url = url;
        }
    }
}