using Bank.Transactions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Accounts
{
    public struct Instrument 
    {
        public string Type, Name;
        public double Price;
        public int Quantity;
    }
    public class BrokerageAccount: AbstractAccount
    {
        private List<Instrument> _portfolio;
        private double _totalPortfolioValue;
        private void CalculateTotalPortfolioValue()
        {
            _totalPortfolioValue = _portfolio.Sum(instrument => instrument.Price);
        }
        private void CheckingAmount(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The amount can't be a negative number!");
        }
        public BrokerageAccount()
        {
            _transaction = new List<Transaction>();
            _portoflio = new List<Instrument>();
            _totalPortfolioValue = 0;
        }
        public BrokerageAccount(int accountNumber, double accountMaintenance, double saldo = 0)
            : this()
        {
            _accountNumber = accountNumber;
            AccountMaintenance = accountMaintenance;
            _saldo = saldo;
        }
        public BrokerageAccount(int accountNumber, double accountMaintenance, double saldo, List<Instrument> portfolio)
            : this(accountNumber, accountMaintenance, saldo)
        {
            _portfolio = portfolio;
            CalculateTotalPortfolioValue();
        }
        public double GeTotalPortofolioValue()
        {
            return _totalPortfolioValue;
        }
        public List<Instrument> GetPortofolio()
        {
            return _portfolio;
        }
        public override double GetSaldo()
        {
            return _saldo;
        }
        public override int GetAccountNumber()
        {
            return _accountNumber;
        }
        public string SeePortfolio()
        {
            var instruments = "";

            foreach(var instrument in _portfolio)
            {
                instruments += "Type: " + instrument.Type + "\tName: " + instrument.Name + "\tPrice: " + instrument.Price + "\tQuantity: " + instrument.Quantity + "\n"; 
            }

            return instruments;
        }
        public override string BankStatment()
        {
            var statements = "";

            foreach (var transaction in _transactions)
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
        public void BuyInstrument(double amount, int accountNumber,string type, string instrument, int quantity, double services, string description = "")
        {
            try
            {
                CheckingAmount()
            }
            catch(ArgumentOutOfRangeException e)
            {
                throw;
            }

            if (quantity <= 0 )
                throw new ArgumentOutOfRangeException("The quantity can't be a negative number!");

            if (amount * quantity > _saldo)
                throw new ArgumentException("You don't have enought money on your account!");

            _saldo -= amount * quantity;
            var index = _portfolio.FindIndex(item => item.Name == instrument);
            if (index == -1)
                _portfolio.Add(new Instrument { Name = instrument, Type = type, Price = amount, Quantity = quantity });
            else
                _portfolio[index].Quantity += quantity;

            _transactions.Add(new Transaction(new DateTime(), description, amount, accountNumber, instrument, quantity, services))
        }
        public void FundsDeposit(double amount)
        {
            try
            {
                CheckingAmount()
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
