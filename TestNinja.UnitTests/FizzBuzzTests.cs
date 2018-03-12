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
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_NumberDivisibleBy3And5_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        

        [Test]
        public void GetOutput_NumberDivisibleBy3ButNot5_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_NumberDivisibleBy5ButNot3_ReturnsBuzz()
        {
            var result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_NumberNotDivisibleBy3Or5_ReturnsNumber()
        {
            var result = FizzBuzz.GetOutput(2);

            Assert.That(result, Is.EqualTo("2"));
        }
    }
}
