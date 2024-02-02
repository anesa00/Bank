using Bank.Transactions;

namespace Bank.Accounts
{
	public abstract class AbstractAccount()
	{
		protected long _accountNumber;
		protected double _saldo;
		protected List<Transaction> _transactions;
		public double AccountMaintenance { get; set; }
		public abstract long GetAccountNumber();
		public abstract double GetSaldo();
		public abstract List<Transaction> GetTransactions();
		public abstract string BankStatment();
		public abstract string MonthStatment(int month);
	}
}