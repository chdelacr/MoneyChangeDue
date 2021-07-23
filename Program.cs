﻿using System;
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
		}
    }
}
