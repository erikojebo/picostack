
using PicoStack.Core.Serialization;
using PicoStack.Core.Web;

namespace PicoStack.Core.Rest
{
    public class RestApiController
    {
        protected HttpResponse OK(object obj)
        {
            return Json(HttpStatusCode.OK, obj);
        }

        protected HttpResponse Created(object obj)
        {
            return Json(HttpStatusCode.Created, obj);
        }

        protected HttpResponse NotFound()
        {
            return new HttpResponse(HttpStatusCode.NotFound);
        }

        protected HttpResponse Json(HttpStatusCode statusCode, object obj)
        {
            //var body = JsonConvert.SerializeObject(obj)
            var body = JsonSerializer.Serialize(obj);

            var response = new HttpResponse(statusCode, body);

            response.AddHeader("Content-Type", "application/json; charset=utf-8");

            return response;
        }
    }
}