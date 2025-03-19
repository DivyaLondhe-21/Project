using System;
using System.Collections.Generic;

namespace RoomServices.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int RoomNumber { get; set; }

    public string RoomType { get; set; } = null!;

    public decimal PricePerNight { get; set; }

    public string Period { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool Availability { get; set; }

    //public ICollection<Reservation> Reservations { get; set; }
}
