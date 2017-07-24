using System;
using System.Collections.Generic;
using LoanCalc.BusinessLogic.Model;

namespace LoanCalc.BusinessLogic.CalculationStrategies
{
    /// <summary>
    /// Calculates monthly payment schedule for "fiexd monthly rate" loan 
    /// See http://www.hughcalc.org/formula.php
    /// To verify, see http://www.calculator.net/loan-calculator.html
    /// </summary>
    public class FixedMonthlyRateCalculationStrategy : ILoanCalculationStrategy
    {
        public List<Payment> GetPayments(LoanCalculationParameters loanCalculationParameters)
        {
            var ret = new List<Payment>();

            var monthlyPayment = GetMonthlyPayment(loanCalculationParameters);
            double montlyInterestRate = loanCalculationParameters.AnnualInterestRate / MagicNumbers.MonthsPerYear; //J

            var currentPrincipal = loanCalculationParameters.Principal;

            while(currentPrincipal > 0)
            {
                var currentMonthlyInterest = currentPrincipal * montlyInterestRate; //H
                var principalForCurrentMonth = monthlyPayment - currentMonthlyInterest; //C
                var endingBalance = currentPrincipal - principalForCurrentMonth; //Q

                ret.Add(new Payment
                {
                    Principal = Round(principalForCurrentMonth),
                    Interest = Round(currentMonthlyInterest),
                    Balance = Round(endingBalance)
                });

                currentPrincipal = endingBalance;
            }

            return ret;
        }

        public double GetMonthlyPayment(LoanCalculationParameters loanCalculationParameters)
        {
            double montlyInterestRate = loanCalculationParameters.AnnualInterestRate / MagicNumbers.MonthsPerYear; //J

            int durationInMonths = loanCalculationParameters.PaybackTimeInYears * MagicNumbers.MonthsPerYear; //N

            double j = montlyInterestRate;
            double n = durationInMonths;
            double p = loanCalculationParameters.Principal;

            return p * (j / (1 - Math.Pow(1 + j, -n)));
        }

        private double Round(double number)
        {
            return Math.Round(number, MagicNumbers.DecimalPonitPrecision);
        }
    }
}
