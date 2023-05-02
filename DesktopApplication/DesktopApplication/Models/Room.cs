using System;
using System.Collections.Generic;

namespace DesktopApplication.Models;

public partial class Room
{
    public int Id { get; set; }

    public int RoomNumber { get; set; }

    public int RoomTypeId { get; set; }

    public bool CheckInStatus { get; set; }

    public bool CleaningStatus { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual RoomType RoomType { get; set; } = null!;

    public virtual ICollection<ServiceTask> ServiceTasks { get; set; } = new List<ServiceTask>();
}
