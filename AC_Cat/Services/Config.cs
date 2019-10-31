using System;
using System.Configuration;

namespace AC_Cat.Services
{
    public interface IConfig
    {
        double ArrangementFeeValue { get; }
        double CompletionFeeValue { get; }
    }

    public class Config : IConfig
    {
        private const double DefaultArrangementFee = 88.0;

        private const double DefaultCompletionFee = 20.0;

        private Lazy<double> _lazyArrangementFee = new Lazy<double>(() =>
        {
            double trailValue = 0.0;
            var success = double.TryParse(ConfigurationManager.AppSettings["ARRANGEMENT_FEE"], out trailValue);
            return success ? trailValue : DefaultArrangementFee;
        });

        private Lazy<double> _lazyCompletionFee = new Lazy<double>(() =>
        {
            double trailValue = 0.0;
            var success = double.TryParse(ConfigurationManager.AppSettings["COMPLETION_FEE"], out trailValue);
            return success ? trailValue : DefaultCompletionFee;
        });

        public double ArrangementFeeValue => _lazyArrangementFee.Value;

        public double CompletionFeeValue => _lazyCompletionFee.Value;
    }
}