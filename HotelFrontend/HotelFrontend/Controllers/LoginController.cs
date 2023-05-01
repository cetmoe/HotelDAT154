using HotelFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelFrontend.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserName)
        {
            HttpClient client = new() { BaseAddress = new Uri("http://localhost:5008") };

            List<User> users = await Helpers.HttpRequest.GetAt<List<User>>("/users");

            User user = users.Find(u => u.UserName.Equals(UserName));

            if (user != null)
            {
                HttpContext.Session.SetString("User", user.UserName);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
    }
}
