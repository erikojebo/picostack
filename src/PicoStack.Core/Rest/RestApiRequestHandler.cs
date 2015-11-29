using System;
using System.Linq;
using System.Reflection;

namespace PicoStack.Core.Rest
{
    public class RestApiRequestHandler : IRequestHandler
    {
        private readonly Assembly _controllerAssembly;

        public RestApiRequestHandler(Assembly controllerAssembly)
        {
            _controllerAssembly = controllerAssembly;
        }

        public HttpResponse Handle(HttpRequest request)
        {
            var routeParts = request.Url.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (!routeParts.Any())
                return new HttpResponse(HttpStatusCode.NotFound);

            var controllerName = $"{routeParts[0]}Controller";

            var controllerType = _controllerAssembly
                .GetTypes()
                .FirstOrDefault(x => x.Name.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase));

            var controllerMethod = controllerType?
                .GetMethods()
                .FirstOrDefault(x => request.Method.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));

            if (controllerType == null || controllerMethod == null)
            {
                return new HttpResponse(HttpStatusCode.NotFound);
            }

            var controllerInstance = Activator.CreateInstance(controllerType);
            return (HttpResponse)controllerMethod.Invoke(controllerInstance, null);
        }
    }
}