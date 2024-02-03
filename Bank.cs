using Bank.Accounts;
using Bank.ATM;
using Bank.Cards;
using Bank.Clients;
using Bank.Employees;
using Bank.Loans;
using Bank.Transactions;
using Bank.Utility_Classes;

namespace Bank
{
    public class Bank
    {
        private string _swiftCode;
        public string Adress { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Person Owner { get; set; }
        private static List<AbstractClient> _clients;
        private List<Employee> _employees;
        private static List<Card> _cards;
        private static List<AutomatedTellerMachine> _ATM;
        private double _saldo;
        private List<Transaction> _transcations;
        private List<Loan> _loans;
        private AbstractClient FindAccount(long accountNumber)
        {
            var client = _clients.Find(client =>
            {
                try
                {
                    var c = client.GetAccount(accountNumber);
                    return c != null && c.GetAccountNumber() == accountNumber;
                }
                catch (Exception)
                {

                    return false;
                }
            });
            
            return client;
        }
        private void CheckIsClientNull(AbstractClient client)
        {
            if (client == null)
                throw new ArgumentNullException("There is no client with this account number!");
        }
        private long GenerateAccountNumber()
        {
            Random rand = new Random();
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            for(; ; )
            {
                var number = Math.Abs(longRand % (100000000000 - 10000000000)) + 10000000000;
                var isThereAccount = FindAccount(number);
                if (isThereAccount == null)
                    return number;
            }
        }
        private int GenerateNumber(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(1000, 10000);
        }
        private long GenerateCardNumber()
        {
            Random rand = new Random();
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (10000000000000000 - 1000000000000000)) + 1000000000000000);
        }
        public Bank()
        {
            _employees = new List<Employee>();
            if(_clients == null)
                _clients = new List<AbstractClient>();
            if (_ATM == null)
                _ATM = new List<AutomatedTellerMachine>();
        }
        public Bank(string swiftCode, string adress, string name, double saldo, string phoneNumber, string nameOfOwner, string surnameOfOwner, DateTime birthDateOfOwner, 
            int ageOfOwner, string JMBGOfOwner, string adressOfOwner, string phoneNumberOfOwner, string emailOfOwner = "")
            : this()
        {
            _swiftCode = swiftCode;
            Adress = adress;
            Name = name;
            PhoneNumber = phoneNumber;
            Owner = new Person(nameOfOwner, surnameOfOwner, birthDateOfOwner, ageOfOwner, JMBGOfOwner, adressOfOwner, phoneNumberOfOwner, emailOfOwner);
        }
        public Bank(string swiftCode, string adress, string name, double saldo, string phoneNumber, List<AutomatedTellerMachine> ATM, string nameOfOwner, 
            string surnameOfOwner, DateTime birthDateOfOwner, int ageOfOwner, string JMBGOfOwner, string adressOfOwner, string phoneNumberOfOwner, 
            string emailOfOwner = "")
            : this(swiftCode, adress, name, saldo, phoneNumber, nameOfOwner, surnameOfOwner, birthDateOfOwner, ageOfOwner, JMBGOfOwner, adressOfOwner, 
                  phoneNumberOfOwner, emailOfOwner)
        {
            _ATM = ATM;
        }
        public void AddEmployee(string JMBG, DateTime birthDate, string name, string surname, int age, string adress, string email, string phonoNumber,
            DateOnly hireDate, double salary, Position position, string contract)
        {
            _employees.Add(new Employee(JMBG, birthDate, name, surname, age, adress, email, phonoNumber, hireDate, salary, position, contract));
        }
        public void RemoveEmployee(string JMBG)
        {
            var index = _employees.FindIndex(employee => employee.Person.GetJMBG() == JMBG);
            if (index == -1)
                throw new ArgumentException("There is no employee with this JMBG!");
            _employees.RemoveAt(index);
        }
        public List<Employee> GetEmployees()
        {
            return _employees;
        }
        public static  List<AbstractClient> GetClients()
        {
            return _clients;
        }
        public void AddIndvidualClientWithCurrentAccount(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, 
            string phoneNumber, double accountMaintenance, string email = "", double saldo = 0, double limit = 0)
        {
            _clients.Add(new IndvidualClient(name, surname, birthDate, age, JMBG, adress, phoneNumber, GenerateAccountNumber(), accountMaintenance, email, 
                saldo, limit));
        }
        public void AddIndvidualClientWithSavingAccount(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, 
            string phoneNumber, double accountMaintenance, string email = "", double saldo = 0, double minSaldo = 0, 
            double bankInterest = 0, int transactionLimit = 0)
        {
            _clients.Add(new IndvidualClient(name, surname, birthDate, age, JMBG, adress, phoneNumber, GenerateAccountNumber(), accountMaintenance, email, 
                saldo, minSaldo, bankInterest, transactionLimit)); ;
        }
        public void AddInvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phoneNumber, 
            double accountMaintenance, string email = "", double saldo = 0)
        {
            _clients.Add(new InvestorClient(name, surname, birthDate, age, JMBG, adress, phoneNumber, GenerateAccountNumber(), accountMaintenance, email, 
                saldo));
        }
        public void AddInvestorClient(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phoneNumber, 
            double accountMaintenance, double saldo, List<Instrument> portfolio, string email = "")
        {
            _clients.Add(new InvestorClient(name, surname, birthDate, age, JMBG, adress, phoneNumber, GenerateAccountNumber(), accountMaintenance, saldo, 
                portfolio, email));
        }
        public void AddLegalEntityClientWIthSavingAccount(string id, string adressOfCompany, string nameOfCompany, string name, string surname, 
            DateTime birthDate, int age,  string JMBG, string adress,double accountMaintenance, string phoneNumber, string email = "", 
            double saldo = 0, double minSaldo = 0, double bankInterest = 0, int transactionLimit = 0)
        {
            _clients.Add(new LegalEntityClient(id, adressOfCompany, nameOfCompany, name, surname, birthDate, age, JMBG, adress, accountMaintenance,
                 GenerateAccountNumber(), phoneNumber, email, saldo, minSaldo, bankInterest, transactionLimit));
        }
        public void AddLegalEntityClientWithBusinessAccount(string id, string adressOfCompany, string nameOfCompany, string name, string surname, 
            DateTime birthDate, int age, string JMBG, string adress, double accountMaintenance, string phoneNumber, string email = "", 
            double saldo = 0, string accountCurrency = "BAM", int dailyTransactionLimit = 0, int monthlyTransactionLimit = 0, int limit = 0)
        {
            _clients.Add(new LegalEntityClient(id, adressOfCompany, nameOfCompany, name, surname, birthDate, age, JMBG, adress, accountMaintenance,
                 GenerateAccountNumber(), phoneNumber, email, saldo, accountCurrency, dailyTransactionLimit, monthlyTransactionLimit, limit));
        }
        public void RemoveClient(string JMBG)
        {
            try 
            {
                _clients.Remove(GetClient(JMBG));
            }
            catch(ArgumentException e)
            {
                throw;
            }
        }
        public AbstractClient GetClient(string JMBG)
        {
            var client = _clients.Find(client => client.Client.GetJMBG() == JMBG);
            if (client == null)
                throw new ArgumentException("There is no client with this JMBG!");
            return client;
        }
        public void OpenCard(CardType card, string JMBG, long accountNumber)
        {
            try
            {
                var client = GetClient(JMBG);
                long number= GenerateCardNumber();
                client.OpenCard(card, accountNumber, number, GenerateNumber(1000, 10000), GenerateNumber(100, 1000), DateTime.Now.AddYears(3));
                _cards.Add(client.GetCard(number));
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        public void CloseCard(long cardNumber, string JMBG)
        {
            try
            {
                var client = GetClient(JMBG);

                client.CloseCard(cardNumber);
                _cards.Remove(GetACard(cardNumber));
            }
            catch (ArgumentException)
            {

                throw;
            }
        }
        public void TakeLoan(string JMBG, double interestRate, int loanTerm, double principal, double insuranceAndFess, string repaymentTerms)
        {
            try
            {
                var client = GetClient(JMBG);
                if (client is IClient c)
                {
                    int id = GenerateNumber(100, 10000);
                    c.TakeLoan(id, interestRate, loanTerm, principal, insuranceAndFess, repaymentTerms);
                    _saldo += insuranceAndFess + interestRate;
                    _loans.Add(new Loan(id, interestRate * principal, interestRate, loanTerm, principal, insuranceAndFess, repaymentTerms));
                }    
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Loan GetLoan(int id)
        {
            return _loans.Find(loan => loan.GetID() == id);
        }
        public void CloseLoan(string JMBG)
        {
            try
            {
                var client = GetClient(JMBG);
                if (client is IClient c)
                { 
                    c.CloseLoan();
                }
                if (client is IndvidualClient ic)
                {
                    _loans.Remove(ic.GetLoan());
                }
                else if(client is LegalEntityClient lec)
                {
                    _loans.Remove(lec.GetLoan());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetSaldo()
        {
            return _saldo ;
        }
        public void MakeATranscation(long accountNumber, double amount, double services, string description = "")
        {
            try
            {
                var client = FindAccount(accountNumber);
                CheckIsClientNull(client);
                var transcation = new Transaction(DateTime.Now, description, amount, accountNumber, services);
                _transcations.Add(transcation);
                transcation.TransactionStatement();
                _saldo += services;
                client.GetAccount(accountNumber).ReceiveTransaction(transcation);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void MakeATranscation(long FromAccountNumber, long ToAccountNumber, double amount, double services, string description = "")
        {
            try
            {
                var FromClient = FindAccount(FromAccountNumber);
                CheckIsClientNull(FromClient);
                var ToClient = FindAccount(ToAccountNumber);
                CheckIsClientNull(ToClient);
                var transcation = new Transaction(DateTime.Now, description, amount, ToAccountNumber, services);
                if(FromClient.GetAccount(FromAccountNumber) is IAccount account)
                {
                    account.MakeATransaction(ToAccountNumber, amount, services, description);
                }
                _transcations.Add(transcation);
                ToClient.GetAccount(ToAccountNumber).ReceiveTransaction(transcation);
                _saldo += services;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void BuyInstrument(long FromAccountNumber, long ToAccountNumber, string description, double amount, string type, string instrument, 
            int quantity, double services = 0)
        {
            try
            {
                var FromClient = FindAccount(FromAccountNumber);
                CheckIsClientNull(FromClient);
                var ToClient = FindAccount(ToAccountNumber);
                CheckIsClientNull(ToClient);
                var transcation = new Transaction(DateTime.Now, description, amount, ToAccountNumber, instrument, quantity, services);
                if (FromClient.GetAccount(FromAccountNumber) is BrokerageAccount account)
                {
                    account.BuyInstrument(amount, ToAccountNumber, type, instrument, quantity, services, description);
                }
                _transcations.Add(transcation);
                if (ToClient.GetAccount(ToAccountNumber) is BrokerageAccount a)
                {
                    a.SellInstrument(transcation);
                }
                _saldo += services;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Card> GetAllCards()
        {
            return _cards;
        }
        public Card GetACard(long cardNumber)
        {
            return _cards.Find(card => card.CardNumber == cardNumber);
        }
        public List<Transaction> GetAllTranscations()
        {
            return _transcations;
        }
        public List<AutomatedTellerMachine> GetAutomatedTellerMachines()
        {
            return _ATM;
        }
        public string GetSWIFTCode() 
        {
            return _swiftCode;
        }
        public List<Loan> GetAllLoans()
        {
            return _loans;
        }
    }
}
