using AC_Cat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AC_Cat.Tests.Services
{
    [TestClass]
    public class DateScheduleServiceTests
    {
        DateScheduleService _subject;
        [TestInitialize]
        public void Setup()
        {
            _subject = new DateScheduleService();
        }

        private DateTimeOffset _halloween2019 = new DateTimeOffset(2019, 10, 31, 0, 0, 0, new TimeSpan(0)); // 31st Nov 2019

        [TestMethod]
        public void GIVEN_halloween_2019_THEN_first_payment_due_4th_Now()
        {
            var firstDate = _subject.GetDates(_halloween2019, 3, DayOfWeek.Monday).First();

            Assert.AreEqual(DayOfWeek.Monday, firstDate.DayOfWeek);
            Assert.AreEqual(4, firstDate.Day);
            Assert.AreEqual(11, firstDate.Month);
            Assert.AreEqual(2019, firstDate.Year);
        }

        [TestMethod]
        public void GIVEN_3_week_payment_term_THEN_list_contains_three_dates()
        {
            var numberOfDates = _subject.GetDates(_halloween2019, 3, DayOfWeek.Monday).Count();

            Assert.AreEqual(3, numberOfDates);
        }

        [TestMethod]
        public void GIVEN_halloween_2019_and_3_weeks_term_THEN_last_payment_due_18th_Now()
        {
            var firstDate = _subject.GetDates(_halloween2019, 3, DayOfWeek.Monday).Last();

            Assert.AreEqual(DayOfWeek.Monday, firstDate.DayOfWeek);
            Assert.AreEqual(18, firstDate.Day);
            Assert.AreEqual(11, firstDate.Month);
            Assert.AreEqual(2019, firstDate.Year);
        }
    }
}
