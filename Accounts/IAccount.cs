using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Accounts
{
    internal interface IAccount
    {
        void MakeATransaction(int accountNumber, double amount, double services, string description = "");
        void FundsWithdrawal(double amount);
        void FundsDeposit(double amount);
    }
}
