using AC_Cat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AC_Cat.Services
{
    public interface IQuoteService
    {
        QuoteSchedule GenerateQuoteSchedule(QuoteServiceParameters quoteServiceParameters);
    }

    public class QuoteService : IQuoteService
    {
        IConfig _config;
        IDateScheduleService _dateScheduleService;

        public QuoteService()
        {
            _config = new Config();
            _dateScheduleService = new DateScheduleService();
        }

        public QuoteService(IConfig config, IDateScheduleService dateScheduleService)
        {
            _config = config;
            _dateScheduleService = dateScheduleService;
        }

        public QuoteSchedule GenerateQuoteSchedule(QuoteServiceParameters quoteServiceParameters)
        {
            var payments = GetPaymentsList(quoteServiceParameters);
            var items = _dateScheduleService.GetDates(quoteServiceParameters.DeliveryDate
                , quoteServiceParameters.TermLengthWeeks)
                .Zip(payments, (dt, payment) => new QuoteScheduleItem(dt, payment))
                .ToList();
            return new QuoteSchedule(items);
        }

        private IEnumerable<double> GetPaymentsList(QuoteServiceParameters quoteServiceParameters)
        {
            //this is a pretty quick way to do this, but I am leaving it as such. To go beyond and recalculate
            //the fractional pennys required to reconcile the total is assumed beyone scope for an interview app
            var paymentValue = quoteServiceParameters.MoneyValue / quoteServiceParameters.TermLengthWeeks;

            return Enumerable.Range(0, quoteServiceParameters.TermLengthWeeks)
                .Select(i =>
                {
                    if (i < 1)
                        return paymentValue + _config.ArrangementFeeValue;
                    if (i == quoteServiceParameters.TermLengthWeeks - 1)
                        return paymentValue + _config.CompletionFeeValue;
                    else
                        return paymentValue;
                });
        }
    }
}