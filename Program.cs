using Bank.Accounts;
using Bank.ATM;
using Bank.Employees;
using Bank.Utility_Classes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Bank.Clients;
using Bank.Cards;
using System.Runtime.CompilerServices;
using Bank.Loans;
using Bank.Transactions;

namespace Bank
{
internal class Program
{
	static void Main(string[] args)
	{
		var ATM = new List<AutomatedTellerMachine> { new AutomatedTellerMachine("Adress 18", "78953k"), new AutomatedTellerMachine("Adress 1", "7854lo") };
		var newBank = new Bank("a785", "Adress 1", "New Bank", 1478996331448221.0, "033/000-001", ATM, "Mickey", "Mouse", new DateTime(1928, 11, 18),
			2024 - 1928, "1811928175026", "New York 18", "033/000-002", "mickey.mouse@gmail.com");
		CreateEmployees(newBank);
		CreateClients(newBank);
		var freeBank = new Bank("a789", "Adress 14", "Free Bank ", 147899633144781.59, "033/000-011", "Minnie", "Mouse", new DateTime(1928, 11, 18),
			2024 - 1928, "1811928175027", "New York 18", "033/000-003");
		CreateEmployees(freeBank);
		CreateClients(freeBank);

		Console.WriteLine("WELCOME! Choose one of option: ");
		Console.WriteLine("What bank do you want?\n1: New Bank\n2: Free Bank");
		var response = Convert.ToInt16(Console.ReadLine());
		if (response == 1)
		{
			Console.WriteLine("If you are an employee or owner and wish to become an employee at the bank, please enter: 1");
			Console.WriteLine("If you are a client or wish to become a client at the bank, please enter: 2");
			Console.WriteLine("If you want to see information about the bank, please enter: 3");
			response = Convert.ToInt16(Console.ReadLine());
			switch (response)
			{
				case 1:
					EnteringData(newBank);
					break;
				case 2:
					break;
				case 3:
					PrintInformationAboutBank(newBank);
					break;
				default:
					Console.WriteLine("You haven't selected any options!");
					break;
			}

		}
		else if (response == 2)
		{
			Console.WriteLine("If you are an employee or owner and wish to become an employee at the bank, please enter: 1");
			Console.WriteLine("If you are a client or wish to become a client at the bank, please enter: 2");
			Console.WriteLine("If you want to see information about the bank, please enter: 3");
			response = Convert.ToInt16(Console.ReadLine());
			switch (response)
			{
				case 1:
					EnteringData(freeBank);
					break;
				case 2:
					break;
				case 3:
					PrintInformationAboutBank(freeBank);
					break;
				default:
					Console.WriteLine("You haven't selected any options!");
					break;
			}
		}
		else
			Console.WriteLine("You haven't selected any options!");

	}
	static void CreateEmployees(Bank bank)
	{
		bank.AddEmployee("1402000785296", new DateTime(2000, 2, 14), "John", "Johnes", 2024 - 2000, "Adress 10", "john@gmail.com", "033/000-005",
			DateTime.Now, 1052, Position.Director, "");
		bank.AddEmployee("1408002785296", new DateTime(2002, 8, 14), "Adel", "McGuire", 2024 - 2002, "Adress 8", "adel@gmail.com", "033/000-015",
			DateTime.Now, 1000, Position.Manager, "");
		bank.AddEmployee("2412000785296", new DateTime(2000, 12, 24), "Mokly", "Moklies", 2024 - 2000, "Adress 6", "mokly@gmail.com", "033/000-006",
			DateTime.Now, 1020, Position.Accountant, "");
		bank.AddEmployee("1704999785256", new DateTime(1999, 4, 17), "Jocky", "Jopless", 2024 - 1999, "Adress 7", "jockyy@gmail.com", "033/000-074",
			DateTime.Now, 999.99, Position.BankOfficer, "");
	}
	static void CreateClients(Bank bank)
	{
		bank.AddIndvidualClientWithCurrentAccount("Mockely", "Negotiv", new DateTime(2003, 2, 14), 2024 - 2003, "1402003141520", "Adress 17",
			"033/745-000", 3.14, "", 100);
		bank.AddIndvidualClientWithSavingAccount("Nicky", "Ilmon", new DateTime(2001, 1, 30), 2024 - 2001, "300100151784", "Adress 85", "033/741-002",
			3.25, "nicky.ilmone@gmai.com", 145, 20, 10.25, 15);
		bank.AddInvestorClient("Selcon", "Derky", new DateTime(2005, 7, 20), 2024 - 2005, "2007005141813", "Adress 74", "033/745-963", 15.75, "", 5023);
		bank.AddInvestorClient("Dokly", "Pecklone", new DateTime(1998, 9, 28), 2024 - 1998, "2809998763682", "Adress 14", "033/512-963", 18.96, 745896,
			new List<Instrument> { new Instrument { Name = "AERT", Price = 78523, Quantity = 1, Type = "Stocks" } }, "dokly.pecklone14@gmail.com");
		bank.AddLegalEntityClientWithBusinessAccount("785kij", "Adress 785", "Company", "Dockles", "Micklonert", new DateTime(1990, 10, 10), 2024 - 1990,
			"1010990562314", "Adress 73", 96.74, "033/000-850", "", 369240, "BAM", 0, 15, 5000);
		bank.AddLegalEntityClientWithSavingAccount("ER874", "Adress 36", "Stick", "Solkon", "Ertany", new DateTime(1987, 2, 28), 2024 - 1987,
			"2802987362375", "Adress 63", 17, "033/639-000", "ertany@gmail.com", 7452, 1000, 31, 32);
	}
	static void PrintInformationAboutBank(Bank bank)
	{
		Console.Write(bank.Name + "\n" + bank.Adress + ", " + bank.PhoneNumber + "\n" + "Owner: " + bank.Owner.Name + " " + bank.Owner.Surname +
			"\n" + "ATM:\n");
		foreach (var a in bank.GetAutomatedTellerMachines())
			Console.WriteLine(a.Adress);
	}
	static void EnteringData(Bank bank)
	{

		byte response = 0;
		Console.WriteLine("Could you enter your JMBG, please?");
		string JMBG = Console.ReadLine();
		do
		{
			try
			{
				var employee = bank.GetEmployee(JMBG);
				if (employee == null)
				{
					Console.WriteLine("There is no employee with this JMBG");
					return;
				}
				Console.WriteLine("Choose one option: ");
				Console.WriteLine("1. Add new employee");
				Console.WriteLine("2. Remove an employee");
				Console.WriteLine("3. Get information about an employee");
				Console.WriteLine("4. Add new client");
				Console.WriteLine("5. Remove a client");
				Console.WriteLine("6. Get information about a client");
				Console.WriteLine("7. Open a card");
				Console.WriteLine("8. Close a card");
				Console.WriteLine("9. Get information about a card");
				Console.WriteLine("10. Provide a loan");
				Console.WriteLine("11. Close a loan");
					Console.WriteLine("12. Get information about a loan");
					Console.WriteLine("13. Make a transaction");
				Console.WriteLine("14. See all transactions");
				Console.WriteLine("15. See all cards");
				Console.WriteLine("16. See all clients");
				Console.WriteLine("17. See all employees");
					Console.WriteLine("18. See all loans");
				Console.WriteLine("0. Exit");
				response = Byte.Parse(Console.ReadLine());
				switch (response)
				{
					case 0:
						break;
					case 1:
						CheckIsEmployeeDirectorOrManager(employee);
						string name, surname, adress, phoneNumber, email, contract;
						DateTime birthDate;
						double salary;
						Position position;
						EnterEmployeeData(out JMBG, out name, out surname, out adress, out email, out phoneNumber, out contract, out birthDate,
							out salary, out position);
						bank.AddEmployee(JMBG, birthDate, name, surname, 2024 - birthDate.Year, adress, email, phoneNumber, DateTime.Now, salary, position,
							contract);
						break;
					case 2:
						CheckIsEmployeeDirectorOrManager(employee);
						EnterJMBG("Please enter the JMBG of the employee you want to dismiss: ", out JMBG);
						bank.RemoveEmployee(JMBG);
						break;
					case 3:
						IsNotBankOfficer(employee);
						EnterJMBG("Please enter the JMBG of the employee you want to check: ", out JMBG);
						PrintInformationAboutEmployee(bank.GetEmployee(JMBG));
						break;
					case 4:
						AddingNewClient(bank);
						break;
					case 5:
							string JMBGClient;
						EnterJMBG("Please enter the JMBG of the client you want to remove: ", out JMBGClient);
						bank.RemoveClient(JMBGClient);
						break;
					case 6:
						EnterJMBG("Please enter the JMBG of the client you want to check: ", out JMBGClient);
						PrintInformationAboutClient(bank.GetClient(JMBGClient));
						break;
						case 7:
							CardType cardType;
							long accountNumber;
							EnterCardData(out cardType, out JMBGClient, out accountNumber);
							bank.OpenCard(cardType, JMBGClient, accountNumber);
							break;
						case 8:
							long cardNumber;
                            EnterJMBG("Please enter the JMBG of the client you want to check: ", out JMBGClient);
                            Console.WriteLine("Please enter th card number: ");
                            cardNumber = Convert.ToInt64(Console.ReadLine());
                            bank.CloseCard(cardNumber, JMBGClient);
                            break;
						case 9:
                            Console.WriteLine("Please enter th card number: ");
							cardNumber = Convert.ToInt64(Console.ReadLine());
							PrintCards(new List<Card> { bank.GetACard(cardNumber)});
							break;
						case 10:
							double interestRate, principal, insuranceAndFees;
							string repaymentTerms;
							int loanTerm;
                            EnterJMBG("Please enter the JMBG of the client: ", out JMBGClient);
                            EnterLoanData(out interestRate, out loanTerm, out principal, out insuranceAndFees, out repaymentTerms);
                            bank.TakeLoan(JMBGClient, interestRate, loanTerm, principal, insuranceAndFees, repaymentTerms);
							break;
						case 11:
                            EnterJMBG("Please enter the JMBG of the client: ", out JMBGClient);
							bank.CloseLoan(JMBGClient);
                            break;
						case 12:
							int id;
                            Console.WriteLine("Please enter id of the loan: ");
							id = Convert.ToInt32(Console.ReadLine());
                            PrintLoan(bank.GetLoan(id));
							break;
						case 13:
							MakingTransaction(bank);
							break;
						case 14:
							PrintAllTransactions(bank.GetAllTranscations());
							break;
						case 15:
							PrintCards(bank.GetAllCards());
							break;
						case 16:
							PrintAllClients(Bank.GetClients());
							break;
					case 17:
						IsNotBankOfficer(employee);
						PrintAllEmployee(bank.GetAllEmployees());
						break;
						case 18:
							PrintLoans(bank.GetAllLoans());
							break;
					default:
						Console.WriteLine("You haven't chosen any option.");
						break;
				}
			}
			catch (ArgumentException e)
			{
				response = 0;
				Console.WriteLine(e);
			}
			catch (Exception e)
			{
				response = 0;
				Console.WriteLine(e);
			}
		} while (response != 0);

	}
	static void PrintInformationAboutEmployee(Employee employee)
	{
		Console.WriteLine(employee.Person.Name + " " + employee.Person.Surname + " (" + employee.Person.Age + ")\n" + employee.Person.Adress + ", " +
			employee.Person.PhoneNumber + "\nContract:\n" + employee.Contract + "\nSalary: " + employee.Salary + "\nPosition: " + employee.Position +
			"\nHire date: " + employee.GetHireDate());
	}

