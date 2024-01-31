using Bank.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Accounts
{
	public class CurrentAccount: AbstractAccount, IAccount
	{
		public double Limit { get; set; }
		private void CheckingAmount(double amount)
		{
			if (amount < 0)
				throw new ArgumentOutOfRangeException("The amount can't be a negative number!");
		}
		public CurrentAccount()
		{
			_transactions = new List<Transaction>();
		}
		public CurrentAccount(int accountNumber, double accountMaintenance, double saldo = 0) 
			: this()
		{
			_accountNumber = accountNumber;
			_saldo = saldo;
			AccountMaintenance = accountMaintenance;
		}
		public override int GetAccountNumber()
		{
			return _accountNumber;
		}
		public override double GetSaldo()
		{
			return _saldo;
		}
		public override string BankStatment()
		{
			var statements = "";

			foreach(var transaction in _transactions)
			{
				statements += transaction.TransactionStatement + "\n";
			}

			return statements;
		}
		public override string MonthStatment(int month)
		{
			var monthTransactions = _transactions.Where(transaction => transaction.GetDateTime().Month == month).ToList();

			var statements = "";

			foreach (var transaction in monthTransactions)
			{
				statements += transaction.TransactionStatement + "\n";
			}

			return statements;
		}
		public void MakeATransaction(int accountNumber, double amount, double services, string description = "")
		{
			try
			{
				CheckingAmount(amount);
			}
			catch (ArgumentOutOfRangeException e)
			{

				throw;
			}

			if (amount + services > _saldo + Limit)
				throw new ArgumentOutOfRangeException("You don't have anought money on your account!");

			_saldo -= amount + services;
			if (_saldo < 0)
			{
				Limit += _saldo;
				_saldo = 0;
			}

			_transactions.Add(new Transaction(new DateTime(), description, amount, accountNumber, services));
		}
		public void FundsWithdrawal(double amount)
		{
			try
			{
				CheckingAmount(amount);
			}
			catch (ArgumentOutOfRangeException e)
			{

				throw;
			}

			if (amount > _saldo + Limit)
				throw new ArgumentOutOfRangeException("You don't have anought money on your account!");

			_saldo -= amount;
			if (_saldo < 0)
			{
				Limit += _saldo;
				_saldo = 0;
			}

			_transactions.Add(new Transaction(new DateTime(), "You have withdrawn " + amount + "BAM from your account.", amount));
		}
		public void FundsDeposit(double amount)
		{
			try
			{
				CheckingAmount(amount);
			}
			catch (ArgumentOutOfRangeException e)
			{

				throw;
			}

			_saldo += amount;
			_transactions.Add(new Transaction(new DateTime(), "You have deposited " + amount + "BAM into your account.", amount));
		}
	}
}
