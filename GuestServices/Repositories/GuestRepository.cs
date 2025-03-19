using GuestServices.Data;
using GuestServices.Interface;
using GuestServices.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestServices.Repositories
{
    public class GuestRepository : IGuestService
    {
        private readonly GuestContext _context;

        public GuestRepository(GuestContext context)
        {
            _context = context;
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            return _context.Guests.Include(g => g.Reservations).ToList();
        }

        public Guest? GetGuestById(int id)
        {
            return _context.Guests.Include(g => g.Reservations)
                                  .FirstOrDefault(x => x.GuestId == id);
        }

        public IEnumerable<Reservation> GetGuestReservations(int guestId)
        {
            var guest = _context.Guests.Include(g => g.Reservations)
                                       .FirstOrDefault(g => g.GuestId == guestId);
            return guest?.Reservations ?? new List<Reservation>();
        }

        public Guest AddGuest(Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            return guest;
        }

        public Guest UpdateGuest(int id, Guest updatedGuest)
        {
            var existingGuest = _context.Guests.FirstOrDefault(x => x.GuestId == id);
            if (existingGuest == null)
            {
                return null;
            }

            existingGuest.Name = updatedGuest.Name;
            existingGuest.Email = updatedGuest.Email;
            existingGuest.PhoneNumber = updatedGuest.PhoneNumber;
            existingGuest.Gender = updatedGuest.Gender;
            existingGuest.Company = updatedGuest.Company;
            existingGuest.Address = updatedGuest.Address;
            existingGuest.MemberCode = updatedGuest.MemberCode;

            _context.SaveChanges();
            return existingGuest;
        }

        public Guest? DeleteGuest(int id)
        {
            var guest = _context.Guests.FirstOrDefault(x => x.GuestId == id);
            if (guest == null)
            {
                return null;
            }

            _context.Guests.Remove(guest);
            _context.SaveChanges();
            return guest;
        }
    }
}
