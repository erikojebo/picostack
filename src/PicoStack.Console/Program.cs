using System.IO;
using System.Reflection;
using PicoStack.Console.Controllers;
using PicoStack.Core;
using PicoStack.Core.Logging;
using PicoStack.Core.Rest;
using PicoStack.Core.Web;

namespace PicoStack.Console
{   
    class Program
    {
        static void Main(string[] args)
        {
            RegistryConfiguration.Initialize();

            var baseDirectory = args.Length > 0 ? args[0] : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var restApiRequestHandler = new RestApiRequestHandler(typeof(UsersController).Assembly);
            var staticFileRequestHandler = new StaticFileRequestHandler(baseDirectory, restApiRequestHandler);

            var server = new Server(
                RegistryConfiguration.Registry.Resolve<ILogger>(), 
                staticFileRequestHandler);

            server.Start(8002);
        }
    }
}
