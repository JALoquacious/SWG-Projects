using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.Data
{
    public class BasicAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Basic Account",
            Balance = 100.00M,
            AccountNumber = "33333",
            Type = AccountType.Basic
        };

        public Account LoadAccount(string accountNumber)
        {
            return (_account.AccountNumber == accountNumber) ? _account : null;
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}