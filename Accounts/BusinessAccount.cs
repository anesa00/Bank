using Bank.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Accounts
{
    public class BusinessAccount: AbstractAccount, IAccount
    {
        public int DailyTransactionCounter { get; set; }
        public int MonthlyTransactionCounter { get; set; }
        public string AccountCurrency { get; set; }
        public int DailyTransactionLimit { get; set; }
        public int MonthlyTransactionLimit { get; set; }
        public int Limit { get; set; }
        private void CheckingAmount(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The amount can't be a negative number!");
        }
        private void CheckingTransactionLimits()
        {
            if (DailyTransactionCounter > DailyTransactionLimit )
                throw new ArgumentOutOfRangeException("You can no longer perform the transaction; you have reached the daily limit!");
            if (MonthlyTransactionCounter > MonthlyTransactionLimit)
                throw new ArgumentOutOfRangeException("You can no longer perform the transaction; you have reached the monthly limit!");
        }
        private void IncreaseCounters()
        {
            DailyTransactionCounter++;
            MonthlyTransactionCounter++;
        }
        public BusinessAccount() 
        {
            _transactions = new List<Transaction>();
            DailyTranscationCounter = 0;
            MonthlyTransactionCounter= 0;
        }
        public BusinessAccount(int accountNumber, double accountMaintenance, double saldo = 0, string accountCurrency = "BAM", int dailyTransactionLimit = 0, 
            int monthlyTransactionLimit = 0, int limit = 0)
            : this()
        {
            _accountNumber = accountNumber;
            _saldo = saldo;
            AccountMaintenance = accountMaintenance;
            AccountCurrency = accountCurrency;
            DailyTransactionLimit = dailyTransactionLimit;
            MonthlyTransactionLimit = monthlyTransactionLimit;
            Limit = limit;
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
        public void MakeATransaction(int accountNumber, double amount, double services, string description = "")
        {
            try
            {
                CheckingAmount(amount);
                CheckingTransactionLimits();
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            if (amount + services > _saldo + limit)
                throw new ArgumentOutOfRangeException("You don't have enought money on your account!");

            _saldo -= amount + services;
            if (_saldo < 0)
            {
                Limit += _saldo;
                _saldo = 0;
            }

            _transactions.Add(new Transaction(new DateTime(), description, amount, accountNumber, services));
            IncreaseCounters();
        }
        public void FundsWithdrawal(double amount)
        {
            try
            {
                CheckingAmount(amount);
                CheckingTransactionLimits();
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
            IncreaseCounters();

        }
        public void FundsDeposit(double amount)
        {
            try
            {
                CheckingAmount(amount);
                CheckingTransactionLimits();
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            _saldo += amount;
            _transactions.Add(new Transaction(new DateTime(), "You have deposited " + amount + "BAM into your account.", amount));
            IncreaseCounters();
        }
    }
}
