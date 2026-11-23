using System;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Base custom exception for all banking operations and business logic violations.
    /// Captures the affected Account and attempted transaction amount for audit logging.
    /// </summary>
    public class BankingException : Exception
    {
        /// <summary>
        /// The specific account on which the failed operation was attempted, if available.
        /// </summary>
        public Account? Account { get; }

        /// <summary>
        /// The monetary amount attempted during the failed transaction.
        /// </summary>
        public decimal AttemptedAmount { get; }

        /// <summary>
        /// Initializes a new instance of the BankingException with an explanation message.
        /// </summary>
        public BankingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BankingException with an explanation message, target account, and attempted amount.
        /// </summary>
        public BankingException(string message, Account? account, decimal attemptedAmount) : base(message)
        {
            Account = account;
            AttemptedAmount = attemptedAmount;
        }

        /// <summary>
        /// Initializes a new instance with an explanation message and underlying inner exception.
        /// </summary>
        public BankingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Custom exception triggered when a withdrawal or transfer debit exceeds available funds or approved overdraft limits.
    /// Provides tailored, account-specific explanations for users.
    /// </summary>
    public class InsufficientFundsException : BankingException
    {
        /// <summary>
        /// Initializes a new instance of InsufficientFundsException with a message.
        /// </summary>
        public InsufficientFundsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance with message, target account, and attempted amount.
        /// </summary>
        public InsufficientFundsException(string message, Account? account, decimal attemptedAmount)
            : base(message, account, attemptedAmount)
        {
        }
    }
}
