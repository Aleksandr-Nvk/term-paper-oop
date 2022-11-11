using System;

namespace DAL
{
    public static class Validator
    {
        public static string ValidateString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid input!");
            }

            return value;
        }
        
        public static int ValidateInteger(int value, int min, int max)
        {
            if (value <= min || value > max)
            {
                throw new ArgumentException("Invalid input!");
            }

            return value;
        }
    }
}