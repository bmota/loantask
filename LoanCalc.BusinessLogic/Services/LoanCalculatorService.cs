using System;
using LoanCalc.BusinessLogic.CalculationStrategies;
using LoanCalc.BusinessLogic.Factories;
using LoanCalc.BusinessLogic.Model;

namespace LoanCalc.BusinessLogic.Services
{
    public class LoanCalculatorService : ILoanCalculatorService
    {
        public PaymentSchedule GetPaymentSchedule(LoanType loanType, int duration, double principal)
        {
            Validate(duration, principal);

            var factory = new LoanFactory();
            var loan = factory.GetLoan(loanType);

            var calculationStrategy = new FixedMonthlyRateCalculationStrategy();

            var loanCalulator = new LoanCalculator(calculationStrategy);

            return loanCalulator.GetPaymentSchedule(new LoanCalculationParameters()
            {
                AnnualInterestRate = loan.InterestRate,
                PaybackTimeInYears = duration,
                Principal = principal
            });
        }

        void Validate(int duration, double principal)
        {
            if (duration <= 0 || duration > 999)
            {
                throw new ArgumentException("Duration range is 1-999");
            }

            if (principal <= 0 || principal > 999999999999)
            {
                throw new ArgumentException("principal range is 1-999999999999");
            }
        }
    }
}
