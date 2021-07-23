using System;
using System.Collections.Generic;

namespace MoneyChangeDue
{
    class Program
    {
		public static List<decimal> currencies;
		static void Main(string[] args)
        {
			// Define country and currencies for global usage
			Console.Write("Set country code for currency (MX, US): ");
			string countryCode = Console.ReadLine();

			currencies = MoneyDenomination.SetMoneyDenomination(countryCode);

			// Validate country code
			if (currencies == null)
			{
				Console.WriteLine("Invalid country code. Please make sure to enter one of the country codes between parenthesis.");
				Environment.Exit(0);
			}

			NewTransaction();
		}

		public static void NewTransaction()
        {
			// Product price and paid amount
			Console.Write("Product price: ");
			decimal productPrice = decimal.Parse(Console.ReadLine());

			Console.Write("Paid amount: ");
			decimal paidAmount = decimal.Parse(Console.ReadLine());

			CalcChange(productPrice, paidAmount);
		}

		public static void CalcChange(decimal productPrice, decimal paidAmount)
		{
			// Calculate and validate change due
			decimal changeDue = paidAmount - productPrice;

			if (changeDue > 0)
			{
				Console.WriteLine("Total change due: " + changeDue);
			}
			else if (changeDue == 0)
			{
				Console.WriteLine("No change due.");
			}
			else
			{
				Console.WriteLine("Paid amount is less than the product price. Please make sure to enter the correct values.");
				Environment.Exit(0);
			}

			// Use sorted list to save count per denomination
			SortedList<decimal, decimal> changeCalc = new();

			foreach (decimal money in currencies)
			{
				if (changeDue / money >= 1)
				{
					changeCalc.Add(money, Math.Truncate(changeDue / money));
				}
			}

			// Display optimum change details from sorted list
			Console.WriteLine("Change details per denomination:");
			Console.WriteLine("Denomination - Total");
			foreach (decimal money in changeCalc.Keys)
			{	
				Console.WriteLine("$" + money + " - " + changeCalc[money]);
			}
		}
	}
}
