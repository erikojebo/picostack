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
            RegistryConfiguration.Initialize();

            var server = new Server(
                RegistryConfiguration.Registry.Resolve<ILogger>(), 
                new RestApiRequestHandler(typeof(UsersController).Assembly));

            server.Start(8002);
        }
    }
}
