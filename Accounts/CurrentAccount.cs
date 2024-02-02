using Bank.Transactions;

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
		private void TransactionStatement(string statement)
		{
			Console.WriteLine(statement);
		}
		public CurrentAccount()
		{
			_transactions = new List<Transaction>();
		}
		public CurrentAccount(long accountNumber, double accountMaintenance, double saldo = 0, double limit = 0) 
			: this()
		{
			_accountNumber = accountNumber;
			_saldo = saldo;
			AccountMaintenance = accountMaintenance;
			Limit = limit;
		}
		public override long GetAccountNumber()
		{
			return _accountNumber;
		}
		public override double GetSaldo()
		{
			return _saldo;
		}
		public override List<Transaction> GetTransactions()
		{
			return _transactions;
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
		public void MakeATransaction(long accountNumber, double amount, double services, string description = "", bool statement = true)
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
				throw new ArgumentOutOfRangeException("You don't have enought money on your account!");

			_saldo -= amount + services;
			if (_saldo < 0)
			{
				Limit += _saldo;
				_saldo = 0;
			}

			_transactions.Add(new Transaction(new DateTime(), description, amount, accountNumber, services));

			if (statement)
				TransactionStatement(_transactions.Last().TransactionStatement());
		}
		public void FundsWithdrawal(double amount, bool statement = true)
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
				throw new ArgumentOutOfRangeException("You don't have enought money on your account!");

			_saldo -= amount;
			if (_saldo < 0)
			{
				Limit += _saldo;
				_saldo = 0;
			}

			_transactions.Add(new Transaction(new DateTime(), "You have withdrawn " + amount + "BAM from your account.", amount));

			if (statement)
				TransactionStatement(_transactions.Last().TransactionStatement());
		}
		public void FundsDeposit(double amount, bool statement = true)
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

			if (statement)
				TransactionStatement(_transactions.Last().TransactionStatement());
		}
		public override void ReceiveTransaction(Transaction transaction)
		{
			_transactions.Add(new Transaction(transaction.GetDateTime(),"Deposit: " + transaction.GetDescription(), transaction.GetAmount(),
				transaction.GetAccountNumber()));
			_saldo += transaction.GetAmount();
		}
	}
}
