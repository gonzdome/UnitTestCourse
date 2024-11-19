using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyMSTest
{
    [TestClass]
    public class CalculatorMSTests
    {
        [TestMethod]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            // Arrange
            Calculator calc = new();

            // Act
            int result = calc.AddNumbers(10, 20);

            // Assert
            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void IsNumberOdd_InputOddInt_CheckIfNumberIsOdd()
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool result = calc.IsOddNumber(3);

            // Assert
            Assert.AreEqual(true, result);
            Assert.IsTrue(result); // Both act the same, but for booleans this might be the best one
        }

        [TestMethod]
        public void IsNumberOdd_InputEvenInt_CheckIfNumberIsEven()
        {
            // Arrange
            Calculator calc = new();

            // Act
            bool result = calc.IsOddNumber(10);

            // Assert
            Assert.AreEqual(false, result);
            Assert.IsFalse(result); // Both act the same, but for booleans this might be the best one
        }
    }
}
