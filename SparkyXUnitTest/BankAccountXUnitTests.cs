using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    public class BankAccountXUnitTests
    {
        public BankAccount account { get; set; }

        [Fact]
        public void DepositWithMOQ_Input100_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            account = new(logMock.Object);

            var result = account.Deposit(100);
            Assert.True(result);
            Assert.Equal(100, account.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(150, 100)]
        public void BankWithdraw_WithdrawLowerThanBalance_ReturnTrue(int balance, int withdraw)
        {
            var logMock = new Mock<ILogger>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x > 0))).Returns(true);

            account = new(logMock.Object);
            account.Deposit(balance);

            var result = account.Withdraw(withdraw);
            Assert.True(result);
        }

        [Theory]
        [InlineData(100, 200)]
        [InlineData(100, 300)]
        public void BankWithdraw_WithdrawBiggerThanBalance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogger>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
            //logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x < 0))).Returns(false);

            account = new(logMock.Object);
            account.Deposit(balance);

            var result = account.Withdraw(withdraw);
            Assert.False(result);
        }

        [Fact]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            string desiredOutput = "Hello";

            logMock.Setup(u => u.MessageWithStringReturn(It.IsAny<string>())) // When the input is a string in this method
                   .Returns((string str) => str); // It should return a string

            Assert.Equal(logMock.Object.MessageWithStringReturn("Hello"), desiredOutput);
        }

        [Fact]
        public void BankLogDummy_LogMockStringOutputMessage_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            string desiredOutput = "Hello";

            logMock.Setup(u => u.LogWithoutOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";

            Assert.True(logMock.Object.LogWithoutOutputResult("Ben", out result));
            Assert.Equal(desiredOutput, result);

        }

        [Fact]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            Customer customer = new Customer();
            Customer customerNotUsed = new Customer(); // If this is used, It'll return false;

            logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);
            Assert.True(logMock.Object.LogWithRefObj(ref customer));
        }

        [Fact]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogger>();
            //logMock.SetupAllProperties(); If this is called here the "logMock.Object.LogSeverity = 100;" will not work (remove comments to test);

            logMock.Setup(u => u.LogSeverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("Warning");

            //logMock.SetupAllProperties(); If this is called here the next line will work (remove comments to test);
            //logMock.Object.LogSeverity = 100 (remove comments to test);

            Assert.Equal(10, logMock.Object.LogSeverity);
            Assert.Equal("Warning" ,logMock.Object.LogType);

            #region Callbacks

            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                   .Returns(true).Callback((string str) => logTemp += str);
            logMock.Object.LogToDb("Ben");
            Assert.Equal("Hello, Ben", logTemp);

            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                   .Returns(true).Callback(() => counter++);
            logMock.Object.LogToDb("Ben");
            Assert.Equal(6, counter);

            #endregion
        }

        [Fact]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogger>();
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(100);

            Assert.Equal(100, bankAccount.GetBalance());

            #region Verification

            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Method Test"), Times.AtLeastOnce);

            logMock.VerifySet(u => u.LogSeverity = 101, Times.Once());
            logMock.VerifyGet(u => u.LogSeverity, Times.Once());

            #endregion
        }
    }
}
