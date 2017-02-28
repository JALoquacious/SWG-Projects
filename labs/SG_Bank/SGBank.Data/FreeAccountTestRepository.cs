using SGBank.Models.Interfaces;
using SGBank.Models;

namespace SGBank.Data
{
    public class FreeAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Free Account",
            Balance = 100.00M,
            AccountNumber = "12345",
            Type = AccountType.Free
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