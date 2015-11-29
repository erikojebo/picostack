namespace PicoStack.Core
{
    public interface IRequestHandler
    {
        HttpResponse Handle(HttpRequest request);
    }
}