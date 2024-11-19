using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        public int balance { get; set; }
        private readonly ILogger _logger;

        public BankAccount(ILogger logger)
        {
            balance = 0;
            _logger = logger;
        }

        public bool Deposit (int amount)
        {
            _logger.Message("Method call: Deposit");
            _logger.Message("Method Test");
            _logger.LogSeverity = 101;

            var temp = _logger.LogSeverity;

            balance += amount;
            return true; 
        }

        public bool Withdraw(int amount)
        {
            if (amount <= balance)
            {
                _logger.LogToDb($"Withdraw amount: {amount.ToString()}");
                balance -= amount;

                return _logger.LogBalanceAfterWithdraw(balance);
            }

            return _logger.LogBalanceAfterWithdraw(balance - amount);
        }

        public int GetBalance()
        {
           return balance;
        }
    }
}
