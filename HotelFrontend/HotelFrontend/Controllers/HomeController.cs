using HotelFrontend.Helpers;
using HotelFrontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelFrontend.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allRooms = await Helpers.HttpRequest.GetAt<List<Room>>("/rooms");

            List<RoomType>? roomTypes = allRooms?
                .Select(r => r.RoomType)
                .DistinctBy(rt => rt.Id)
                .ToList();

            return View(roomTypes);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime? FromDate, DateTime? ToDate, int? MinBeds, int? MaxBeds)
        {
            var allRooms = await Helpers.HttpRequest.GetAt<List<Room>>("/rooms");

            var allReservations = await Helpers.HttpRequest.GetAt<List<Reservation>>("/reservations");

            // Removes rooms that are occupied with in the date
            allRooms?
                .RemoveAll(r => allReservations?.Find(res =>
                {
                    if (res.Room.Id == r.Id)
                    {
                        if (FromDate != null && res.From < FromDate && res.To > FromDate) return true;
                        if (ToDate != null && res.From < ToDate && res.To > ToDate) return true;
                        if (FromDate != null && ToDate != null && res.From >= FromDate && res.To <= ToDate) return true;
                    }
                    return false;
                }) != null);

            if (MinBeds != null)
            {
                allRooms?.RemoveAll(r => r.RoomType.Beds < MinBeds);
            }

            if (MaxBeds != null)
            {
                allRooms?.RemoveAll(r => r.RoomType.Beds > MaxBeds);
            }

            List<RoomType>? filteredRoomTypes = allRooms?
                .Select(r => r.RoomType)
                .DistinctBy(r => r.Id)
                .ToList();


            return View(filteredRoomTypes);
        }
    }
}
