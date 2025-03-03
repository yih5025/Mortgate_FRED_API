using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MortgageWebProject.Models;
using MortgageWebProject.Services;

namespace MortgageWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMortgageRateService _mortgageRateService;

        public HomeController(IMortgageRateService mortgageRateService)
        {
            _mortgageRateService = mortgageRateService;
        }

        public async Task<IActionResult> Index()
        {
            double currentRate = 0;
            try
            {
                currentRate = await _mortgageRateService.GetLatestMortgageRateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FRED API ERRORS: {ex.Message}");
            }
            ViewBag.CurrentMortgageRate = currentRate;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(MortgageCalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CurrentMortgageRate = await _mortgageRateService.GetLatestMortgageRateAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"FRED API ERRORS: {ex.Message}");
                    model.CurrentMortgageRate = 0;
                }

                double monthlyRate = model.AnnualInterestRate / 100 / 12;
                int numberOfPayments = model.TermYears * 12;
                model.MonthlyPayment = model.LoanAmount * monthlyRate *
                    Math.Pow(1 + monthlyRate, numberOfPayments) /
                    (Math.Pow(1 + monthlyRate, numberOfPayments) - 1);

                return View("Result", model);
            }
            return View("Index", model);
        }
    }
}
