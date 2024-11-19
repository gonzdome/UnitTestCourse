using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new(); // Arrange
        }

        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Act
            int result = calculator.AddNumbers(10, 20);

            // Assert
            ClassicAssert.AreEqual(30, result);
        }

        [Test]
        [TestCase(10.21, 23.45, ExpectedResult = 33.66)]
        [TestCase(12.59, 23.25, ExpectedResult = 35.84)]
        [TestCase(20.77, 23.55, ExpectedResult = 44.32)]
        public double AddNumbers_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            return calculator.AddDoubleNumbers(a, b);
        }

        [Test]
        [TestCase(1.10, 8.90)]
        [TestCase(2.50, 7.40)]
        [TestCase(5.00, 5.10)]
        public void AddNumbers_InputTwoDouble_GetCloseAddition(double a, double b)
        {
            double result = calculator.AddDoubleNumbers(a, b);
            ClassicAssert.AreEqual(10.00, result, .1); // The number .1 indicates that the difference can be between 34.84 and 36.84 and .1 35.74 and 35.94
        }

        [Test]
        [TestCase(3, ExpectedResult = true)]
        public bool IsNumberOdd_InputOddInt_CheckIfNumberIsOdd(int a)
        {
            return calculator.IsOddNumber(3);
        }

        [Test]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(9, ExpectedResult = true)]
        public bool IsNumberOdd_InputEvenInt_CheckIfNumberIsEven(int a)
        {
            return calculator.IsOddNumber(a);

            // These two bellow where used when the type of return was void and with no TestCase Attribute
            //Assert.That(result, Is.EqualTo(false));
            //ClassicAssert.IsFalse(result); // Both act the same, but for booleans this might be the best one
        }

        [Test]
        [TestCase(10, ExpectedResult = false)]
        //[TestCase(11, ExpectedResult = false)]
        public bool IsNumberOdd_InputInt_CheckIfNumberIsEven(int a)
        {
            return calculator.IsOddNumber(a);
        }

        [Test]
        public void OddRange_InputMinAndMaxInt_CheckOddNumbersRange()
        {
            List<int> expectedOddRange = new() { 5, 7, 9 };

            List<int> result = calculator.GetOddRange(5, 10);

            ClassicAssert.That(result, Is.EquivalentTo(expectedOddRange));
            ClassicAssert.AreEqual(result, expectedOddRange);
            ClassicAssert.Contains(7, result);
            ClassicAssert.That(result, Is.Not.Empty);
            ClassicAssert.That(result.Count(), Is.EqualTo(3));
            ClassicAssert.That(result, Has.No.Member(8));
            ClassicAssert.That(result, Is.Ordered.Ascending); // Check if is ordered by ascending (has descending too)
            ClassicAssert.That(result, Is.Unique); // Check if every number is unique
        }
    }
}
