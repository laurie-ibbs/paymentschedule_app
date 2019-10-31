using System;

namespace AC_Cat.Models
{
    public class QuoteScheduleItem
    {
        public QuoteScheduleItem(DateTimeOffset dateTimeOffset, double amountDue)
        {
            DateTime = dateTimeOffset;
            AmountDue = amountDue;
        }

        public DateTimeOffset DateTime { get; }
        public double AmountDue { get; }
    }
}