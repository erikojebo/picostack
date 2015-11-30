using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PicoStack.Core.Logging;

namespace PicoStack.Core
{
    /* REFERENCES:
       
        https://www.jmarshall.com/easy/http/
    */

    /* REQUIREMENTS:
      
        Request ends with a blank line (newline for last row followed by another newline, \r\n\r\n)
        Response must include a Date header on the format "Date: Fri, 31 Dec 1999 23:59:59 GMT", specified in GMT

    */

    /* DEMO STEPS:

        - Basic server with hard coded response
        - Parse HTTP method and requested URL
        - Parse response / request (tests)

    */

    public class Server
    {
        private readonly ILogger _logger;
        private readonly IRequestHandler _requestHandler;

        public Server(ILogger logger, IRequestHandler requestHandler)
        {
            _logger = logger;
            _requestHandler = requestHandler;
        }

        public void Start(int port)
        {
            var ipString = "127.0.0.1";

            var listener = new TcpListener(IPAddress.Parse(ipString), port);

            _logger.Write($"Listening at: {ipString}:{port}...");

            listener.Start();

            while (true)
            {
                try
                {
                    HandleRequest(listener);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void HandleRequest(TcpListener listener)
        {
            var client = listener.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(x => HandleRequest(client));
        }

        private void HandleRequest(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                using (var writer = new StreamWriter(stream))
                using (var reader = new StreamReader(stream))
                {
                    var request = ParseRequest(reader);
                    var response = _requestHandler.Handle(request);

                    var dateString = DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss", new CultureInfo("en-US")) +
                                     " GMT";

                    response.AddHeader("Date", dateString);
                    response.AddHeader("Server", "PicoStack 0.1 alpha");
                    response.AddHeader("Connection", "Close");

                    writer.Write($"HTTP/1.1 {response.StatusCode.Code} {response.StatusCode.Message}\r\n");

                    foreach (var header in response.Headers)
                    {
                        writer.Write(header.ToString());
                    }

                    if (response.Body != null)
                    {
                        writer.Write("\r\n");
                        writer.Flush();
                        stream.Write(response.Body, 0, response.Body.Length);
                    }

                    writer.Flush();
                    stream.Flush();
                    writer.Close();
                }
            }
            finally
            {
                client.Close();
            }
        }

        private HttpRequest ParseRequest(StreamReader reader)
        {
            var request = string.Empty;

            while (!request.EndsWith("\r\n\r\n"))
            {
                var character = (char)reader.Read();
                request += character;
            }

            _logger.Write($"Request received: {Environment.NewLine}{request}");

            var firstLineEnding = request.IndexOf("\r\n");
            var firstLine = request.Substring(0, firstLineEnding);

            var requestHeaderLineParts = firstLine.Split(' ');
            var requestMethod = requestHeaderLineParts[0];
            var requestedUrl = requestHeaderLineParts[1];

            return new HttpRequest(requestMethod, requestedUrl);
        }
    }
}
