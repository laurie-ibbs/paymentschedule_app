using AC_Cat.Models;
using AC_Cat.Services;
using AC_Cat.ViewModels;
using System;
using System.Web.Mvc;

namespace AC_Cat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuoteService _quoteService;

        public HomeController()
        {
            _quoteService = new QuoteService();
        }

        public HomeController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(QuoteScheduleViewModel model)
        {
            CheckModel(model);

            if (ModelState.IsValid)
            {
                model.Schedule = _quoteService.GenerateQuoteSchedule(
                    new QuoteServiceParameters(
                        model.SelectedLoanTerm, 
                        (double)(model.TotalPrice - model.DepositAmount), 
                        model.DeliveryDate));
            }

            return View(model);
        }

        private void CheckModel(QuoteScheduleViewModel model)
        {
            if (model.DepositAmount > model.TotalPrice)
            {
                ModelState.AddModelError(nameof(model.DepositAmount), "Deposit cannot be greater than the total.");
            }

            if (model.DepositAmount < model.TotalPrice * 0.15m)
            {
                ModelState.AddModelError(nameof(model.DepositAmount), "Deposit amount must be greater than 15% of the total.");
            }
        }
    }
}