using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Loans
{
    public class Loan
    {
        private double _loanAmount, _principal, _insuranceAndFees, _paidOff;
        public double InterestRate { get; set; }
        private int _loanTerm;
        private string _repaymentTerms ;

        public Loan()
        {
            _paidOff = 0;   
        }
        public Loan(double loanAmount, double interestRate, int loanTerm, double principal, double insuranceAndFees, string repaymentTerms, double paidOff)
            : this()
        {
            _loanAmount = loanAmount;
            InterestRate = interestRate;
            _loanTerm = loanTerm;
            _principal = principal;
            _insuranceAndFees = insuranceAndFees;
            _paidOff = paidOff;
            _repaymentTerms = repaymentTerms;
        }
        public double GetLoanAmount()
        {
            return _loanAmount;
        }
        public double GetPrincipal()
        {
            return _principal;
        }
        public int GetLoanTerm()
        {
            return _loanTerm;
        }
        public string GetRepaymentTerms()
        {
            return _repaymentTerms;
        }
        public double GetInsuranceAndFees()
        {
            return _insuranceAndFees;
        }
        public double GetPaidOff()
        {
            return _paidOff;
        }
        public double GetRemainingBalanceOfTheLain()
        {
            return _loanAmount - _paidOff;
        }
    }
}
