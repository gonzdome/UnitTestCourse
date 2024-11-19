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
    public class GradingCalculatorNUnitTests
    {
        public GradingCalculator gradingCalculator;
        [SetUp]
        public void Setup()
        {
            gradingCalculator = new();
        }

        [Test]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsA()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;
            Assert.That(gradingCalculator.GetGrade(), Is.EqualTo("A"));
        }

        [Test]
        [TestCase(85, 90)]
        [TestCase(95, 65)]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsB(int score, int attPercentage)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attPercentage;
            Assert.That(gradingCalculator.GetGrade(), Is.EqualTo("B"));
        }

        [Test]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsC()
        {
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;
            Assert.That(gradingCalculator.GetGrade(), Is.EqualTo("C"));
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsF(int score, int attPercentage)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attPercentage;
            Assert.That(gradingCalculator.GetGrade(), Is.EqualTo("F"));
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_InputScoreAndAttendancePercentageAsInt_CheckAllGrades(int score, int attPercentage)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attPercentage;
            return gradingCalculator.GetGrade();
        }
    }
}
