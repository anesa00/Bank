using Bank.Transactions;

namespace Bank.Accounts
{
	public abstract class AbstractAccount()
	{
		protected int _accountNumber;
		protected double _saldo;
		protected List<Transaction> _transactions;
		public double AccountMaintenance { get; set; }
		public abstract int GetAccountNumber();
		public abstract double GetSaldo();
		public abstract string BankStatment();
		public abstract string MonthStatment();
	}
}