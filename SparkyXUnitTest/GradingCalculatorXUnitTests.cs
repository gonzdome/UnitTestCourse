using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class GradingCalculatorXUnitTests
    {
        private GradingCalculator gradingCalculator = new();

        public GradingCalculatorXUnitTests () 
        {
            gradingCalculator = new();
        }

        [Fact]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsA()
        {
            GradingCalculator gradingCalculator = new();

            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;
            Assert.Equal("A", gradingCalculator.GetGrade());
        }

        [Theory]
        [InlineData(85, 90)]
        [InlineData(95, 65)]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsB(int score, int attPercentage)
        {
            GradingCalculator gradingCalculator = new();

            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attPercentage;
            Assert.Equal("B", gradingCalculator.GetGrade());
        }

        [Fact]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsC()
        {
            GradingCalculator gradingCalculator = new();

            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;
            Assert.Equal("C", gradingCalculator.GetGrade());
        }

        [Theory]
        [InlineData(95, 55)]
        [InlineData(65, 55)]
        [InlineData(50, 90)]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckIfGradeIsF(int score, int attPercentage)
        {
            GradingCalculator gradingCalculator = new();

            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attPercentage;
            Assert.Equal("F", gradingCalculator.GetGrade());
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(95, 65, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GetGrade_InputScoreAndAttendancePercentageAsInt_CheckAllGrades(int score, int attPercentage, string expectedResult)
        {
            GradingCalculator gradingCalculator = new();

            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attPercentage;
            Assert.Equal(expectedResult, gradingCalculator.GetGrade());
        }
    }
}
