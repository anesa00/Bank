using Bank.Accounts;
using Bank.ATM;
using Bank.Employees;
using System.Globalization;
using System.Numerics;

namespace Bank
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var ATM = new List<AutomatedTellerMachine> { new AutomatedTellerMachine("Adress 18", "78953k"), new AutomatedTellerMachine("Adress 1", "7854lo") };
			var newBank = new Bank("a785", "Adress 1", "New Bank", 1478996331448221.0, "033/000-001", ATM, "Mickey", "Mouse", new DateTime(18, 11, 1928),
				2024 - 1928, "1811928175026", "New York 18", "033/000-002", "mickey.mouse@gmail.com");
			CreateEmployees(newBank);
			CreateClients(newBank);
			var freeBank= new Bank("a789", "Adress 14", "Free Bank ", 147899633144781.59, "033/000-011", "Minnie", "Mouse", new DateTime(18, 11, 1928),
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
			}
			else
				Console.WriteLine("You haven't selected any options!");
			
		}
		static void CreateEmployees(Bank bank)
		{
			bank.AddEmployee("1402000785296", new DateTime(14, 2, 2000), "John", "Johnes", 2024 - 2000, "Adress 10", "john@gmail.com", "033/000-005",
				DateTime.Now, 1052, Utility_Classes.Position.Director, "");
			bank.AddEmployee("1408002785296", new DateTime(14, 8, 2002), "Adel", "McGuire", 2024 - 2002, "Adress 8", "adel@gmail.com", "033/000-015",
				DateTime.Now, 1000, Utility_Classes.Position.Manager, "");
			bank.AddEmployee("2412000785296", new DateTime(24, 12, 2000), "Mokly", "Moklies", 2024 - 2000, "Adress 6", "mokly@gmail.com", "033/000-006",
				DateTime.Now, 1020, Utility_Classes.Position.Accountant, "");
			bank.AddEmployee("1704999785256", new DateTime(17, 4, 1999), "Jocky", "Jopless", 2024 - 1999, "Adress 7", "jockyy@gmail.com", "033/000-074",
				DateTime.Now, 999.99, Utility_Classes.Position.BankOfficer, "");
		}
		static void CreateClients(Bank bank)
		{
			bank.AddIndvidualClientWithCurrentAccount("Mockely", "Negotiv", new DateTime(14, 2, 2003), 2024 - 2003, "1402003141520", "Adress 17",
				"033/745-000", 3.14, "", 100);
			bank.AddIndvidualClientWithSavingAccount("Nicky", "Ilmon", new DateTime(30, 1, 2001), 2024 - 2001, "300100151784", "Adress 85", "033/741-002",
				3.25, "nicky.ilmone@gmai.com", 145, 20, 10.25, 15);
			bank.AddInvestorClient("Selcon", "Derky", new DateTime(20, 7, 2005), 2024 - 2005, "2007005141813", "Adress 74", "033/745-963", 15.75, "", 5023);
			bank.AddInvestorClient("Dokly", "Pecklone", new DateTime(28, 9, 1998), 2024 - 1998, "2809998763682", "Adress 14", "033/512-963", 18.96, 745896,
				new List<Instrument> { new Instrument { Name = "AERT", Price = 78523, Quantity = 1, Type = "Stocks" } }, "dokly.pecklone14@gmail.com");
			bank.AddLegalEntityClientWithBusinessAccount("785kij", "Adress 785", "Company", "Dockles", "Micklonert", new DateTime(10, 10, 1990), 2024 - 1990,
				"1010990562314", "Adress 73", 96.74, "033/000-850", "", 369240, "BAM", 0, 15, 5000);
			bank.AddLegalEntityClientWIthSavingAccount("ER874", "Adress 36", "Stick", "Solkon", "Ertany", new DateTime(28, 2, 1987), 2024 - 1987,
				"2802987362375", "Adress 63", 17, "033/639-000", "ertany@gmail.com", 7452, 1000, 31, 32);
		}
		static void PrintInformationAboutBank(Bank bank)
		{
			Console.Write(bank.Name + "\n" + bank.Adress + ", " + bank.PhoneNumber + "\n" + "Owner: " + bank.Owner.Name + " " + bank.Owner.Surname + 
				", " + bank.Owner.Age + "\n" + "ATM: ");
			foreach (var a in bank.GetAutomatedTellerMachines())
				Console.WriteLine(a.Adress);
		}
		static void EnteringData(Bank bank)
		{

			byte response = 0;
			do
			{
				Console.WriteLine("Could you enter your JMBG, please?");
				string JMBG = Console.ReadLine();
				bool willItContinue = true;
				try
				{
					var employee = bank.GetEmployees().Find(employee => employee.Person.GetJMBG() == JMBG);
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
					Console.WriteLine("12. See all transactions");
					Console.WriteLine("13. See all cards");
					Console.WriteLine("14. See all clients");
					Console.WriteLine("0. Exit");
					response = Byte.Parse(Console.ReadLine());
				}
				catch (ArgumentException e)
				{
					Console.WriteLine(e);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				finally
				{
					Console.WriteLine("Good Bye!");
				}
			} while (response != 0);

		}
	}
}
