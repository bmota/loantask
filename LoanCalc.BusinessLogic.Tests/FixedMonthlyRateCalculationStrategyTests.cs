using System;
using LoanCalc.BusinessLogic.CalculationStrategies;
using LoanCalc.BusinessLogic.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LoanCalc.BusinessLogic.Tests
{
    /// <summary>
    /// Unit tests for "fixed monthly rate" calculation strategy
    /// Expected values taken from http://www.calculator.net/loan-calculator.html
    /// </summary>
    [TestClass]
    public class FixedMonthlyRateCalculationStrategyTests
    {
        [TestMethod]
        public void CanGetMonthlyPaymentTest1()
        {
            CanGetMonthlyPaymentTest(new LoanCalculationParameters
            {
                Principal = 100000,
                AnnualInterestRate = 0.03D,
                PaybackTimeInYears = 20
            }, 554.60D);
        }

        [TestMethod]
        public void CanGetMonthlyPaymentTest2()
        {
            CanGetMonthlyPaymentTest(new LoanCalculationParameters
            {
                Principal = 100000,
                AnnualInterestRate = 0.06D,
                PaybackTimeInYears = 10
            }, 1110.21D);              
        }

        [TestMethod]
        public void CanGenerateSchedule_ShouldGenerateProperNumberOfPayments()
        {
            var sut = new FixedMonthlyRateCalculationStrategy();

            var parameters = new LoanCalculationParameters
            {
                Principal = 100000,
                AnnualInterestRate = 0.06D,
                PaybackTimeInYears = 10
            };

            var ret = sut.GetPayments(parameters);

            Assert.AreEqual(120, ret.Count);
        }

        [TestMethod]
        public void CanGenerateSchedule_MonthlyPaymentsShouldBeCorrect()
        {
            var sut = new FixedMonthlyRateCalculationStrategy();

            var parameters = new LoanCalculationParameters
            {
                Principal = 100000,
                AnnualInterestRate = 0.06D,
                PaybackTimeInYears = 10
            };

            var ret = sut.GetPayments(parameters);

            ExpectMonthlyPayment(new Payment
            {
                Interest = 500,
                Principal = 610.21D, 
                Balance = 99389.79D
            }, ret[0]);

            ExpectMonthlyPayment(new Payment
            {
                Interest = 471.99D,
                Principal = 638.22D,
                Balance = 93758.81D
            }, ret[9]);

            ExpectMonthlyPayment(new Payment
            {
                Interest = 5.52D,
                Principal = 1104.68D,
                Balance = 0
            }, ret[119]);
        }

        private void ExpectMonthlyPayment(Payment expected, Payment actual)
        {
            Assert.AreEqual(expected.Principal, actual.Principal);
            Assert.AreEqual(expected.Interest, actual.Interest);
            Assert.AreEqual(expected.Balance, actual.Balance);
        }

        private void CanGetMonthlyPaymentTest(LoanCalculationParameters parameters, double expected)
        {
            var sut = new FixedMonthlyRateCalculationStrategy();

            var ret = sut.GetMonthlyPayment(parameters);

            Assert.AreEqual(expected, Math.Round(ret, MagicNumbers.DecimalPonitPrecision));
        }
    }
}
