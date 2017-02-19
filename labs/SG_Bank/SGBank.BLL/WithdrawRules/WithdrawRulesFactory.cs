using System;
using SGBank.Models;
using SGBank.Models.Interfaces;

namespace SGBank.BLL.WithdrawRules
{
    class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Free:
                    return new FreeAccountWithdrawRule();
                case AccountType.Basic:
                    return new BasicAccountWithdrawRule();
                case AccountType.Premium:
                    return new PremiumAccountWithdrawRule();
                default:
                    throw new Exception("Account type is not supported!");
            }

            
        }
    }
}
