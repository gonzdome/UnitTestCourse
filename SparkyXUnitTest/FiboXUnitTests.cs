using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class FiboXUnitTests
    {
        private Fibo fibo;

        public FiboXUnitTests ()
        {
            fibo = new();
        }

        [Fact]
        public void GetFiboSeries_InputInt1_GetCorrectOutputs()
        {
            fibo.Range = 1;

            var fiboSeries = fibo.GetFiboSeries();

            Assert.NotEmpty(fibo.GetFiboSeries());
            Assert.Equal(fibo.GetFiboSeries().OrderBy(u => u), fiboSeries);
            Assert.Equal(new List<int>() { 0 }, fibo.GetFiboSeries());
        }

        [Fact]
        public void GetFiboSeries_InputInt6_GetCorrectOutputs()
        {
            fibo.Range = 6;

            var fiboSeries = fibo.GetFiboSeries();

            Assert.Contains(3, fiboSeries);
            Assert.Equal(6, fiboSeries.Count);
            Assert.DoesNotContain(4, fiboSeries);
            Assert.Equal(new List<int>() { 0, 1, 1, 2, 3, 5 }, fiboSeries);
        }
    }
}
