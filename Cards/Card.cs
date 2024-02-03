using Bank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Cards
{
    public enum CardType { Debit, Credit, Prepaid, Charged, Busines }
    public class Card
    {
        public AbstractAccount Account { get; set; }
        public long CardNumber { get; set; }
        private CardType _type;
        private int _pin;
        public int CVV { get; set; }
        public DateTime CardExpiry { get; set; }

        public Card(AbstractAccount account, long cardNumber, CardType cardType, int pin, int cvv, DateTime cardExpiry)
        {
            Account = account;
            CardNumber = cardNumber;
            _type = cardType;
            _pin = pin;
            CVV = cvv;
            CardExpiry = cardExpiry;
        }

        public void Pay(long accountNumber, double amount, string description = "", bool statement = true)
        {
            if (Account is IAccount account)
                account.MakeATransaction(accountNumber, amount, 0, description, statement);
        }
        public void Pay(double amount, long accountNumber, string type, string instrument, int quantity, string description = "", bool statement = true)
        {
            if (Account is BrokerageAccount account)
                account.BuyInstrument(amount, accountNumber, type, instrument, quantity, 0, description, statement);
        }
        public void Withdraw(double amount, bool statement = true)
        {
            if (Account is IAccount account)
            {
                account.FundsWithdrawal(amount, statement);
            }
        }
        public void Deposit(double amount, bool statement = true)
        {
            if (Account is IAccount account)
            {
                account.FundsDeposit(amount, statement);
            }
        }
    }
}
