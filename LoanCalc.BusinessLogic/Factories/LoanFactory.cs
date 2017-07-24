using System;
using System.Collections.Generic;
using LoanCalc.BusinessLogic.Model;

namespace LoanCalc.BusinessLogic.Factories
{
    public class LoanFactory
    {
        private readonly Dictionary<LoanType, Loan> _loans = new Dictionary<LoanType, Loan>();

        public LoanFactory()
        {
            _loans.Add(LoanType.HouseLoan, new HouseLoan());
        }

        public Loan Get(LoanType type)
        {
            if (!_loans.ContainsKey(type))
            {
                throw new ArgumentOutOfRangeException($"No loan definition for type {type}");
            }
            return _loans[type];
        }
    }
}
