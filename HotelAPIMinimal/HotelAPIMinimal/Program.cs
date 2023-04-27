using HotelAPIMinimal.Data;
using HotelAPIMinimal.Models;
using Microsoft.EntityFrameworkCore;

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


var app = builder.Build();

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


// Haven't implemented any errorhandling

// Hotel routes

app.MapGet("/hotels", async (HotelDBContext db) => await db.Hotel.ToListAsync())
.WithName("GetHotels")
.WithTags("Hotel")
.WithOpenApi();


app.MapPost("/hotel", async (HotelDBContext db, Hotel hotel) =>
{
    await db.Hotel.AddAsync(hotel);
    await db.SaveChangesAsync();
    return Results.Created($"/hotel/{hotel.Id}", hotel);
})
.WithName("CreateHotel")
.WithTags("Hotel")
.WithOpenApi();

app.MapDelete("/hotel/{id}", async (HotelDBContext db, int id) =>
{
    var hotel = await db.Hotel.FindAsync(id);
    if (hotel is null)
    {
        return Results.NotFound();
    }
    db.Hotel.Remove(hotel);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithName("DeleteHotel")
.WithTags("Hotel")
.WithOpenApi();

// Room API

app.MapGet("/rooms", async (HotelDBContext db) => await db.Room.ToListAsync())
.WithName("GetRooms")
.WithTags("Room")
.WithOpenApi();

app.MapGet("/rooms/filter", async (HotelDBContext db, [AsParameters] FilterCriteria criteria) =>
{
    var rooms = db.Room.Select(r => r);
    if (criteria.Type != null)
    {
        rooms = rooms.Where(r => r.RoomType.Id == criteria.Type);
    }

    if (criteria.Cleaned != null)
    {
        rooms = rooms.Where(r => r.CleaningStatus == criteria.Cleaned);
    }

    if (criteria.Checkedin != null)
    {
        rooms = rooms.Where(r => r.CheckInStatus == criteria.Checkedin);
    }

    return await rooms.ToListAsync();

})
.WithName("GetFilteredRooms")
.WithTags("Room")
.WithOpenApi();

app.MapPost("/room", async (HotelDBContext db, Room room) =>
{
    await db.Room.AddAsync(room);
    await db.SaveChangesAsync();
    return Results.Created($"/room/{room.Id}", room);
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

// Guest API
app.MapGet("/guests", async (HotelDBContext db) => await db.Guest.ToListAsync())
.WithName("GetGuests")
.WithTags("Guest")
.WithOpenApi();

app.MapGet("/guest/{id}", async (HotelDBContext db, int id) =>
{
    var guest = await db.Guest.FindAsync(id);
    if (guest is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(guest);
})
.WithName("GetGuest")
.WithTags("Guest")
.WithOpenApi();

app.MapPost("/guest", async (HotelDBContext db, Guest guest) =>
{
    await db.Guest.AddAsync(guest);
    await db.SaveChangesAsync();
    return Results.Created($"/guest/{guest.Id}", guest);
})
.WithName("CreateGuest")
.WithTags("Guest")
.WithOpenApi();

app.Run();


public class FilterCriteria
{
    public int? Type { get; set; }
    public Boolean? Cleaned { get; set; }
    public Boolean? Checkedin { get; set; }
}