using System;
using System.Collections.Generic;

namespace GuestServices.Models;

public partial class Guest
{
    public int GuestId { get; set; }

    public int MemberCode { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Company { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public ICollection<Reservation>? Reservations { get; set; }
}
