using System;
using System.Collections.Generic;

namespace DesktopApplication.Models;

public partial class RoomType
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string RoomSize { get; set; } = null!;

    public int Beds { get; set; }

    public int Price { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
