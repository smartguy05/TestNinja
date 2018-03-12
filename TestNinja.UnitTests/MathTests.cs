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
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(2,2,2)]
        public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
    }
}
