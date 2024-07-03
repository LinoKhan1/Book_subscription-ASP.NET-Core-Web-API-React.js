using Microsoft.AspNetCore.Mvc;

namespace Book_subscription.Server.API.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
