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
    public class FiboNUnitTests
    {
        public Fibo fibo;

        [SetUp]
        public void Setup()
        {
            fibo = new();
        }

        [Test]
        public void GetFiboSeries_InputInt1_GetCorrectOutputs()
        {
            fibo.Range = 1;

            ClassicAssert.That(fibo.GetFiboSeries(), Is.Not.Empty);
            ClassicAssert.That(fibo.GetFiboSeries(), Is.Ordered);
            ClassicAssert.That(fibo.GetFiboSeries(), Is.EqualTo(new List<int>() { 0 }));
        }

        [Test]
        public void GetFiboSeries_InputInt6_GetCorrectOutputs()
        {
            fibo.Range = 6;

            ClassicAssert.That(fibo.GetFiboSeries(), Has.Member(3));
            ClassicAssert.That(fibo.GetFiboSeries().Count, Is.EqualTo(6));
            ClassicAssert.That(fibo.GetFiboSeries(), Has.No.Member("4"));
            ClassicAssert.That(fibo.GetFiboSeries(), Is.EqualTo(new List<int>() { 0, 1, 1, 2, 3, 5 }));
        }
    }
}
