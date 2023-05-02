using HotelFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelFrontend.Controllers
{
    public class ReservationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string? UserName = HttpContext.Session.GetString("User");

            List<Reservation> reservations = new();

            if (UserName != null)
            {
                reservations = await Helpers.HttpRequest.GetAt<List<Reservation>>("/reservations");
                reservations = reservations.FindAll(r => r.User.UserName == UserName);
            }

            return View(reservations);
        }
    }
}
