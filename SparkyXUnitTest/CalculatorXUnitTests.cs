using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CalculatorXUnitTests
    {
        private Calculator calculator;

        public CalculatorXUnitTests() 
        { 
            calculator = new Calculator();
        }

        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Act
            int result = calculator.AddNumbers(10, 20);

            // Assert
            Assert.Equal(30, result);
        }

        [Theory]
        [InlineData(10.21, 23.45, 33.66)]
        [InlineData(12.59, 23.25, 35.84)]
        [InlineData(20.77, 23.55, 44.32)]
        public void AddNumbers_InputTwoDouble_GetCorrectAddition(double a, double b, double expectedResult)
        {
            var result = calculator.AddDoubleNumbers(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1.10, 8.90)]
        [InlineData(2.50, 7.40)]
        [InlineData(5.00, 5.10)]
        public void AddNumbers_InputTwoDouble_GetCloseAddition(double a, double b)
        {
            double result = calculator.AddDoubleNumbers(a, b);
            Assert.Equal(10.00, result, 0); // The third param indicates the decimal place and if it'll round or not
        }

        [Theory]
        [InlineData(3, true)]
        public void IsNumberOdd_InputOddInt_CheckIfNumberIsOdd(int a, bool expectedResult)
        {
            var result = calculator.IsOddNumber(3);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(3, true)]
        [InlineData(5, true)]
        [InlineData(9, true)]
        public void IsNumberOdd_InputEvenInt_CheckIfNumberIsEven(int a, bool expectedResult)
        {
            var result = calculator.IsOddNumber(a);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(10, false)]
        //[InlineData(11, false)]
        public void IsNumberOdd_InputInt_CheckIfNumberIsEven(int a, bool expectedResult)
        {
            var result = calculator.IsOddNumber(a);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void OddRange_InputMinAndMaxInt_CheckOddNumbersRange()
        {
            List<int> expectedOddRange = new() { 5, 7, 9 };

            List<int> result = calculator.GetOddRange(5, 10);

            Assert.Equal(expectedOddRange, result);
            Assert.Equal(result, expectedOddRange);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.DoesNotContain(8, result);
            Assert.Equal(result.OrderBy(u => u), result); // Check if is ordered by ascending (has descending too)
        }
    }
}
