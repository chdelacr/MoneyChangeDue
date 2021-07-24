using System;
using System.Collections.Generic;

namespace MoneyChangeDue
{
    public static class OptimumChange
    {
		static List<decimal> moneyDenominations;
		static string countryCode;
		static void Main(string[] args)
        {
			// Define country and currency for global usage
			Console.Write("Set country code for currency (MX, US): ");
			countryCode = Console.ReadLine();

			moneyDenominations = MoneyDenomination.SetMoneyDenomination(countryCode);

			// Validate country code
			if (moneyDenominations.Count == 0)
			{
				Console.WriteLine("\nInvalid country code. Please make sure to enter one of the country codes between parenthesis.");
				Environment.Exit(0);
			}

			BeginTransaction();
		}

		static void BeginTransaction()
        {
			decimal productPrice = 0;
			decimal paidAmount = 0;
			try
            {
				// Product price and paid amount
				Console.Write("\nProduct price: ");
				productPrice = decimal.Parse(Console.ReadLine());

				Console.Write("Paid amount: ");
				paidAmount = decimal.Parse(Console.ReadLine());				
			}
			catch
            {
				Console.WriteLine("\nOnly numeric values allowed. Please try again.");
				BeginTransaction();
            }

			SortedList<decimal, decimal> changeCalc = CalculateChange(productPrice, paidAmount);

			// Display optimum change details from sorted list
			Console.WriteLine("\nChange details per denomination:");
			Console.WriteLine("Denomination - Total");
			foreach (decimal denomination in changeCalc.Keys)
			{
				Console.WriteLine("$" + denomination + " - " + changeCalc[denomination]);
			}

			NewTransaction();
		}

		/*
		 * CalculateChange method can be called by sending product price and paid amount as arguments.
		 * This method will return a sorted list with the optimum change details per denomination.
         * This overloaded method is used by the main program.
		*/
		static SortedList<decimal, decimal> CalculateChange(decimal productPrice, decimal paidAmount)
		{
			SortedList<decimal, decimal> changeCalc = new SortedList<decimal, decimal>();
			decimal changeDue = 0;

			// Validate that amounts have only two decimals
			bool productPriceIsCorrect = (productPrice * 100) == Math.Truncate(productPrice * 100);
			bool paidAmountIsCorrect = (paidAmount * 100) == Math.Truncate(paidAmount * 100);

			if (productPriceIsCorrect && paidAmountIsCorrect)
			{
				// Calculate and validate change due
				changeDue = paidAmount - productPrice;

				if (changeDue > 0)
				{
					Console.WriteLine("\nTotal change due: " + changeDue);
				}
				else if (changeDue == 0)
				{
					Console.WriteLine("\nNo change due.");
					BeginTransaction();
				}
				else
				{
					Console.WriteLine("\nPaid amount is less than the product price. Please make sure to enter the correct values.");
					BeginTransaction();
				}
			}
			else
			{
				Console.WriteLine("\nEntered amounts have more than two decimals. Please make sure to enter the correct values.");
                Environment.Exit(0);
			}
			
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
				Console.WriteLine("\nA money denomination cannot be equal to 0.");
                Environment.Exit(0);
			}

			return changeCalc;
		}

        /*
		 * CalculateChange method can be called by sending product price, paid amount and money denominations as arguments.
		 * This method will return a sorted list with the optimum change details per denomination.
         * This overloaded method can be used by declaring its static class.
		*/
        public static SortedList<decimal, decimal> CalculateChange(decimal productPrice, decimal paidAmount, List<decimal> moneyDenominations)
        {
            SortedList<decimal, decimal> changeCalc = new SortedList<decimal, decimal>();
            decimal changeDue = 0;

            try
            {
                // Validate that amounts have only two decimals
                bool productPriceIsCorrect = (productPrice * 100) == Math.Truncate(productPrice * 100);
                bool paidAmountIsCorrect = (paidAmount * 100) == Math.Truncate(paidAmount * 100);

                if (productPriceIsCorrect && paidAmountIsCorrect)
                {
                    // Calculate and validate change due
                    changeDue = paidAmount - productPrice;

                    if (changeDue > 0)
                    {
                        Console.WriteLine("\nTotal change due: " + changeDue);
                    }
                    else if (changeDue == 0)
                    {
                        Console.WriteLine("\nNo change due.");
                        return changeCalc;
                    }
                    else
                    {
                        Console.WriteLine("\nPaid amount is less than the product price. Please make sure to enter the correct values.");
                        return changeCalc;
                    }
                }
                else
                {
                    Console.WriteLine("\nEntered amounts have more than two decimals. Please make sure to enter the correct values.");
                    return changeCalc;
                }
            }
            catch
            {
                // Print exception message and return null sorted list
                Console.WriteLine("\nOnly numeric values allowed. Please try again.");
                return changeCalc;
            }

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
                Console.WriteLine("\nA money denomination cannot be equal to 0.");
                Environment.Exit(0);
            }

            return changeCalc;
        }

        static void NewTransaction() {
			try
			{
				// New transaction after calc
				Console.Write("\nNew transaction? (Y, N) ");

				char response = char.Parse(Console.ReadLine());
				if (response == 'Y')
				{
					BeginTransaction();
				}
				else if (response == 'N')
				{
					Console.WriteLine("\nExiting program...");
					Environment.Exit(0);
				}
				else
				{
					Console.WriteLine("\nInvalid selection, please try again.");
					NewTransaction();
				}
			}
			catch
			{
				Console.WriteLine("\nPlease enter a response.");
				NewTransaction();
			}
		}
	}
}
