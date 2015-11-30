namespace PicoStack.Core.Web
{
    public class HttpHeader
    {
        public readonly string Name;
        public readonly string Value;

        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}\r\n";
        }

        public static HttpHeader Parse(string line)
        {
            var parts = line.Split(':');

            var name = parts[0].Trim();
            var value = parts[1].Trim();

            return new HttpHeader(name, value);
        }
    }
}