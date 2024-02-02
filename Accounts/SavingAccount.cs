using Bank.Transactions;

namespace Bank.Accounts
{
    public class SavingAccount: AbstractAccount, IAccount
    {
        public int TransactionCounter { get; set; }
        public double MinSaldo { get; set; }
        public double BankInterest { get; set; }
        public int TransactionLimit { get; set; }
        private void CheckingAmount(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("The amount can't be a negative number!");
        }
        private void CheckingTransactionLimit()
        {
            if (TransactionCounter > TransactionLimit)
                throw new ArgumentOutOfRangeException("You can no longer perform the transaction; you have reached the limit!");
        }
        private void TransactionStatement(string statement)
        {
            Console.WriteLine(statement);
        }
        public SavingAccount()
        {
            _transactions = new List<Transaction>();
            TransactionCounter = 0;
           
        }
        public SavingAccount(long accountNumber, double accountMaintenance, double saldo = 0, double minSlado = 0, double bankInterest = 0, int transactionLimit = 0)
            : this()
        {
            _accountNumber = accountNumber;
            _saldo = saldo;
            AccountMaintenance = accountMaintenance;
            MinSaldo = minSlado;
            BankInterest = bankInterest;
            TransactionLimit = transactionLimit;
        }

        public override long GetAccountNumber()
        {
            return _accountNumber;
        }
        public override double GetSaldo()
        {
            return _saldo;
        }
        public override List<Transaction> GetTransactions()
        {
            return _transactions;
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
        public void InterestDeposit(double bankInterest)
        {
            try
            {
                CheckingAmount(bankInterest);
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            _saldo += bankInterest;
        }
        public void MakeATransaction(long accountNumber, double amount, double services, string description = "", bool statement = true)
        {
            try
            {
                CheckingAmount(amount);
                CheckingTransactionLimit();
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            if (amount + services > _saldo || _saldo - amount > MinSaldo)
                throw new ArgumentOutOfRangeException("You don't have enought money on your account!");
            
            _saldo -= amount;

            _transactions.Add(new Transaction(new DateTime(), description, amount, accountNumber, services));
            TransactionCounter++;

            if (statement)
                TransactionStatement(_transactions.Last().TransactionStatement());
        }
        public void FundsWithdrawal(double amount, bool statement = true)
        {
            try
            {
                CheckingAmount(amount);
                CheckingTransactionLimit();
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            if (amount > _saldo || _saldo - amount > MinSaldo)
                throw new ArgumentOutOfRangeException("You don't have enought money on your account!");

            _saldo -= amount;

            _transactions.Add(new Transaction(new DateTime(), "You have withdrawn " + amount + "BAM from your account.", amount));
            TransactionLimit++;

            if (statement)
                TransactionStatement(_transactions.Last().TransactionStatement());
        }
        public void FundsDeposit(double amount, bool statement = true)
        {
            try
            {
                CheckingAmount(amount);
                CheckingTransactionLimit();
            }
            catch (ArgumentOutOfRangeException e)
            {

                throw;
            }

            _saldo += amount;
            _transactions.Add(new Transaction(new DateTime(), "You have deposited " + amount + "BAM into your account.", amount));
            TransactionLimit++;

            if (statement)
                TransactionStatement(_transactions.Last().TransactionStatement());
        }
        public override void ReceiveTransaction(Transaction transaction)
        {
            _transactions.Add(new Transaction(transaction.GetDateTime(), "Deposit: " + transaction.GetDescription(), transaction.GetAmount(),
                transaction.GetAccountNumber()));
            _saldo += transaction.GetAmount();
        }
    }
}