	static void PrintAllEmployee(List<Employee> employees)
	{
		foreach (var item in employees)
		{
			PrintInformationAboutEmployee(item);
			Console.WriteLine();
		}
	}
	static void CheckIsEmployeeDirectorOrManager(Employee employee)
	{
		if (employee.Position == Position.Accountant || employee.Position == Position.BankOfficer)
			throw new ArgumentException("You don't have authorization!");
	}
	static void IsNotBankOfficer(Employee employee)
	{
		if (employee.Position == Position.BankOfficer)
			throw new ArgumentException("You don't have authorization!");
	}
	static void PrintAccounts(List<AbstractAccount> accounts)
	{
		foreach (var item in accounts)
		{
				Console.WriteLine(item.GetAccountNumber() + "\nSaldo: " + item.GetSaldo());

				if (item is CurrentAccount ca)
					Console.WriteLine("Limit: " + ca.Limit);
				else if (item is SavingAccount sa)
					Console.WriteLine("Minimum saldo: " + sa.MinSaldo + "\nBank interest: " + sa.BankInterest + "\nTransaction limit:" + sa.TransactionLimit);
				else if (item is BusinessAccount ba)
					Console.WriteLine("Account currency : " + ba.AccountCurrency + "\nDaily transaction limit: " + ba.DailyTransactionLimit +
						"\nMonthly transaction limit: " + ba.MonthlyTransactionLimit + "\nLimit: " + ba.Limit);
				else if (item is BrokerageAccount brokerage)
				{
					Console.WriteLine("Total portfolio value: " + brokerage.GeTotalPortofolioValue() + "\nInstruments");
					brokerage.SeePortfolio();
				}
                Console.WriteLine("Transactions: ");
				item.BankStatment();
		}

	}
	static void PrintCards(List<Card> cards)
	{
		foreach (var item in cards)
		{
			Console.WriteLine(item.GetType() + ": " + item.CardNumber + "\n" + "Expiry: " + item.CardExpiry.Month + "/" + item.CardExpiry.Year +
				"\nCVV: " + item.CVV + "\nAccount: " + item.Account.GetAccountNumber());
		}
	}

