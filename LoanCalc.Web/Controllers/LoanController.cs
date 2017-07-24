using System;
using System.Web.Mvc;
using LoanCalc.BusinessLogic.Model;
using LoanCalc.BusinessLogic.Services;
using LoanCalc.Web.Models;

namespace LoanCalc.Web.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanCalculatorService _loanCalculatorService;

        public LoanController(ILoanCalculatorService loanCalculatorService)
        {
            this._loanCalculatorService = loanCalculatorService;
        }
        
        public JsonResult Index(PaymentScheduleQuery query)
        {
            var schedule = _loanCalculatorService.GetPaymentSchedule(LoanType.HouseLoan, query.PaybackTimeInYears, query.Amount);

            return Json(schedule, JsonRequestBehavior.AllowGet);
        }
    }
}