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
    public class CustomerNUnitTests
    {
        private Customer customer;

        [SetUp]
        public void SetUp() 
        { 
            customer = new();
        }

        [Test]
        public void GreetAndCombineName_TwoStringParams_ReturnFullName()
        {
            var result = customer.GreetAndCombineName("João", "Silva");
            Assert.Multiple(() =>
            {
                ClassicAssert.That(customer.GreetMessage, Is.EqualTo("Hello, João Silva!"));
                ClassicAssert.That(customer.GreetMessage, Does.Contain("joão silva").IgnoreCase);
                ClassicAssert.That(customer.GreetMessage, Does.StartWith("Hello"));
                ClassicAssert.That(customer.GreetMessage, Does.EndWith("Silva!"));
                ClassicAssert.That(customer.GreetMessage, Does.Match("Hello, [A-Z\u00C0-\u00FF]{1}[a-z\u00C0-\u00FF]+ [A-Z\u00C0-\u00FF]{1}[a-z\u00C0-\u00FF]+!"));
            });
        }

        [Test]
        public void GreetAndCombineName_TwoStringParams_CheckIfIsNull()
        {
            ClassicAssert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            ClassicAssert.That(customer.Discount, Is.InRange(15, 25));
        }

        [Test]
        public void GreetAndCombineName_InputStringName_ReturnsNotNull()
        {
            var result = customer.GreetAndCombineName("João");

            ClassicAssert.IsNotNull(customer.GreetMessage);
            ClassicAssert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Test]
        public void GreetAndCombineName_InputStringName_ThrowsException()
        {
            var result = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineName(""));

            ClassicAssert.AreEqual("Name is required!", result?.Message);
            ClassicAssert.That(() => customer.GreetAndCombineName(""), Throws.ArgumentException.With.Message.EqualTo("Name is required!"));

            // Check if there is any exception
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineName(""));
            ClassicAssert.That(() => customer.GreetAndCombineName(""), Throws.ArgumentException);
        }

        [Test]
        public void GreetAndCombine_GreetCustomerWithLessThan100Orders_ReturnBasicCustomer ()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }
    }
}
