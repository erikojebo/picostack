using PicoStack.Console.Models;
using PicoStack.Core;
using PicoStack.Core.Rest;

namespace PicoStack.Console.Controllers
{
    public class UsersController : RestApiController
    {
        public HttpResponse Get()
        {
            return OK(new []
            {
                new User { FirstName = "Kalle", LastName = "Persson", Email = "challe_p@domain.com" },
                new User { FirstName = "Stina", LastName = "Karlsson", Email = "stina77@domain.com" },
                new User { FirstName = "Nils-Arne", LastName = "Bengtsson", Email = "nils-arne@bengo.com" },
            });
        }

        public HttpResponse Post(User user)
        {
            return Created(user);
        }
    }
}