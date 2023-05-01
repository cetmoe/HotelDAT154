using Microsoft.AspNetCore.Mvc;

namespace HotelFrontend.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}
