namespace PicoStack.Core.Web
{
    public interface IRequestHandler
    {
        HttpResponse Handle(HttpRequest request);
    }
}