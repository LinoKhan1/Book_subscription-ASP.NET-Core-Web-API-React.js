using Microsoft.AspNetCore.Mvc;

namespace Book_subscription.Server.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
