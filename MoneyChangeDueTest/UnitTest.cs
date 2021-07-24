using System;
using System.Collections.Generic;
using MoneyChangeDue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoneyChangeDueTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Test_CalculateChange_Success()
        {
            // Helper variables
            decimal productPrice = 114.85m;
            decimal paidAmount = 200;

            // Set money denomination per country code
            List<decimal> moneyDenominations = MoneyDenomination.SetMoneyDenomination("US");

            // Get count per denomination
            SortedList<decimal, decimal> changeCalc = OptimumChange.CalculateChange(productPrice, paidAmount, moneyDenominations);

            // Calculate change due from change calculated
            decimal changeDue = 0;
            foreach (decimal denomination in changeCalc.Keys)
            {
                changeDue += denomination * changeCalc[denomination];
            }

            // Change for MX currency is not exact and leads to check integers only
            Assert.IsTrue(changeCalc.Count > 0 && Math.Truncate(changeDue) == Math.Truncate(paidAmount - productPrice));
        }

        [TestMethod]
        public void Test_CalculateChange_Fail()
        {
            // Helper variables
            decimal productPrice = 74.49m;
            decimal paidAmount = 50;

            // Set money denomination per country code
            List<decimal> moneyDenominations = MoneyDenomination.SetMoneyDenomination("MX");

            // Get count per denomination
            SortedList<decimal, decimal> changeCalc = OptimumChange.CalculateChange(productPrice, paidAmount, moneyDenominations);

            // Calculate change due from change calculated
            decimal changeDue = 0;
            foreach (decimal denomination in changeCalc.Keys)
            {
                changeDue += denomination * changeCalc[denomination];
            }

            // Change for MX currency is not exact and leads to check integers only
            Assert.IsFalse(changeCalc.Count > 0 && Math.Truncate(changeDue) == Math.Truncate(paidAmount - productPrice));
        }
    }
}
