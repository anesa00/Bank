﻿using Bank.Accounts;
using Bank.Cards;
using Bank.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Clients
{
    public class IndvidualClient : AbstractClient, IClient
    {
        private string _JMBG;
        private DateTime _birthDate;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string PhonoNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        private Loan _loan;
        private void CheckIndex(int index)
        {
            if (index == -1)
                throw new ArgumentException("There is no account with this account number!");
        }
        private IndvidualClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, string email = "")
        {
            Name = name;
            Surname = surname;
            Age = age;
            Adress = adress;
            _JMBG = JMBG;
            _birthDate = birthDate;
            PhonoNumber = phonoNumber;
            Email = email;
        }
        public IndvidualClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, long accountNumber,
            double accountMaintenance, string email = "", double saldo = 0, double limit = 0)
            : this(name, surname, birthDate, age, JMBG, adress, phonoNumber, email)
        {
            OpenCurrentAccount(accountNumber, accountMaintenance, saldo, limit);
        }
        public IndvidualClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, long accountNumber, 
            double accountMaintenance, string email = "", double saldo = 0, double minSaldo = 0, double bankInterest = 0, int transactionLimit = 0) 
            : this(name, surname, birthDate, age, JMBG, adress, phonoNumber, email)
        {
            OpenSavingAccount(accountNumber, accountMaintenance, saldo, minSaldo, bankInterest, transactionLimit);
        }
        public void OpenCurrentAccount(long accountNumber, double accountMaintenance, double saldo = 0, double limit = 0)
        {
            Accounts.Add(new CurrentAccount(accountNumber, accountMaintenance, saldo, limit));
        }
        public void OpenSavingAccount(long accountNumber, double accountMaintenance, double saldo = 0, double minSaldo = 0, double bankInterest = 0, 
            int transactionLimit = 0)
        {
            Accounts.Add(new SavingAccount(accountNumber, accountMaintenance, saldo, minSaldo, bankInterest, transactionLimit));
        }
        public override void CloseAccount(long accountNumber)
        {
            var index = Accounts.FindIndex(account => account.GetAccountNumber() == accountNumber);
            try
            {
                CheckIndex(index);
            }
            catch (ArgumentException e)
            {

                throw;
            }
            
            Accounts.RemoveAt(index);
        }
        public override void GetCard(CardType card, long accountNumber, long cardNumber, int pin, int cvv, DateOnly cardExpiry)
        {
            var account = Accounts.Find(a => a.GetAccountNumber() == accountNumber);
            Cards.Add(new Card(account, cardNumber, card, pin, cvv, cardExpiry));
        }
        public override void CloseCard(long cardNumber)
        {
            var index = Cards.FindIndex(card => card.CardNumber == cardNumber);
            try
            {
                CheckIndex(index);
            }
            catch (ArgumentException e)
            {

                throw;
            }

            Cards.RemoveAt(index);
        }
        public void OpenOnlineAccount(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public void CloseOnlineAccount()
        {
            UserName = "";
            Password = "";
        }
        public void TakeLoan(double interestRate, int loanTerm, double principal, double insuranceAndFess, string repaymentTerms)
        {
            if (_loan != null)
                throw new Exception("You have already taken the laon!");
            _loan = new Loan(interestRate * principal, interestRate, loanTerm, principal, insuranceAndFess, repaymentTerms);
        }
        public void CloseLoan()
        {
            if (_loan == null)
                throw new Exception("You don't have any loan!");
            _loan = null;
        }
    }
}
