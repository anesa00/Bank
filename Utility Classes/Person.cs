

namespace Bank.Utility_Classes
{
    public class Person
    {
        private string _JMBG;
        private DateTime _birthDate;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Person(string name, string surname, DateTime birthDate, int age, string JMBG, string adress, string phoneNumber, string email = "")
        {
            Name = name;
            Surname = surname;
            Age = age;
            Adress = adress;
            _JMBG = JMBG;
            _birthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public string GetJMBG()
        {
            return _JMBG;
        }
        public DateTime GetBirthDate()
        {
            return _birthDate;
        }
    }
}
