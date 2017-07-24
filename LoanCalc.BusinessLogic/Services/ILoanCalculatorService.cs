using LoanCalc.BusinessLogic.Model;

namespace LoanCalc.BusinessLogic.Services
{
    public interface ILoanCalculatorService
    {
        PaymentSchedule GetPaymentSchedule(LoanType loanType, int duration, double principal);
    }
}