using PicoStack.Core.DependencyInjection;
using PicoStack.Core.Logging;

namespace PicoStack.Console
{
    public class RegistryConfiguration
    {
        public static Registry Registry = new Registry();

        public static void Initialize()
        {
            Registry.Register<ILogger, ConsoleLogger>();
        }
    }
}