		static void PrintLoan(Loan loan)
		{
			if(loan != null)
				Console.WriteLine(loan.GetID() + "\nLoan amount: " + loan.GetLoanAmount() + "\nInterest rate: " + loan.InterestRate + "\nPrincipal: " +
					loan.GetPrincipal() + "\nLoan term: " + loan.GetLoanTerm() + "\nInsurance and Fees: " + loan.GetInsuranceAndFees() + "\nRepayment terms: " +
					loan.GetRepaymentTerms() + "\nPaid off: " + loan.GetRemainingBalanceOfTheLain());
		}
		static void PrintLoans(List<Loan> loans)
		{
            foreach (var item in loans)
            {
				PrintLoan(item);
                Console.WriteLine();
            }
        }
		static void PrintInformationAboutClient(AbstractClient client)
		{
			if (client is IndvidualClient ic)
			{
				Console.WriteLine(ic.Client.Name + " " + ic.Client.Surname + " (" + ic.Client.Age + ")\n" + ic.Client.Adress + ", " + ic.Client.PhoneNumber +
					"\nAccounts: ");
				PrintAccounts(ic.Accounts);
				Console.WriteLine("\nCards: ");
				PrintCards(ic.Cards);
				Console.WriteLine("\nLoan: ");
				PrintLoan(ic.GetLoan());
			}
			else if (client is InvestorClient investor)
			{
                Console.WriteLine(investor.Client.Name + " " + investor.Client.Surname + " (" + investor.Client.Age + ")\n" + investor.Client.Adress + ", " +
					investor.Client.PhoneNumber + "\nAccounts: ");
                PrintAccounts(investor.Accounts);
                Console.WriteLine("\nCards: ");
                PrintCards(investor.Cards);
            }
			else if(client is LegalEntityClient lc)
			{
                Console.WriteLine("ID: " + lc.GetID() + "\nName of company: " + lc.Name + ",  " + lc.Adress + "\nName of owner: " + lc.Client.Name + 
					" " + lc.Client.Surname + " (" + lc.Client.Age + ")\n" + lc.Client.Adress + ", " + lc.Client.PhoneNumber + "\nAccounts: ");
                PrintAccounts(lc.Accounts);
                Console.WriteLine("\nCards: ");
                PrintCards(lc.Cards);
                Console.WriteLine("\nLoan: ");
                PrintLoan(lc.GetLoan());
            }
		}
		static void PrintAllClients(List<AbstractClient> clients)
		{
            foreach (var item in clients)
            {
				PrintInformationAboutClient(item);
                Console.WriteLine();
            }
        }
		static void PrintAllTransactions(List<Transaction> transactions)
		{
            foreach (var item in transactions)
            {
				Console.WriteLine(item.TransactionStatement());
                Console.WriteLine();
            }
        }
		static bool IsDigitsOnly(string str)
		{
			foreach (char c in str)
			{
				if (c < '0' || c > '9')
					return false;
			}

			return true;
		}
		static void EnterLoanData(out double interestRate, out int loanTerm, out double principal, out double insuranceAndFees, out string repaymentTerms)
		{
			interestRate = 0;
			loanTerm = 0;
			principal = 0;
			insuranceAndFees = 0;
			repaymentTerms = "";
			do
			{
                Console.WriteLine("Please enter a interest rate:");
				interestRate = Convert.ToInt32(Console.ReadLine());
				if (interestRate < 0)
				{
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
                Console.WriteLine("Please enter a loan term in months:");
                loanTerm = Convert.ToInt32(Console.ReadLine());
                if (loanTerm <= 0)
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
                Console.WriteLine("Please enter a principal:");
                principal = Convert.ToInt32(Console.ReadLine());
                if (principal <= 0)
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
                Console.WriteLine("Please enter an insurance and fees:");
                insuranceAndFees = Convert.ToInt32(Console.ReadLine());
                if (insuranceAndFees <= 0)
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
                Console.WriteLine("Please enter repayment terms:");
                repaymentTerms = Console.ReadLine();
                if (repaymentTerms == "")
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
				break;
            } while (true);
		}
		static void EnterCardData(out CardType cardType, out string JMBG, out long accountNumber)
		{
			do
			{
				cardType = CardType.Debit;
				JMBG = "";
				accountNumber = 0;
				Console.WriteLine("Please enter the type of card you desire (Debit, Credit, Prepaid, Charged, Business): ");
				if (!Enum.TryParse(Console.ReadLine(), true, out cardType))
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				EnterJMBG("Please enter the JMBG of the client: ", out JMBG);
				var temp = JMBG;
				foreach (var item in Bank.GetClients().Find(client => client.Client.GetJMBG() == temp).Accounts)
				{
                    Console.WriteLine("Do you want a card for this account {0}? (Y/N)", item.GetAccountNumber());
					var response = Console.ReadLine();
					if (response.ToLower() == "y")
						accountNumber = item.GetAccountNumber();
                }
				if(accountNumber == 0)
				{
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
				break;
			} while (true); 
        }
		static void EnterJMBG(string inputText, out string JMBG)
		{
			Console.WriteLine(inputText);
			JMBG = "";
			JMBG = Console.ReadLine();
		}
		static void EnterEmployeeData(out string JMBG, out string name, out string surname, out string adress, out string email, out string phoneNumber,
			out string contract, out DateTime birthDate, out double salary, out Position position)
		{
			JMBG = "";
			name = "";
			surname = "";
			adress = "";
			email = "";
			phoneNumber = "";
			contract = "";
			birthDate = DateTime.MinValue;
			salary = 0;
			position = Position.Director;
			var isCorrect = false;
			do
			{
				Console.WriteLine("Please enter the JMBG of the new employee: ");
				JMBG = Console.ReadLine();
				if (JMBG.Length != 13 || !IsDigitsOnly(JMBG))
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the name of the new employee: ");
				name = Console.ReadLine();
				Console.WriteLine("Please enter the surname of the new employee: ");
				surname = Console.ReadLine();
				Console.WriteLine("Please enter the adress of the new employee: ");
				adress = Console.ReadLine();
				Console.WriteLine("Please enter the email of the new employee: ");
				email = Console.ReadLine();
				if (email == "")
					continue;
				else if (new EmailAddressAttribute().IsValid(email) == false)
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the phone number of the new employee: ");
				phoneNumber = Console.ReadLine();
				Console.WriteLine("Please enter the contract of the new employee: ");
				contract = Console.ReadLine();
				Console.WriteLine("Please enter the birth date (year, month, day) of the new employee: ");
				if (!DateTime.TryParse(Console.ReadLine(), out birthDate))
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the salary of the new employee: ");
				salary = Convert.ToDouble(Console.ReadLine());
				if (salary <= 0)
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the position (Director, BankOfficer, Manager, Accountant): ");
				if (!Enum.TryParse(Console.ReadLine(), true, out position))
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				isCorrect = true;
			} while (!isCorrect);
		}
		static void EnterClientData(out string name, out string surname, out DateTime birthDate, out string JMBG, out string adress, out string email,
			out string phoneNumber, out double accountMaintenance)
		{
			JMBG = "";
			name = "";
			surname = "";
			adress = "";
			email = "";
			phoneNumber = "";
			birthDate = DateTime.MinValue;
			accountMaintenance = 0;
			do
			{
				Console.WriteLine("Please enter the JMBG of the new client: ");
				JMBG = Console.ReadLine();
				if (JMBG.Length != 13 || !IsDigitsOnly(JMBG))
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the name of the new client: ");
				name = Console.ReadLine();
				Console.WriteLine("Please enter the surname of the new client: ");
				surname = Console.ReadLine();
				Console.WriteLine("Please enter the adress of the new client: ");
				adress = Console.ReadLine();
				Console.WriteLine("Please enter the email of the new client: ");
				email = Console.ReadLine();
				if (email == "")
					continue;
				else if (new EmailAddressAttribute().IsValid(email) == false)
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the phone number of the new client: ");
				phoneNumber = Console.ReadLine();
				Console.WriteLine("Please enter the birth date (year, month, day) of the new client: ");
				if (!DateTime.TryParse(Console.ReadLine(), out birthDate))
				{
					Console.WriteLine("Incorrect input!");
					continue;
				}
				Console.WriteLine("Please enter the account maintenance of the new client: ");
				accountMaintenance = Convert.ToDouble(Console.ReadLine());
				break;
			} while (true);

		}
		static void AddingNewClient(Bank bank)
		{
			Console.WriteLine("Choose one of the options: ");
			Console.WriteLine("1. Indvidual client with current account");
			Console.WriteLine("2. Indvidual client with saving account");
			Console.WriteLine("3. Legal entity client with saving account");
			Console.WriteLine("4. Legal entity client with business account");
			Console.WriteLine("5. Investor client");
			var response = Convert.ToInt16(Console.ReadLine());
			switch (response)
			{
				case 1:
					string name, surname, JMBG, adress, phoneNumber, email;
					DateTime birthDate;
					double accountMaintenance, saldo, limit;
					EnterClientData(out name, out surname, out birthDate, out JMBG, out adress, out email, out phoneNumber, out accountMaintenance);
					Console.WriteLine("Please enter a saldo: ");
					saldo = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a limit: ");
					limit = Convert.ToDouble(Console.ReadLine());
					bank.AddIndvidualClientWithCurrentAccount(name, surname, birthDate, 2024 - birthDate.Year, JMBG, adress, phoneNumber, accountMaintenance,
						email, saldo, limit);
					break;
				case 2:
					double minSaldo, bankInterest;
					int transactionLimit;
					EnterClientData(out name, out surname, out birthDate, out JMBG, out adress, out email, out phoneNumber, out accountMaintenance);
					Console.WriteLine("Please enter a saldo: ");
					saldo = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a minimum saldo: ");
					minSaldo = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a bank interest: ");
					bankInterest = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a transaction limit: ");
					transactionLimit = Convert.ToInt32(Console.ReadLine());
					bank.AddIndvidualClientWithSavingAccount(name, surname, birthDate, 2024 - birthDate.Year, JMBG, adress, phoneNumber, accountMaintenance,
						email, saldo, minSaldo, bankInterest, transactionLimit);
					break;
				case 3:
					string id, nameOfCompany, adressOfCompany;
					Console.WriteLine("Please enter a id of the company: ");
					id = Console.ReadLine();
					Console.WriteLine("Please enter a name of the company: ");
					nameOfCompany = Console.ReadLine();
					Console.WriteLine("Please enter a adress of the company: ");
					adressOfCompany = Console.ReadLine();
					EnterClientData(out name, out surname, out birthDate, out JMBG, out adress, out email, out phoneNumber, out accountMaintenance);
					Console.WriteLine("Please enter a saldo: ");
					saldo = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a minimum saldo: ");
					minSaldo = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a bank interest: ");
					bankInterest = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a transaction limit: ");
					transactionLimit = Convert.ToInt32(Console.ReadLine());
					bank.AddLegalEntityClientWithSavingAccount(id, adressOfCompany, nameOfCompany, name, surname, birthDate, 2024 - birthDate.Year, JMBG,
						adress, accountMaintenance, phoneNumber, email, saldo, minSaldo, bankInterest, transactionLimit);
					break;
				case 4:
					string accountCurrency;
					int dailyTransactionLimit, monthlyTransactionLimit;
					Console.WriteLine("Please enter a id of the company: ");
					id = Console.ReadLine();
					Console.WriteLine("Please enter a name of the company: ");
					nameOfCompany = Console.ReadLine();
					Console.WriteLine("Please enter a adress of the company: ");
					adressOfCompany = Console.ReadLine();
					EnterClientData(out name, out surname, out birthDate, out JMBG, out adress, out email, out phoneNumber, out accountMaintenance);
					Console.WriteLine("Please enter a saldo: ");
					saldo = Convert.ToDouble(Console.ReadLine());
					Console.WriteLine("Please enter a account currency: ");
					accountCurrency = Console.ReadLine();
					Console.WriteLine("Please enter a daily transaction limit: ");
					dailyTransactionLimit = Convert.ToInt32(Console.ReadLine());
					Console.WriteLine("Please enter a monthly transaction limit: ");
					monthlyTransactionLimit = Convert.ToInt32(Console.ReadLine());
					Console.WriteLine("Please enter a limit: ");
					limit = Convert.ToDouble(Console.ReadLine());
					bank.AddLegalEntityClientWithBusinessAccount(id, adressOfCompany, nameOfCompany, name, surname, birthDate, 2024 - birthDate.Year, JMBG,
						adress, accountMaintenance, phoneNumber, email, saldo, accountCurrency, dailyTransactionLimit, monthlyTransactionLimit, limit);
					break;
				case 5:
					Console.WriteLine("Do you want enter the intruments? (Y/N)");
					var input = Console.ReadLine();
					EnterClientData(out name, out surname, out birthDate, out JMBG, out adress, out email, out phoneNumber, out accountMaintenance);
					Console.WriteLine("Please enter a saldo: ");
					saldo = Convert.ToDouble(Console.ReadLine());
					if (input.ToLower() == "n")
						bank.AddInvestorClient(name, surname, birthDate, 2024 - birthDate.Year, JMBG, adress, phoneNumber, accountMaintenance, email, saldo);
					else if (input.ToLower() == "y")
						bank.AddInvestorClient(name, surname, birthDate, 2024 - birthDate.Year, JMBG, adress, phoneNumber, accountMaintenance, saldo,
								EnterInstruments(), email);
					break;
				default:
					Console.WriteLine("You haven't chosen any option.");
					break;

			}
		}
		static List<Instrument> EnterInstruments()
		{
			int count = 0;
			do {
				Console.WriteLine("How many instruments do you want?");
				count = Convert.ToInt32(Console.ReadLine());
			} while (count <= 0);

			var list = new List<Instrument>(count);
			for (int i = 0; i < count; i++)
			{
				Console.WriteLine("Enter a type: ");
				var type = Console.ReadLine();
				Console.WriteLine("Enter a name: ");
				var name = Console.ReadLine();
				Console.WriteLine("Enter a price: ");
				var price = Convert.ToDouble(Console.ReadLine());
				Console.WriteLine("Enter a quantity: ");
				var quantity = Convert.ToInt32(Console.ReadLine());
				if (price <= 0 || quantity <= 0)
				{
					Console.WriteLine("Incorect input!");
					i--;
					continue;
				}
				list.Add(new Instrument { Type = type, Name = name, Price = price, Quantity = quantity });
			}

			return list;
		}
		static void MakingTransaction(Bank bank)
		{
			long fromAccountNumber = 0, toAccountNumber;
			double amount, services;
			string description;
            Console.WriteLine("Do you want to deposit from one client account to another? (Y/N)");
			var response = Console.ReadLine();
			if (response.ToLower() == "y")
			{
				Console.WriteLine("Please enter the account from which you want to deposit money: ");
				fromAccountNumber = Convert.ToInt64(Console.ReadLine());
			}
            do
			{
				Console.WriteLine("Please enter the account to which you want to deposit money: ");
				toAccountNumber = Convert.ToInt64(Console.ReadLine());
				Console.WriteLine("Please enter the amount: ");
				amount = Convert.ToDouble(Console.ReadLine());
                if(amount <= 0)
				{
                    Console.WriteLine("Incorect input!");
                    continue;
                }
                Console.WriteLine("Please enter the services:");
				services = Convert.ToDouble(Console.ReadLine());
                if (services < 0)
                {
                    Console.WriteLine("Incorect input!");
                    continue;
                }
                Console.WriteLine("Please enter the description: ");
				description = Console.ReadLine();
				break;
			} while (true);
			if (response == "y")
				bank.MakeATranscation(fromAccountNumber, toAccountNumber, amount, services, description);
			else bank.MakeATranscation(toAccountNumber, amount, services, description);
        }
    }
}
