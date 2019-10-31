using System;
using System.Collections.Generic;
using System.Linq;
using AC_Cat.Models;
using AC_Cat.Services;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AC_Cat.Tests.Services
{
    [TestClass]
    public class QuoteServiceTests
    {
        IConfig _config;
        IDateScheduleService _dateScheduleService;
        QuoteService _subject;

        private static DateTimeOffset _halloween2019 = new DateTimeOffset(2019, 10, 31, 0, 0, 0, new TimeSpan(0));
        private QuoteServiceParameters testParameters = new QuoteServiceParameters(3, 300, _halloween2019);

        [TestInitialize]
        public void Setup()
        {
            _config = A.Fake<IConfig>();
            _dateScheduleService = A.Fake<IDateScheduleService>();
            A.CallTo(() => _dateScheduleService.GetDates(DateTimeOffset.UtcNow, 0, DayOfWeek.Monday))
                .WithAnyArguments()
                .Returns(new List<DateTimeOffset> { DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow });
            _subject = new QuoteService(_config, _dateScheduleService);
        }

        [TestMethod]
        public void GIVEN_arrangement_fee_THEN_first_generated_quote_value_has_arrangement_fee_added()
        {
            A.CallTo(() => _config.ArrangementFeeValue).Returns(88.0);

            var output = _subject.GenerateQuoteSchedule(testParameters).QuoteItems;

            Assert.AreEqual(188, output.First().AmountDue);
        }

        [TestMethod]
        public void GIVEN_completion_fee_THEN_last_generated_quote_value_has_completion_fee_added()
        {
            A.CallTo(() => _config.CompletionFeeValue).Returns(20.0);

            var output = _subject.GenerateQuoteSchedule(testParameters).QuoteItems;

            Assert.AreEqual(120, output.Last().AmountDue);
        }

        [TestMethod]
        public void GIVEN_config_fees_are_zero_THEN_generated_quote_values_are_all_the_same()
        {
            A.CallTo(() => _config.ArrangementFeeValue).Returns(0.0);
            A.CallTo(() => _config.CompletionFeeValue).Returns(0.0);

            var output = _subject.GenerateQuoteSchedule(testParameters).QuoteItems;

            Assert.AreEqual(3, output.Count());
            Assert.IsTrue(output.All(o => o.AmountDue == 100.0));
        }

        [TestMethod]
        public void GIVEN_fees_THEN_generated_quote_total_adds_to_expected_value()
        {
            A.CallTo(() => _config.ArrangementFeeValue).Returns(88.0);
            A.CallTo(() => _config.CompletionFeeValue).Returns(20.0);

            var output = _subject.GenerateQuoteSchedule(testParameters).QuoteItems;
            
            Assert.AreEqual(408.0, output.Aggregate(0.0, (runningTotal, item) => runningTotal + item.AmountDue));
            //300 + 20 + 88
        }
    }
}
