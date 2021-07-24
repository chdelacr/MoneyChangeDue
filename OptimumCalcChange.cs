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
			decimal productPrice = 0;
			decimal paidAmount = 0;
			try
            {
				// Product price and paid amount
				Console.Write("Product price: ");
				productPrice = decimal.Parse(Console.ReadLine());

				Console.Write("Paid amount: ");
				paidAmount = decimal.Parse(Console.ReadLine());				
			}
			catch
            {
				Console.WriteLine("Only numeric values allowed.");
            }

			SortedList<decimal, decimal> changeCalc = CalcChange(productPrice, paidAmount);

			// Display optimum change details from sorted list
			Console.WriteLine("Change details per denomination:");
			Console.WriteLine("Denomination - Total");
			foreach (decimal denomination in changeCalc.Keys)
			{
				Console.WriteLine("$" + denomination + " - " + changeCalc[denomination]);
			}

			NewTransaction();
		}

		public static SortedList<decimal, decimal> CalcChange(decimal productPrice, decimal paidAmount)
		{
			decimal changeDue = 0;
			try
            {
				// Calculate and validate change due
				changeDue = paidAmount - productPrice;

				if (changeDue > 0)
				{
					Console.WriteLine("Total change due: " + changeDue);
				}
				else if (changeDue == 0)
				{
					Console.WriteLine("No change due.");
					BeginTransaction();
				}
				else
				{
					Console.WriteLine("Paid amount is less than the product price. Please make sure to enter the correct values.");
					BeginTransaction();
				}
			}
			catch
            {
				Console.WriteLine("Only numeric values allowed.");
			}

			SortedList<decimal, decimal> changeCalc = new();
			try
            {
				// Save count per denomination
				foreach (decimal denomination in moneyDenominations)
				{
					if (changeDue / denomination >= 1)
					{
						changeCalc.Add(denomination, Math.Truncate(changeDue / denomination));

						// Update change as the remainder with respect to money denomination
						changeDue %= denomination;
					}
				}
			}
			catch (DivideByZeroException)
            {
				Console.WriteLine("A money denomination cannot be equal to 0.");
            }

			return changeCalc;
		}

		public static void NewTransaction() {
			// New transaction after calc
			Console.Write("New transaction? (Y, N)");

			char response = char.Parse(Console.ReadLine());
			if (response == 'Y') {
				BeginTransaction();
			}
			else if (response == 'N') {
				Console.WriteLine("Exiting program...");
				Environment.Exit(0);
			}
			else
            {
				Console.WriteLine("Invalid selection, please try again.");
				NewTransaction();
            }
		}
	}
}
