namespace ReservationServices.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }
    public int GuestId { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public int NumberOfChildren { get; set; }
    public int NumberOfAdults { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfNights { get; set; }
    public string Status { get; set; } = null!;
    public decimal Discount { get; set; }
    public int? PaymentId { get; set; }
}
