using System;
using NUnit.Framework;
using TestNinja;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            //Assert.IsTrue(result);
            //Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            User newUser = new User()
            {
                IsAdmin = false
            };
            reservation.MadeBy = newUser;
            var result = reservation.CanBeCancelledBy(newUser);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            User newUser = new User()
            {
                IsAdmin = false
            };
            var result = reservation.CanBeCancelledBy(newUser);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
