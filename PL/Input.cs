using System;
using System.Globalization;

namespace PL
{
    public static class Input
    {
        public static string InputName()
        {
            while (true)
            {
                Console.Write("Enter name: ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid name! Enter a valid non-zero length name.");
                }
                else
                {
                    return input;
                }
            }
        }
        
        public static string InputDescription()
        {
            while (true)
            {
                Console.Write("Enter description: ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid description! Enter a valid non-zero description.");
                }
                else
                {
                    return input;
                }
            }
        }
        
        public static int InputAge()
        {
            while (true)
            {
                Console.Write("Enter age: ");
                if (int.TryParse(Console.ReadLine(), out var age))
                {
                    if (age <= 0 || age > 120)
                    {
                        Console.WriteLine("Invalid age! Enter a whole number BETWEEN 0 and 121");
                    }
                    else
                    {
                        return age;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid age! Enter a number.");
                }
            }
        }
        
        public static int InputMoney()
        {
            while (true)
            {
                Console.Write("Enter a sum of money: ");
                if (int.TryParse(Console.ReadLine(), out var money))
                {
                    if (money <= 0 || money > 100_000_000)
                    {
                        Console.WriteLine("Invalid value! Enter a whole number BETWEEN 0 and 100 000 000");
                    }
                    else
                    {
                        return money;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid value! Enter a number.");
                }
            }
        }
        
        public static DateTime InputDate()
        {
            while (true)
            {
                Console.Write("Enter a date in format DD:MM:YYYY: ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd:MM:yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    return date;
                }

                Console.WriteLine("Invalid value! Enter a date.");
            }
        }
    }
}