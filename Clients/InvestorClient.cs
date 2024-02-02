
using Bank.Accounts;
using Bank.Cards;

namespace Bank.Clients
{
    public class InvestorClient: AbstractClient
    {
        private string _JMBG;
        private DateTime _birthDate;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string PhonoNumber { get; set; }
        private void CheckIndex(int index)
        {
            if (index == -1)
                throw new ArgumentException("There is no account with this account number!");
        }
        private InvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, string email = "")
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
        public InvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, long accountNumber,
            double accountMaintenance, string email = "", double saldo = 0)
            :this(name, surname, birthDate, age, JMBG, adress, phonoNumber, email)
        {
            OpenAccount(accountNumber, accountMaintenance, saldo);
        }
        public InvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phonoNumber, long accountNumber,
            double accountMaintenance, double saldo, List<Instrument> portfolio, string email = "")
            : this(name, surname, birthDate, age, JMBG, adress, phonoNumber, email)
        {
            OpenAccount(accountNumber, accountMaintenance, saldo, portfolio);
        }
        public void OpenAccount(long accountNumber, double accountMaintenance, double saldo = 0)
        {
            Accounts.Add(new BrokerageAccount(accountNumber, accountMaintenance, saldo));
        }
        public void OpenAccount(long accountNumber, double accountMaintenance, double saldo, List<Instrument> portfolio)
        {
            Accounts.Add(new BrokerageAccount(accountNumber, accountMaintenance, saldo, portfolio));
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
    }
}
