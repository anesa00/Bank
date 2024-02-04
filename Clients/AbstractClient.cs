using Bank.Accounts;
using Bank.Cards;
using Bank.Utility_Classes;

namespace Bank.Clients
{ 
    public abstract class AbstractClient
    {
        public List<AbstractAccount> Accounts { get; set; }
        public Person Client { get; set; }
        public List<Card> Cards { get; set; }
        public abstract AbstractAccount GetAccount(long accountNumber);
        public abstract void CloseAccount(long accountNumber);
        public abstract void OpenCard(CardType card, long accountNumber, long cardNumber, int pin, int cvv, DateTime cardExpiry);
        public abstract Card GetCard(long cardNumber);
        public abstract void CloseCard(long cardNumber);
    }
}
