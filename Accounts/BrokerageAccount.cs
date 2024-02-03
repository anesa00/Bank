using Bank.Transactions;

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
        private void TransactionStatement(string statement)
        {
            Console.WriteLine(statement);
        }
        public BrokerageAccount()
        {
            _transactions = new List<Transaction>();
            _portfolio = new List<Instrument>();
            _totalPortfolioValue = 0;
        }
        public BrokerageAccount(long accountNumber, double accountMaintenance, double saldo = 0)
            : this()
        {
            _accountNumber = accountNumber;
            AccountMaintenance = accountMaintenance;
            _saldo = saldo;
        }
        public BrokerageAccount(long accountNumber, double accountMaintenance, double saldo, List<Instrument> portfolio)
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
        public override long GetAccountNumber()
        {
            return _accountNumber;
        }
        public override List<Transaction> GetTransactions()
        {
            return _transactions;
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
        public void BuyInstrument(double amount, long accountNumber, string type, string instrument, int quantity, double services, string description = "", 
            bool statement = true)
        {
            try
            {
                CheckingAmount(amount);
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
            var resoult = _portfolio.Find(item => item.Name == instrument);
            if (resoult.Equals(default(Instrument)))
                _portfolio.Add(new Instrument { Name = instrument, Type = type, Price = amount, Quantity = quantity });
            else 
                resoult.Quantity += quantity;

            _transactions.Add(new Transaction(DateTime.Now, description, amount, accountNumber, instrument, quantity, services));

            if (statement)
                TransactionStatement(_transactions.Last().TransactionStatement());
        }
        public void FundsDeposit(double amount, bool statement = true)
        {
            try
            {
                CheckingAmount(amount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw;
            }
            _saldo += amount;
            _transactions.Add(new Transaction(DateTime.Now, "You have deposited " + amount + "BAM into your account.", amount));
            
            if (statement)
                TransactionStatement(_transactions.Last().TransactionStatement());
        }
        public void FundsWithdrawal(double amount, bool statement = true)
        {
            try
            {
                CheckingAmount(amount);
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            if (amount > _saldo)
                throw new ArgumentOutOfRangeException("You don't have enought money on your account!");

            _saldo -= amount;
            _transactions.Add(new Transaction(DateTime.Now, "You have withdrawn " + amount + "BAM from your account.", amount));

            if (statement)
                TransactionStatement(_transactions.Last().TransactionStatement());
        }
        public void SellInstrument(Transaction transaction)
        {
            var resoult = _portfolio.Find(item => item.Name == transaction.GetInstrument());
            if (resoult.Equals(default(Instrument)))
                throw new ArgumentException("There is no instrument in the portfolio!");
            if (transaction.GetQuantity() > resoult.Quantity)
                throw new ArgumentException("You cannot sell this instrument, you do not have enough in stock!");

            _saldo += transaction.GetAmount() * transaction.GetQuantity();
            resoult.Quantity -= transaction.GetQuantity();
            if (resoult.Quantity == 0)
                _portfolio.Remove(resoult);

            _transactions.Add(new Transaction(transaction.GetDateTime(), "Deposit: " + transaction.GetDescription(), transaction.GetAmount(),
                transaction.GetAccountNumber(), transaction.GetInstrument(), transaction.GetQuantity()));
        }
        public override void ReceiveTransaction(Transaction transaction)
        {
            _transactions.Add(new Transaction(transaction.GetDateTime(), "Deposit: " + transaction.GetDescription(), transaction.GetAmount(),
                transaction.GetAccountNumber()));
            _saldo += transaction.GetAmount();
        }
    }
}
