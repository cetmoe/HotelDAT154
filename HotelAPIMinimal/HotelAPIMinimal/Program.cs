using HotelAPIMinimal.Data;
using HotelAPIMinimal.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<HotelDBContext>(options => options.UseSqlServer(@"Data Source=tcp:cetmoe.database.windows.net,1433;Initial Catalog=cetmoe;User Id=cetmoe@cetmoe;Password=7fgQH.RcGusMrvv"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Hotel API",
        Description = "API for interacting with the three applications in the hotel system",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();

// ExceptionHandler
app.Map("/exception", ()
    =>
{ throw new InvalidOperationException("Sample Exception"); });




// Guest API
app.MapGet("/users", async (HotelDBContext db) => await db.User.ToListAsync())
.WithName("GetUsers")
.WithTags("User")
.WithOpenApi();

app.MapGet("/user/{id}", async (HotelDBContext db, int Id) =>
{
    var guest = await db.User.FindAsync(Id);
    if (guest is null) return Results.NotFound("User not found.");
    return Results.Ok(guest);
})
.WithName("GetUser")
.WithTags("User")
.WithOpenApi();

app.MapPost("/user", async (HotelDBContext db, User User) =>
{
    if (User is null
    || User.UserName is null
    || User.FirstName is null
    || User.LastName is null
    || User.Email is null) return Results.Problem("User data missing.");

    List<User> validation = await db.User.Where(u => u.UserName == User.UserName).ToListAsync();
    if (validation.Count > 0) return Results.Problem("User already exists.");

    await db.User.AddAsync(User);
    await db.SaveChangesAsync();
    return Results.Created($"Created a guest at endpoint /user/{User.UserId}", User);
})
.WithName("CreateUser")
.WithTags("User")
.WithOpenApi();







// Room API
app.MapGet("/rooms", async (HotelDBContext db) => await db.Room
.Include(r => r.RoomType)
.ToListAsync())
.WithName("GetRooms")
.WithTags("Room")
.WithOpenApi();

app.MapGet("/rooms/filter", async (HotelDBContext db, [AsParameters] FilterCriteria criteria) =>
{
    var rooms = db.Room
    .Include(r => r.RoomType)
    .Select(r => r);

    if (criteria.TypeReq())
    {
        rooms = rooms.Where(r => criteria.MinBeds == null || r.RoomType.Beds >= criteria.MinBeds);
        rooms = rooms.Where(r => criteria.MaxBeds == null || r.RoomType.Beds <= criteria.MaxBeds);
        rooms = rooms.Where(r => criteria.MinPrice == null || r.RoomType.Beds >= criteria.MinPrice);
        rooms = rooms.Where(r => criteria.MaxPrice == null || r.RoomType.Beds <= criteria.MaxPrice);
    }

    if (criteria.Checkedin != null)
    {
        rooms = rooms.Where(r => r.CheckInStatus == criteria.Checkedin);
    }

    var listOfRooms = await rooms.ToListAsync();
    if (listOfRooms.Count > 0) return Results.Ok(listOfRooms);

    return Results.NotFound();

})
.WithName("GetFilteredRooms")
.WithTags("Room")
.WithOpenApi();

app.MapPost("/room", async (HotelDBContext db, Room room) =>
{
    var roomType = await db.RoomType.FindAsync(room.RoomType.Id);

    if (roomType == null) return Results.NotFound();

    await db.Room.AddAsync(room);
    await db.SaveChangesAsync();
    return Results.Created($"Created a room at endpoint /room/{room.Id}", room);
})
.WithName("CreateRoom")
.WithTags("Room")
.WithOpenApi();

app.MapPut("/room/checkin/{id}", async (HotelDBContext db, int id) =>
{
    var room = await db.Room.FindAsync(id);
    if (room is null)
    {
        return Results.NotFound();
    }
    room.CheckInStatus = !room.CheckInStatus;
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("ToggleCheckIn")
.WithTags("Room")
.WithOpenApi();

app.MapPut("/room/cleaning/{id}", async (HotelDBContext db, int id) =>
{
    var room = await db.Room.FindAsync(id);
    if (room is null)
    {
        return Results.NotFound();
    }
    room.CleaningStatus = !room.CleaningStatus;
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("ToggleCleaning")
.WithTags("Room")
.WithOpenApi();


app.MapDelete("/room/{id}", async (HotelDBContext db, int id) =>
{
    var room = await db.Room.FindAsync(id);
    if (room is null)
    {
        return Results.NotFound();
    }
    db.Room.Remove(room);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithName("DeleteRoom")
.WithTags("Room")
.WithOpenApi();








// RoomType API

app.MapGet("/roomtype", async (HotelDBContext db) => await db.RoomType.ToListAsync())
.WithName("GetRoomTypes")
.WithTags("RoomType")
.WithOpenApi();

app.MapGet("/roomtype/{id}", async (HotelDBContext db, int id) =>
{
    var roomType = await db.RoomType.FindAsync(id);
    if (roomType is null)
    {
        return Results.NotFound("RoomType not found.");
    }
    return Results.Ok(roomType);
})
.WithName("GetRoomTypeById")
.WithTags("RoomType")
.WithOpenApi();

app.MapPost("/roomtype", async (HotelDBContext db, RoomType roomtype) =>
{
    await db.RoomType.AddAsync(roomtype);
    await db.SaveChangesAsync();
    return Results.Created($"Created a roomtype at endpoint /roomtype/{roomtype.Id}", roomtype);
})
.WithName("CreateRoomType")
.WithTags("RoomType")
.WithOpenApi();

app.MapDelete("/roomtype/{id}", async (HotelDBContext db, int id) =>
{
    var roomType = await db.RoomType.FindAsync(id);
    if (roomType is null)
    {
        return Results.NotFound();
    }
    db.RoomType.Remove(roomType);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithName("DeleteRoomType")
.WithTags("RoomType")
.WithOpenApi();








// Reservation API

app.MapGet("/reservations", async (HotelDBContext db) => await db.Reservation
    .Include(r => r.User)
    .Include(r => r.Room)
    .Include(r => r.Room.RoomType)
    .ToListAsync())
.WithName("GetReservations")
.WithTags("Reservation")
.WithOpenApi();

app.MapPost("/reservation", async (HotelDBContext db, Reservation res) =>
{

    var userId = res.User.UserId;
    var roomId = res.Room.Id;
    res.Room = null;
    res.User = null;

    User? user = await db.User.FindAsync(userId);
    Room? room = await db.Room.FindAsync(roomId);

    res.Room = room;
    res.User = user;

    await db.Reservation.AddAsync(res);
    await db.SaveChangesAsync();
    return Results.Created($"Created a reservation at endpoint /room/{res.Id}", res);
})
.WithName("CreateReservation")
.WithTags("Reservation")
.WithOpenApi();

app.MapPut("/reservation/{id}", async (HotelDBContext db, Reservation newRes) =>
{
    if (await db.Room.FindAsync(newRes.Room.Id) == null) return Results.NotFound("Room does not exist.");

    Reservation? res = await db.Reservation.FindAsync(newRes.Id);
    if (res is null) return Results.NotFound("The reservation to be edited does not exist.");

    res = newRes;
    await db.SaveChangesAsync();
    return Results.Ok("Reservation changes applied successfully.");
})
.WithName("UpdateReservation")
.WithTags("Reservation")
.WithOpenApi();

app.MapDelete("/reservation/{id}", async (HotelDBContext db, int id) =>
{
    var res = await db.Reservation.FindAsync(id);
    if (res is null) return Results.NotFound("Reservation not found.");

    db.Reservation.Remove(res);
    await db.SaveChangesAsync();
    return Results.Ok(id);
})
.WithName("DeleteReservation")
.WithTags("Reservation")
.WithOpenApi();










// Service Task API

app.MapGet("/services", async (HotelDBContext db) => await db.ServiceTask.Include(s => s.Room).Include(s => s.Room.RoomType).ToListAsync())
.WithName("GetServices")
.WithTags("ServiceTask")
.WithOpenApi();

app.MapPost("/service", async (HotelDBContext db, ServiceTask ser) =>
{
    Room? room = await db.Room.FindAsync(ser.Room.Id);

    if (room is null) return Results.NotFound();

    await db.ServiceTask.AddAsync(ser);
    await db.SaveChangesAsync();
    return Results.Created($"Created a service at endpoint /service/{ser.Id}", ser);
})
.WithName("CreateService")
.WithTags("ServiceTask")
.WithOpenApi();

app.MapPut("/service", async (HotelDBContext db, ServiceTask ser) =>
{
    Room? room = await db.Room.FindAsync(ser.Room.Id);
    ServiceTask service = await db.ServiceTask.FindAsync(ser.Id);

    if (room is null || service is null)
    {
        Debug.WriteLine("null");
        return Results.NotFound();
    }

    service.Note = ser.Note;
    service.Status = ser.Status;
    await db.SaveChangesAsync();
    return Results.Ok(ser);
})
.WithName("UpdateService")
.WithTags("ServiceTask")
.WithOpenApi();

app.MapDelete("/service/{id}", async (HotelDBContext db, int id) =>
{
    var ser = await db.ServiceTask.FindAsync(id);

    if (ser is null) return Results.NotFound();

    db.ServiceTask.Remove(ser);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithName("DeleteService")
.WithTags("ServiceTask")
.WithOpenApi(); ;

app.Run();

public class FilterCriteria
{
    public int? MinBeds { get; set; }
    public int? MaxBeds { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }

    public bool? Cleaned { get; set; }
    public bool? Checkedin { get; set; }

    public bool TypeReq()
    {
        if (MinBeds != null || MaxBeds != null || MinPrice != null || MaxPrice != null) return true;
        return false;
    }
}