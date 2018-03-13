using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingStorage> _repository;

        [SetUp]
        public void Setup()
        {
            _existingBooking = new Booking
            {
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a",
                Id = 2
            };

            _repository = new Mock<IBookingStorage>();
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStatusCancelled_ReturnEmptyString()
        {
            Booking booking = new Booking() {Status = "Cancelled"};

            var result = BookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result == string.Empty);
        }

        [Test]
        public void BookingStartsAndFinishesBeforeExistingBooking_ReturnEmtpyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _repository.Object);

            Assert.That(result == string.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate)
            }, _repository.Object);

            Assert.That(result == _existingBooking.Reference);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _repository.Object);

            Assert.That(result == _existingBooking.Reference);
        }
        
        [Test]
        public void BookingStartsAndFinishesInMiddleOfAnExistingBooking_ReturnExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _repository.Object);

            Assert.That(result == _existingBooking.Reference);
        }

        [Test]
        public void BookingStartsInTheMiddleOfAnExistingBookingButFinishesAfter_ReturnExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _repository.Object);

            Assert.That(result == _existingBooking.Reference);
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmtpyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2)
            }, _repository.Object);

            Assert.That(result == string.Empty);
        }

        [Test]
        public void BookingOverlapButNewBookingCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled"
            }, _repository.Object);

            Assert.That(result == string.Empty);
        }



        #region Helper Methods
        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        #endregion
    }
}
