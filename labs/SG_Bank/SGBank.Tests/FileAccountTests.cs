using System.Configuration;
using NUnit.Framework;
using System.IO;
using SGBank.BLL;
using SGBank.Data;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    class FileAccountTests
    {
        [SetUp]
        public void RecreateFile()
        {
            string filePath = ConfigurationManager.AppSettings["FilePath"];
            string backupPath = ConfigurationManager.AppSettings["BackupPath"];

            File.Copy(backupPath, filePath, true);
        }

        [Test]
        public void CanLoadAccountFromFile()
        {
            IAccountRepository repo = new FileAccountRepository();
            Account account = repo.LoadAccount("1");

            Assert.AreEqual("1", account.AccountNumber);
            Assert.AreEqual("Michael Jackson", account.Name);
            Assert.AreEqual(150.00M, account.Balance);
            Assert.AreEqual(AccountType.Basic, account.Type);
        }

        [TestCase("1", -100, false)]
        [TestCase("2", 2250, false)]
        [TestCase("3", 100, true)]
        [TestCase("4", 50, true)]
        public void CanDepositToAccountInFile(string accountNumber, decimal depositAmount, bool expectedResult)
        {
            IAccountRepository repo = new FileAccountRepository();
            Account account = repo.LoadAccount(accountNumber);
            AccountManager accountManager = new AccountManager(repo);
            AccountDepositResponse response = accountManager.Deposit(account.AccountNumber, depositAmount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("1", -100)]
        [TestCase("2", -250)]
        [TestCase("3", 0)]
        [TestCase("4", 0)]
        public void NegativeOrZeroAmountDepositsFail(string accountNumber, decimal amount)
        {
            IAccountRepository repo = new FileAccountRepository();
            Account account = repo.LoadAccount(accountNumber);
            AccountManager accountManager = new AccountManager(repo);
            AccountDepositResponse response = accountManager.Deposit(account.AccountNumber, amount);

            Assert.IsFalse(response.Success);
        }

        [TestCase("1", 100, false)]
        [TestCase("2", 2250, false)]
        [TestCase("3", -100, true)]
        [TestCase("4", -50, true)]
        public void CanWithdrawFromAccountInFile(string accountNumber, decimal depositAmount, bool expectedResult)
        {
            IAccountRepository repo = new FileAccountRepository();
            Account account = repo.LoadAccount(accountNumber);
            AccountManager accountManager = new AccountManager(repo);
            AccountWithdrawResponse response = accountManager.Withdraw(account.AccountNumber, depositAmount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("3", 100)]
        [TestCase("4", 250)]
        [TestCase("5", 0)]
        [TestCase("6", 0)]
        public void PositiveOrZeroAmountWithdrawalsFail(string accountNumber, decimal amount)
        {
            IAccountRepository repo = new FileAccountRepository();
            Account account = repo.LoadAccount(accountNumber);
            AccountManager accountManager = new AccountManager(repo);
            AccountWithdrawResponse response = accountManager.Withdraw(account.AccountNumber, amount);

            Assert.IsFalse(response.Success);
        }
    }
}
