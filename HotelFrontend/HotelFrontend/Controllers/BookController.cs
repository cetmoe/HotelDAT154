using HotelFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HotelFrontend.Controllers
{
    public class BookController : Controller
    {
        public async Task<IActionResult> RoomPage(string hotelName, string roomTypeId)
        {
            HttpClient client = new() { BaseAddress = new Uri("http://localhost:5008") };

            // fetch rooms
            using HttpResponseMessage response = await client.GetAsync("/roomtype/" + roomTypeId);
            var json = await response.Content.ReadAsStringAsync();
            RoomType? roomType = JsonSerializer.Deserialize<RoomType>(json);

            return View(roomType);
        }

        public async Task<IActionResult> BookRoom(DateTime? FromDate, DateTime? ToDate, int roomTypeId)
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

            Room? bookedRoom = allRooms?.Where(r => r.RoomType.Id == roomTypeId).First();


            List<User>? users = await Helpers.HttpRequest.GetAt<List<User>>("/users");
            string? UserName = HttpContext.Session.GetString("User");
            User? user = users.Where(u => u.UserName == UserName).First();

            Reservation? reservation = new()
            {
                User = user,
                Room = bookedRoom,
                From = (DateTime)FromDate,
                To = (DateTime)ToDate,
            };

            return View();
        }
    }
}