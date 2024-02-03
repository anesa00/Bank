
using Bank.Accounts;
using Bank.Cards;
using Bank.Utility_Classes;

namespace Bank.Clients
{
    public class InvestorClient: AbstractClient
    {
        private void CheckIndex(int index)
        {
            if (index == -1)
                throw new ArgumentException("There is no account with this account number!");
        }
        public InvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phoneNumber, long accountNumber,
            double accountMaintenance, string email = "", double saldo = 0)
        {
            Client = new Person(name, surname, birthDate, age, JMBG, adress, phoneNumber, email);
            OpenAccount(accountNumber, accountMaintenance, saldo);
        }
        public InvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phoneNumber, long accountNumber,
            double accountMaintenance, double saldo, List<Instrument> portfolio, string email = "")
        {
            Client = new Person(name, surname, birthDate, age, JMBG, adress, phoneNumber, email);
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
        public override void OpenCard(CardType card, long accountNumber, long cardNumber, int pin, int cvv, DateTime cardExpiry)
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
        public override AbstractAccount GetAccount(long accountNumber)
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
            return Accounts[index];
        }
        public override Card GetCard(long cardNumber)
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
            return Cards[index];
        }
    }
}
