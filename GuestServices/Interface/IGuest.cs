using GuestServices.Models;

namespace GuestServices.Interface
{
    public interface IGuestService
    {
        IEnumerable<Guest> GetAllGuests();
        Guest? GetGuestById(int id);
        Guest AddGuest(Guest guest);
        Guest UpdateGuest(int id, Guest updatedGuest);
        Guest? DeleteGuest(int id);

        IEnumerable<Reservation> GetGuestReservations(int guestId);
    }
}
