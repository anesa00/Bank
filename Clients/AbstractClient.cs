using Bank.Accounts;
using Bank.Cards;
using Bank.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Bank.Clients
{ 
    public abstract class AbstractClient
    {
        public List<AbstractAccount> Accounts { get; set; }
        public List<Card> Cards { get; set; }
        public abstract void OpenAccount(string accountType, long accountNumber);
        public abstract void CloseAccount(long accountNumber);
        public abstract void GetCard(CardType card, long accountNumber, long cardNumber, int pin, int cvv, DateOnly cardExpiry);
        public abstract void CloseCard(long cardNumber);
    }
}
