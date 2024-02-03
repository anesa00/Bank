

namespace Bank.Transactions
{
	public class Transaction
	{
		DateTime _date;
		string _description, _instrument;
		long _accountNumber;
		int _quantity;
		double _amount, _bankingServices;

		public Transaction(DateTime date, string description, double amount)
		{
			_date = date;
			_description = description;
			_amount = amount;
		}

		public Transaction(DateTime date, string description, double amount, long accountNumber, double services = 0)
			: this(date, description, amount)
		{
			_accountNumber = accountNumber;
			_bankingServices = services;
		}

		public Transaction(DateTime date, string description, double amount, long accountNumber, string instrument, int quantity, double services = 0)
			: this(date, description, amount)
		{
			_accountNumber = accountNumber;
			_instrument = instrument;
			_quantity = quantity;
			_bankingServices = services;
		}
		public DateTime GetDateTime() 
		{ 
			return _date; 
		}
		public string GetDescription()
		{
			return _description;
		}
		public double GetAmount()
		{
			return _amount;
		}
		public long GetAccountNumber()
		{
			return _accountNumber;
		}
		public string GetInstrument()
		{
			return _instrument;
		}
		public int GetQuantity()
		{
			return _quantity;
		}
		public string TransactionStatement()
		{

			if (_accountNumber != 0)
				return "Date: " + _date + "\nDescription: " + _description + "\nAmount: " + _amount + "\nServices provided by the bank" + _bankingServices +
					"\nAccount Number: " + _accountNumber;

			if (_instrument != null && _quantity != 0)
				return "Date: " + _date + "\nDescription: " + _description + "\nInstrument: " + _instrument + "\nQuantity: " + _quantity +
					"\nAmount: " + _amount + "\nServices provided by the bank" + _bankingServices + "\nAccount Number: " + _accountNumber;						

			return "Date: " + _date + "\nDescription: " + _description + "\nAmount: " + _amount;
		}
	}
}
