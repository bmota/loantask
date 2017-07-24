using System.Collections.Generic;
using LoanCalc.BusinessLogic.Model;

namespace LoanCalc.BusinessLogic.CalculationStrategies
{
    public interface ILoanCalculationStrategy
    {
        List<Payment> CalculatePaymentSchedule(LoanCalculationParameters loanCalculationParameters);
    }
}
