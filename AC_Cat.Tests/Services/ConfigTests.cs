using System;
using System.Configuration;
using AC_Cat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AC_Cat.Tests.Services
{
    [TestClass]
    public class ConfigTests
    {
        Config _subject;

        [TestInitialize]
        public void Setup()
        {
            _subject = new Config();
        }

        [TestMethod]
        public void WHEN_ARRANGEMENT_FEE_configured_THEN_returns_configured_ARRANGEMENT_FEE_value()
        {
            ConfigurationManager.AppSettings["ARRANGEMENT_FEE"] = "1.0";

            Assert.AreEqual(1.0, _subject.ArrangementFeeValue);
        }

        [TestMethod]
        public void WHEN_COMPLETION_FEE_configured_THEN_returns_configured_COMPLETION_FEE_value()
        {
            ConfigurationManager.AppSettings["COMPLETION_FEE"] = "1.0";

            Assert.AreEqual(1.0, _subject.CompletionFeeValue);
        }

        [TestMethod]
        public void WHEN_ARRANGEMENT_FEE_not_configured_THEN_default_ARRANGEMENT_FEE_value_returned()
        {
            ConfigurationManager.AppSettings["ARRANGEMENT_FEE"] = string.Empty;

            Assert.AreEqual(88.0, _subject.ArrangementFeeValue);
        }

        [TestMethod]
        public void WHEN_COMPLETION_FEE_not_configured_THEN_default_COMPLETION_FEE_value_returned()
        {
            ConfigurationManager.AppSettings["COMPLETION_FEE"] = string.Empty;

            Assert.AreEqual(20.0, _subject.CompletionFeeValue);
        }

    }
}
