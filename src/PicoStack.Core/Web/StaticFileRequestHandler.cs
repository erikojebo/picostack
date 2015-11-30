using System.Collections.Generic;
using System.IO;

namespace PicoStack.Core.Web
{
    public class StaticFileRequestHandler : IRequestHandler
    {
        private readonly string _baseDirectory;
        private readonly IRequestHandler _next;

        private readonly IDictionary<string, string> _mimeTypes = new Dictionary<string, string>()
        {
            { ".html", "text/html" },
            { ".htm", "text/html" },
            { ".js", "text/javascript" },
            { ".css", "text/css" },
            { ".json", "application/json" },
        };

        public StaticFileRequestHandler(string baseDirectory)
        {
            var directoryInfo = new DirectoryInfo(baseDirectory);
            _baseDirectory = directoryInfo.FullName;
        }

        public StaticFileRequestHandler(string baseDirectory, IRequestHandler next) : this(baseDirectory)
        {
            _next = next;
        }

        public HttpResponse Handle(HttpRequest request)
        {
            var path = Path.Combine(_baseDirectory, request.Url.TrimStart('/'));

            var fileInfo = new FileInfo(path);

            if (!fileInfo.FullName.StartsWith(_baseDirectory))
            {
                return new HttpResponse(HttpStatusCode.Forbidden);
            }

            if (File.Exists(path))
            {
                var response = new HttpResponse(HttpStatusCode.OK, File.ReadAllBytes(path));

                var fileExtension = Path.GetExtension(path);

                if (_mimeTypes.ContainsKey(fileExtension))
                {
                    response.AddHeader("Content-Type", _mimeTypes[fileExtension]);
                }

                return response;
            }

            if (_next != null)
            {
                return _next.Handle(request);
            }

            return new HttpResponse(HttpStatusCode.NotFound);
        }
    }
}