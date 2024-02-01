using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Accounts
{
    internal interface IAccount
    {
        void MakeATransaction(int accountNumber, double amount, double services, string description = "", bool statement = true);
        void FundsWithdrawal(double amount, bool statement = true);
        void FundsDeposit(double amount, bool statement = true);
    }
}
