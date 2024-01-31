using System;
using System.Runtime.InteropServices;

namespace Bank.Transactions
{
	public class Transaction
	{
		DateTime _date;
		string _description, _instrument;
		int _accountNumber, _quantity;
		double _amount, _bankingServices;

		public Transaction(DateTime date, string description, double amount)
		{
			_date = date;
			_description = description;
			_amount = amount;
		}

		public Transaction(DateTime date, string description, double amount, int accountNumber, double services)
			: this(date, description, amount)
		{
			_accountNumber = accountNumber;
			_bankingServices = services;
		}

		public Transaction(DateTime date, string description, double amount, int accountNumber, string instrument, int quantity, double services)
			: this(date, description, amount)
		{
			_accountNumber = accountNumber;
			_instrument = instrument;
			_quantity = quantity;
			_bankingServices = services;
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
