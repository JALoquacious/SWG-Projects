using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    public class PremiumAccountTests
    {
        [TestCase("99999", "Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, -100, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, 2500, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance,
            AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("99999", "Premium Account", 1500, AccountType.Premium, -2500, 1500, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Basic, -100, 100, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, 100, 100, false)]
        [TestCase("99999", "Premium Account", 150, AccountType.Premium, -50, 100, true)]
        [TestCase("99999", "Premium Account", 1000, AccountType.Premium, -1490, -500, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance,
            AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdrawal = new PremiumAccountWithdrawRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse response = withdrawal.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);

            if (response.Success)
            {
                Assert.AreEqual(newBalance, response.Account.Balance);
            }
        }
    }
}