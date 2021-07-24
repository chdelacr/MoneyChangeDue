using System.Collections.Generic;

namespace MoneyChangeDue
{
    public static class MoneyDenomination
    {
		public static List<decimal> SetMoneyDenomination(string countryCode)
		{
			List<decimal> currencies = new List<decimal>();

			// Money denominations can be added per country code
			if (countryCode.ToUpper() == "MX")
			{
				currencies.Add(100);
				currencies.Add(50);
				currencies.Add(20);
				currencies.Add(10);
				currencies.Add(5);
                currencies.Add(2);
                currencies.Add(1);
				currencies.Add(0.50m);
                currencies.Add(0.20m);
                currencies.Add(0.10m);
                currencies.Add(0.05m);
            }
			else if (countryCode.ToUpper() == "US")
			{
				currencies.Add(100);
				currencies.Add(50);
				currencies.Add(20);
				currencies.Add(10);
				currencies.Add(5);
				currencies.Add(2);
				currencies.Add(1);
				currencies.Add(0.50m);
				currencies.Add(0.25m);
				currencies.Add(0.10m);
				currencies.Add(0.05m);
				currencies.Add(0.01m);
			}

			return currencies;
		}
	}
}
