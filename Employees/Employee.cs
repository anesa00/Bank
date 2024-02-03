
using Bank.Utility_Classes;

namespace Bank.Employees
{
    public class Employee
    {
        public Person Person { get; set; }
        private DateTime _hireDate;
        public double Salary { get; set; }
        public Position Position { get; set; }
        public string Contract { get; set; }

        public Employee(string JMBG, DateTime birthDate, string name, string surname, int age, string adress, string email, string phoneNumber,
            DateTime hireDate, double salary, Position position, string contract)
        {
            Person = new Person(name, surname, birthDate, age, JMBG, adress, phoneNumber, email);
            _hireDate = hireDate;
            Salary = salary;
            Position = position;
            Contract = contract;
        }
        public DateTime GetHireDate()
        {
            return _hireDate;
        }
    }
}