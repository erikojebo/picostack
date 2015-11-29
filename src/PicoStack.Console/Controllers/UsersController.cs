using PicoStack.Console.Models;
using PicoStack.Core;
using PicoStack.Core.DataAccess;
using PicoStack.Core.Rest;

namespace PicoStack.Console.Controllers
{
    public class UsersController : RestApiController
    {
        private readonly Repository _repository;

        public UsersController()
        {
            _repository = new Repository("Server=.;Database=picostack;Trusted_Connection=True;");
        }

        public HttpResponse Get()
        {
            var users = _repository.Get<User>();

            return OK(users);
        }

        public HttpResponse Post(User user)
        {
            return Created(user);
        }
    }
}