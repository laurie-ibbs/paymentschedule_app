using System;
using System.Collections.Generic;

namespace AC_Cat.Models
{
    public class QuoteSchedule
    {
        public static QuoteSchedule Default = new QuoteSchedule(new List<QuoteScheduleItem>());

        public QuoteSchedule(IEnumerable<QuoteScheduleItem> quoteItems)
        {
            QuoteItems = quoteItems;
        }

        public IEnumerable<QuoteScheduleItem> QuoteItems { get; }
    }
}