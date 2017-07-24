namespace LoanCalc.BusinessLogic.Model
{
    public class HouseLoan : Loan
    {
        public override double InterestRate => MagicNumbers.HouseLoanAnnualInterestRate;
    }
}
