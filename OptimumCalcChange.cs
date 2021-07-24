using System;
using System.Collections.Generic;

namespace MoneyChangeDue
{
    static class OptimumCalcChange
    {
		public static List<decimal> moneyDenominations;
		static void Main(string[] args)
        {
			// Define country and currency for global usage
			Console.Write("Set country code for currency (MX, US): ");
			string countryCode = Console.ReadLine();

			moneyDenominations = MoneyDenomination.SetMoneyDenomination(countryCode);

			// Validate country code
			if (moneyDenominations == null)
			{
				Console.WriteLine("Invalid country code. Please make sure to enter one of the country codes between parenthesis.");
				Environment.Exit(0);
			}

			BeginTransaction();
		}

		public static void BeginTransaction()
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
				NewTransaction();
			}
			else
			{
				Console.WriteLine("Paid amount is less than the product price. Please make sure to enter the correct values.");
				NewTransaction();
			}

			// Use sorted list to save count per denomination
			SortedList<decimal, decimal> changeCalc = new();

			foreach (decimal denomination in moneyDenominations)
			{
				if (changeDue / denomination >= 1)
				{
					changeCalc.Add(denomination, Math.Truncate(changeDue / denomination));

					// Update change as the remainder with respect to money denomination
					changeDue %= denomination;
				}
			}

			// Display optimum change details from sorted list
			Console.WriteLine("Change details per denomination:");
			Console.WriteLine("Denomination - Total");
			foreach (decimal denomination in changeCalc.Keys)
			{	
				Console.WriteLine("$" + denomination + " - " + changeCalc[denomination]);
			}

			NewTransaction();
		}

		public static void NewTransaction() {
			Console.Write("New transaction? (Y, N)");
			if (Console.ReadLine().ToUpper() == "Y") {
				BeginTransaction();
			}
			else {
				Console.WriteLine("Exiting program...");
				Environment.Exit(0);
			}
		}
	}
}
