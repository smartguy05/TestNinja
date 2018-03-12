using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _dmCalculator;

        [SetUp]
        public void Setup()
        {
            _dmCalculator = new DemeritPointsCalculator();
            
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedLessThanZeroOrGreaterThanMax_ThrowOutOfRangeException(int speed)
        {
            Assert.That(() => _dmCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(30,0)]
        [TestCase(65,0)]
        [TestCase(66,0)]
        [TestCase(70,1)]
        [TestCase(75,2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int demerit)
        {
            var result = _dmCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(demerit));
        }
    }
}
