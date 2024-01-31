using System;
using System.Runtime.InteropServices;

namespace Bank.Transactions
{
	public class Transaction
	{
		DateTime _date;
		string _description, _currency, _instrument;
		int _accountNumber, _quantity;
		double _amount, _bankingServices;
		bool _successful;

		public Transaction(DateTime date, string description, double amount)
		{
			this._date = date;
			this._description = description;
			this._amount = amount;
			_successful = true;
		}

		public Transaction(DateTime date, string description, int accountNumber, double amount, double services)
			: this (date, description, amount)
		{
			this._accountNumber = accountNumber;
			this._bankingServices = services;
		} 

		public Transaction(DateTime date, string description, double amount, string currency)
			: this(date, description, amount)
		{
			this._currency = currency;
		}

		public Transaction(DateTime date, string description, double amount, int accountNumber, string currency, double services)
			: this(date, description, amount)
		{
			this._accountNumber = accountNumber;
			this._currency = currency;
			this._bankingServices = services;
		}

		public Transaction(DateTime date, string description, double amount, int accountNumber, string instrument, int quantity, double services)
			: this(date, description, amount)
		{
			this._accountNumber = accountNumber;
			this._instrument = instrument;
			this._quantity = quantity;
			this._bankingServices = services;
		}
		public bool Successful { get; set; }
		public string TransactionStatement()
		{
			var status = _successful ? "The transaction has been successful." : "The transaction has been failed.";

			if (_accountNumber != 0 && _currency != null)
				return "Date: " + _date + "\nDescription: " + _description + "\nAmount: " + _amount + "\nServices provided by the bank" + _bankingServices +
					"\nAccount Number: " + _accountNumber + "\nCurrency: " + _currency + "\nStatus: " + status;

			if (_instrument != null && _quantity != 0)
				return "Date: " + _date + "\nDescription: " + _description + "\nInstrument: " + _instrument + "\nQuantity: " + _quantity + 
					"\nAmount: " + _amount + "\nServices provided by the bank" + _bankingServices + "\nAccount Number: " + _accountNumber + 
					"\nStatus: " + status;

			if (_accountNumber == 0)
				return "Date: " + _date + "\nDescription: " + _description + "\nAmount: " + _amount + "\nServices provided by the bank" + _bankingServices +
						"\nCurrency: " + _currency + "\nStatus: " + status;

			if(_currency  == null)
				return "Date: " + _date + "\nDescription: " + _description + "\nAmount: " + _amount + "\nServices provided by the bank" + _bankingServices +
					"\nAccount Number: " + _accountNumber + "\nStatus: " + status;	

			return "Date: " + _date + "\nDescription: " + _description + "\nAmount: " + _amount + "\nStatus: " + status;
		}
	}
}
