using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private readonly string _filePath = ConfigurationManager.AppSettings["FilePath"];
        private string _line;
        private string _type;
        private string[] _columns;
        private Account _account;
        private List<Account> _accounts = new List<Account>();

        private List<Account> ReadAccountsFromFlatFile()
        {
            using (StreamReader stream = new StreamReader(_filePath))
            {
                
                if (_accounts.Any()) _accounts.Clear();

                while (!stream.EndOfStream)
                {
                    _line = stream.ReadLine();
                    if (_line == null) throw new Exception("File Read Error: File does not contain any data.");
                    if (_line.StartsWith("AccountNumber")) continue;

                    _account = new Account();
                    _columns = _line.Split(',');
                    _account.AccountNumber = _columns[0];
                    _account.Name = string.Join(" ", _columns[1], _columns[2]);
                    _account.Balance = decimal.Parse(_columns[3]);
                    _type = _columns[4];

                    if      (_type == "P") _account.Type = AccountType.Premium;
                    else if (_type == "B") _account.Type = AccountType.Basic;
                    else if (_type == "F") _account.Type = AccountType.Free;
                    else throw new Exception($"File Read Error: Invalid account type ({_type}) listed in account file.");

                    _accounts.Add(_account);
                }
            }
            return _accounts;
        }

        private void WriteAccountsToFlatFile()
        {
            using (StreamWriter stream = new StreamWriter(_filePath, false))
            {
                if (!_accounts.Any())
                {
                    throw new Exception("File Write Error: Account list is empty -- no data exists to write to file.");
                }

                stream.WriteLine("AccountNumber,FirstName,LastName,Balance,Type");

                foreach (var account in _accounts)
                {
                    string[] name = account.Name.Split(' ');

                    if      (account.Type == AccountType.Premium) _type = "P";
                    else if (account.Type == AccountType.Basic) _type = "B";
                    else if (account.Type == AccountType.Free) _type = "F";
                    else throw new Exception($"File Write Error: Account #{account.AccountNumber} has an unsupported type.");

                    stream.WriteLine($"{account.AccountNumber},{name[0]},{name[1]},{account.Balance},{_type}");
                }
            }
        }

        public Account LoadAccount(string accountNumber)
        {
            _accounts = ReadAccountsFromFlatFile();
            return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void SaveAccount(Account account)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (account.AccountNumber == _accounts[i].AccountNumber)
                {
                    _accounts[i] = account;
                }
            }
            WriteAccountsToFlatFile();
        }
    }
}
