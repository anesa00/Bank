

using Bank.Accounts;
using Bank.Cards;
using Bank.Loans;

namespace Bank.Clients
{
    public class Owner
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
        public Owner(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, string email = "")
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
    }
    public class LegalEntityClient: AbstractClient, IClient
    {
        public Owner Owner { get; set; }
        private string _id;
        public string Adress { get; set; }
        private Loan _loan;
        private void CheckIndex(int index)
        {
            if (index == -1)
                throw new ArgumentException("There is no account with this account number!");
        }
        private LegalEntityClient(string id, string adressOfCompany, string name, string surname, DateTime birthDate, int age, string JMBG, string adress,
            string phonoNumber, string email = "")
        {
            _id = id;
            Adress = adressOfCompany;
            Owner = new Owner(name, surname, birthDate, age, JMBG, adress, phonoNumber, email);
        }
        public LegalEntityClient(string id, string adressOfCompany, string name, string surname, DateTime birthDate, int age, string JMBG, string adress,
            double accountMaintenance, long accountNumber, string phonoNumber, string email = "", double saldo = 0, double minSaldo = 0, 
            double bankInterest = 0, int transactionLimit = 0)
            : this(id, adressOfCompany, name, surname, birthDate, age, JMBG, adress, phonoNumber, email)
        {
            OpenSavingAccount(accountNumber, accountMaintenance, saldo, minSaldo, bankInterest, transactionLimit);
        }
        public LegalEntityClient(string id, string adressOfCompany, string name, string surname, DateTime birthDate, int age, string JMBG, string adress,
            double accountMaintenance, long accountNumber, string phonoNumber, string email = "", double saldo = 0, string accountCurrency = "BAM", 
            int dailyTransactionLimit = 0, int monthlyTransactionLimit = 0, int limit = 0)
            : this(id, adressOfCompany, name, surname, birthDate, age, JMBG, adress, phonoNumber, email)
        {
            OpenBusinessAccount(accountNumber, accountMaintenance, saldo, accountCurrency, dailyTransactionLimit, monthlyTransactionLimit, limit);
        }
        public void OpenSavingAccount(long accountNumber, double accountMaintenance, double saldo = 0, double minSaldo = 0, double bankInterest = 0,
            int transactionLimit = 0)
        {
            Accounts.Add(new SavingAccount(accountNumber, accountMaintenance, saldo, minSaldo, bankInterest, transactionLimit));
        }

        public void OpenBusinessAccount(long accountNumber, double accountMaintenance, double saldo = 0, string accountCurrency = "BAM", 
            int dailyTransactionLimit = 0, int monthlyTransactionLimit = 0, int limit = 0)
        {
            Accounts.Add(new BusinessAccount(accountNumber, accountMaintenance, saldo, accountCurrency, dailyTransactionLimit, monthlyTransactionLimit, limit));
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
            Owner.UserName = userName;
            Owner.Password = password;
        }
        public void CloseOnlineAccount()
        {
            Owner.UserName = "";
            Owner.Password = "";
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
