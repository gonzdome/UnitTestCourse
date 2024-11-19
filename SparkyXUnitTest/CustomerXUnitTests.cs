using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class CustomerXUniTests
    {
        private Customer customer;

        public CustomerXUniTests() 
        { 
            customer = new();
        }

        [Fact]
        public void GreetAndCombineName_TwoStringParams_ReturnFullName()
        {
            var result = customer.GreetAndCombineName("João", "Silva");
            Assert.Multiple(() =>
            {
                Assert.Equal("Hello, João Silva!", customer.GreetMessage);
                Assert.Contains("joão silva", customer.GreetMessage?.ToLower());
                Assert.StartsWith("Hello", customer.GreetMessage);
                Assert.EndsWith("Silva!", customer.GreetMessage);
                Assert.Matches("Hello, [A-Z\u00C0-\u00FF]{1}[a-z\u00C0-\u00FF]+ [A-Z\u00C0-\u00FF]{1}[a-z\u00C0-\u00FF]+!", customer.GreetMessage);
            });
        }

        [Fact]
        public void GreetAndCombineName_TwoStringParams_CheckIfIsNull()
        {
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
        {
            Assert.InRange(customer.Discount, 15, 25);
        }

        [Fact]
        public void GreetAndCombineName_InputStringName_ReturnsNotNull()
        {
            var result = customer.GreetAndCombineName("João");

            Assert.NotNull(customer.GreetMessage);
            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetAndCombineName_InputStringName_ThrowsException()
        {
            var result = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineName(""));

            Assert.Equal("Name is required!", result?.Message);
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineName(""));
        }


        [Fact]
        public void GreetAndCombine_GreetCustomerWithLessThan100Orders_ReturnBasicCustomer ()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.IsType<BasicCustomer>(result);
        }
    }
}
