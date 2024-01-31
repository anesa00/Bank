using Bank.Transactions;

namespace Bank.Accounts
{
	public abstract class AbstractAccount()
	{
		private int _accountNumber;
		private double _saldo;
		private List<Transaction> _transactions;
		public double AccountMaintenance { get; set; }
		public abstract int GetAccountNumber();
		public abstract double GetSaldo();
		public abstract string BankStatment();
		public abstract string MonthStatment();
	}
}