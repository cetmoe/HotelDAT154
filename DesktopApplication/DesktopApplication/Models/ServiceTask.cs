using System;
using System.Collections.Generic;

namespace DesktopApplication.Models;

public partial class ServiceTask
{
    public int Id { get; set; }

    public int Type { get; set; }

    public DateTime ScheduledDate { get; set; }

    public int RoomId { get; set; }

    public virtual Room Room { get; set; } = null!;
}
