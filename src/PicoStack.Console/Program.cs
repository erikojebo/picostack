using PicoStack.Console.Controllers;
using PicoStack.Core;
using PicoStack.Core.Logging;
using PicoStack.Core.Rest;

namespace PicoStack.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server(new ConsoleLogger(), new RestApiRequestHandler(typeof(UsersController).Assembly));
            server.Start(8002);
        }
    }
}
