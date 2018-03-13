using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IBookingStorage
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingStorage : IBookingStorage
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

            return bookings;
        }
    }
}
