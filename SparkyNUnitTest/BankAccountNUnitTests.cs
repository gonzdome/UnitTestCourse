using Moq;
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
    public class BankAccountNUnitTests
    {
        public BankAccount account { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void DepositFakeLogger_Input100_ReturnTrue()
        //{
        //    account = new(new FakeLogger()); // If we use Logger It'll turn the test into Integration test, so we use a FakeLogger

        //    var result = account.Deposit(100);
        //    ClassicAssert.IsTrue(result);
        //    ClassicAssert.That(account.GetBalance, Is.EqualTo(100));
        //}

        [Test]
        public void DepositWithMOQ_Input100_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            account = new(logMock.Object);

            var result = account.Deposit(100);
            ClassicAssert.IsTrue(result);
            ClassicAssert.That(account.GetBalance, Is.EqualTo(100));
        }

        [Test]
        [TestCase(200, 100)]
        [TestCase(150, 100)]
        public void BankWithdraw_WithdrawLowerThanBalance_ReturnTrue(int balance, int withdraw)
        {
            var logMock = new Mock<ILogger>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x > 0))).Returns(true);

            account = new(logMock.Object);
            account.Deposit(balance);

            var result = account.Withdraw(withdraw);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        [TestCase(100, 200)]
        [TestCase(100, 300)]
        public void BankWithdraw_WithdrawBiggerThanBalance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogger>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdraw(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
            //logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x < 0))).Returns(false);

            account = new(logMock.Object);
            account.Deposit(balance);

            var result = account.Withdraw(withdraw);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            string desiredOutput = "Hello";

            logMock.Setup(u => u.MessageWithStringReturn(It.IsAny<string>())) // When the input is a string in this method
                   .Returns((string str) => str); // It should return a string

            ClassicAssert.That(logMock.Object.MessageWithStringReturn("Hello"), Is.EqualTo(desiredOutput));
        }

        [Test]
        public void BankLogDummy_LogMockStringOutputMessage_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            string desiredOutput = "Hello";

            logMock.Setup(u => u.LogWithoutOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";

            ClassicAssert.IsTrue(logMock.Object.LogWithoutOutputResult("Ben", out result));
            ClassicAssert.That(result, Is.EqualTo(desiredOutput));

        }

        [Test]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogger>();
            Customer customer = new Customer();
            Customer customerNotUsed = new Customer(); // If this is used, It'll return false;

            logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);
            ClassicAssert.IsTrue(logMock.Object.LogWithRefObj(ref customer));
        }

        [Test]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogger>();
            //logMock.SetupAllProperties(); If this is called here the "logMock.Object.LogSeverity = 100;" will not work (remove comments to test);

            logMock.Setup(u => u.LogSeverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("Warning");

            //logMock.SetupAllProperties(); If this is called here the next line will work (remove comments to test);
            //logMock.Object.LogSeverity = 100 (remove comments to test);

            ClassicAssert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            ClassicAssert.That(logMock.Object.LogType, Is.EqualTo("Warning"));

            #region Callbacks

            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                   .Returns(true).Callback((string str) => logTemp += str);
            logMock.Object.LogToDb("Ben");
            ClassicAssert.That(logTemp, Is.EqualTo("Hello, Ben"));

            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                   .Returns(true).Callback(() => counter++);
            logMock.Object.LogToDb("Ben");
            ClassicAssert.That(counter, Is.EqualTo(6));

            #endregion
        }

        [Test]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogger>();
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(100);

            ClassicAssert.That(bankAccount.GetBalance(), Is.EqualTo(100));

            #region Verification

            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Method Test"), Times.AtLeastOnce);

            logMock.VerifySet(u => u.LogSeverity = 101, Times.Once());
            logMock.VerifyGet(u => u.LogSeverity, Times.Once());

            #endregion
        }
    }
}
