using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface ILogger
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }
        void Message(string message);
        bool LogToDb(string message);
        bool LogBalanceAfterWithdraw(int balanceAfterWithdraw);
        string MessageWithStringReturn(string message);
        bool LogWithoutOutputResult(string message, out string outputMessage);
        bool LogWithRefObj(ref Customer customer);
    }

    public class Logger : ILogger
    {
        public int LogSeverity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LogType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool LogBalanceAfterWithdraw(int balanceAfterWithdraw)
        {
            var message = balanceAfterWithdraw > 0 ? "Success" : "Failure";
            Console.WriteLine(message);

            return balanceAfterWithdraw > 0;
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogWithoutOutputResult(string message, out string outputMessage)
        {
            outputMessage = $"Hello {message}";
            return true;
        }

        public bool LogWithRefObj(ref Customer customer)
        {
            return true;
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public string MessageWithStringReturn(string message)
        {
            Console.WriteLine(message);
            return message;
        }
    }
}
