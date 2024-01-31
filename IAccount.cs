using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
	internal interface IAccount
	{
		void MakeATransaction(int accountNumber, double amount);
		void FundsWithdrawal(double amount);
		void FundsDeposit(double amount);
	}
}
