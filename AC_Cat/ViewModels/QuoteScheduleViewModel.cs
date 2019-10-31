using AC_Cat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AC_Cat.ViewModels
{
    public class QuoteScheduleViewModel
    {
        public static IEnumerable<SelectListItem> AvailableLoanTerms = new[] { 1, 2, 3 }.Select(years => new SelectListItem { Text = (years * 52).ToString(), Value = years.ToString() });

        [Range(1, 100000000)]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
        [Range(1, 100000000)]
        [DataType(DataType.Currency)]
        public decimal DepositAmount { get; set; }
        public int SelectedLoanTerm { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset DeliveryDate { get; set; }

        public QuoteSchedule Schedule { get; set; } = QuoteSchedule.Default;
    }
}