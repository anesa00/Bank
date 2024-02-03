
using Bank.Clients;
using Bank.Utility_Classes;
using System.Diagnostics.CodeAnalysis;

namespace Bank.Employees
{
    public enum Position { Director, BankOfficer, Manager, Accountant}
    public class Employee
    {
        public Person Person { get; set; }
        private DateOnly _hireDate;
        public double Salary { get; set; }
        public Position Position { get; set; }
        public string Contract { get; set; }

        public Employee(string JMBG, DateTime birthDate, string name, string surname, int age, string adress, string email, string phoneNumber,
            DateOnly hireDate, double salary, Position position, string contract)
        {
            Person = new Person(name, surname, birthDate, age, JMBG, adress, phoneNumber, email);
            _hireDate = hireDate;
            Salary = salary;
            Position = position;
            Contract = contract;
        }
        public DateOnly GetHireDate()
        {
            return _hireDate;
        }
    }
}