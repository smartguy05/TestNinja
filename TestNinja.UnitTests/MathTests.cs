using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        // Setup
        // Teardown
        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [Test]
        [Ignore("Because I wanted to!")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgIsGreater_ReturnFirstArg()
        {
            var result = _math.Max(2, 1);
            
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_SecondArgIsGreater_ReturnSecondArg()
        {
            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }
        
        [Test]
        public void Max_ArgsAreEqual_ReturnTheSameArgument()
        {
            var result = _math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
