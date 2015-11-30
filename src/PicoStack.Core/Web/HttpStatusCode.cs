namespace PicoStack.Core.Web
{
    public class HttpStatusCode
    {
        public readonly int Code;
        public readonly string Message;

        public HttpStatusCode(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Code} {Message}";
        }

        public static readonly HttpStatusCode OK = new HttpStatusCode(200, "OK");
        public static readonly HttpStatusCode Created = new HttpStatusCode(201, "Created");
        public static readonly HttpStatusCode Forbidden = new HttpStatusCode(403, "Forbidden");
        public static readonly HttpStatusCode NotFound = new HttpStatusCode(404, "Not found");
    }
}