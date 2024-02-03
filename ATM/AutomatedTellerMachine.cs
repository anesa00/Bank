using Bank.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Bank.ATM
{
    public class AutomatedTellerMachine
    {
        public string Adress { get; set; }
        private string _id;
        public AutomatedTellerMachine(string adress,string id) 
        {
            Adress = adress;
            _id = id;
        }
        public string GetID() 
        {
            return _id;
        }
    }
}
