using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Clients
{
    public interface IClient
    {
        void OpenSavingAccount(long accountNumber, double accountMaintenance, double saldo = 0, double minSaldo = 0, double bankInterest = 0,
            int transactionLimit = 0)
        void TakeLoan(double interestRate, int loanTerm, double principal, double insuranceAndFess, string repaymentTerms);
        void CloseLoan();
        void OpenOnlineAccount(string userName, string password);
        void CloseOnlineAccount();
    }
}
