using System;

namespace DAL
{
    [Serializable]
    public class RegularMoneyChange
    {
        private int _sum;
        public int Sum
        {
            get => _sum;
            set => _sum = Validator.ValidateInteger(value, -100_000_000, 100_000_000);
        }

        private DateTime _beginPeriod;
        public DateTime BeginPeriod
        {
            get => _beginPeriod;
            set => _beginPeriod = value;
        }
        
        private DateTime _endPeriod;
        public DateTime EndPeriod
        {
            get => _endPeriod;
            set => _endPeriod = value;
        }
        
        public RegularMoneyChange() {}

        public RegularMoneyChange(int sum, DateTime beginPeriod, DateTime endPeriod)
        {
            Sum = sum;
            BeginPeriod = beginPeriod;
            EndPeriod = endPeriod;
        }
    }
}