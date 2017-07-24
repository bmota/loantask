using LoanCalc.BusinessLogic.CalculationStrategies;

namespace LoanCalc.BusinessLogic.Model
{
    public class LoanCalculator
    {
        private readonly ILoanCalculationStrategy _loanCalculationStrategy;

        public LoanCalculator(ILoanCalculationStrategy strategy)
        {
            _loanCalculationStrategy = strategy;
        }

        public PaymentSchedule CalculateMonthlySchedule(LoanCalculationParameters loanCalculationParameters)
        {
            return new PaymentSchedule
            {
                Payments = _loanCalculationStrategy.CalculatePaymentSchedule(loanCalculationParameters)
            };
        }
    }
}
