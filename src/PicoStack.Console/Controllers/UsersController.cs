using PicoStack.Console.Models;
using PicoStack.Core;
using PicoStack.Core.DataAccess;
using PicoStack.Core.Rest;

namespace PicoStack.Console.Controllers
{
    public class UsersController : RestApiController
    {
        public HttpResponse Get()
        {
            var repository = new Repository("Server=.;Database=picostack;Trusted_Connection=True;");

            var users = repository.Get<User>();

            return OK(users);
        }

        public HttpResponse Post(User user)
        {
            return Created(user);
        }
    }
}