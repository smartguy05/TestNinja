using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Push_ObjectNull_ThrowArgumentNullException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_WhenCalled_AddItemToList()
        {
            var stack = new Fundamentals.Stack<int>();

            stack.Push(1);

            Assert.That(stack.Count == 1);
        }

        [Test]
        public void Count_Empty_ReturnZero()
        {
            var stack = new Stack<string>();

            Assert.That(stack.Count == 0);
        }

        [Test]
        public void Pop_ListCountIsZero_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<int>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_WhenCalled_RemoveLastItemFromListAndReturnIt()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var result = stack.Pop();

            Assert.That(result == 3);
            Assert.That(stack.Count == 2);
        }

        [Test]
        public void Peek_ListCountIsZero_ThrowInvalidOperationException()
        {
            var stack = new Stack<int>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_WhenCalled_ReturnsLastValueInList()
        {
            var stack = new Stack<int>();

            stack.Push(1);

            Assert.That(stack.Peek() == 1);
        }

    }
}
