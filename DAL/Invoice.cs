using System;

namespace DAL
{
    [Serializable]
    public class Invoice
    {
        private int _sum;
        public int Sum
        {
            get => _sum;
            set => _sum = Validator.ValidateInteger(value, -100_000_000, 100_000_000);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }
        
        public Invoice() {}

        public Invoice(int sum, DateTime date)
        {
            Sum = sum;
            Date = date;
        }
    }
}