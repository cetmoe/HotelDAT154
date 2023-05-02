using System;
using System.Collections.Generic;

namespace DesktopApplication.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
