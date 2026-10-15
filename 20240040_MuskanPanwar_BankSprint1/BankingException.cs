using System;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Base custom exception for all banking operations and business logic violations.
    /// </summary>
    public class BankingException : Exception
    {
        public Account Account { get; }
        public decimal AttemptedAmount { get; }

        public BankingException(string message) : base(message)
        {
        }

        public BankingException(string message, Account account, decimal attemptedAmount) : base(message)
        {
            Account = account;
            AttemptedAmount = attemptedAmount;
        }

        public BankingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Custom exception triggered when a withdrawal exceeds available funds or overdraft limits.
    /// Provides tailored, account-specific messages.
    /// </summary>
    public class InsufficientFundsException : BankingException
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }

        public InsufficientFundsException(string message, Account account, decimal attemptedAmount)
            : base(message, account, attemptedAmount)
        {
        }
    }
}
