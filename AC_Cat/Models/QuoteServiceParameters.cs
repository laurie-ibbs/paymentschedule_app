using System;

namespace AC_Cat.Models
{
    public class QuoteServiceParameters
    {
        public QuoteServiceParameters(int termLengthWeeks, double moneyValue, DateTimeOffset deliveryDate)
        {
            TermLengthWeeks = termLengthWeeks;
            MoneyValue = moneyValue;
            DeliveryDate = deliveryDate;
        }

        public int TermLengthWeeks { get; }
        public double MoneyValue { get; }
        public DateTimeOffset DeliveryDate { get; }
    }
}