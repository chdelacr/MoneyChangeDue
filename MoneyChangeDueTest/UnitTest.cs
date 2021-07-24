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
            List<decimal> moneyDenominations = MoneyDenomination.SetMoneyDenomination("MX");
            SortedList<decimal, decimal> changeCalc = OptimumChange.CalculateChange(11, 25.75m, moneyDenominations);
            Assert.IsTrue(changeCalc.Count > 0);
        }
    }
}
