using System.ComponentModel.DataAnnotations;

namespace MortgageWebProject.Models
{
    public class MortgageCalculatorModel {
        [Required]
        [Display(Name = "대출 금액 ($)")]
        public double LoanAmount {get; set;}

        [Required]
        [Display(Name = "연이자율 (%)")]
        public double AnnualInterestRate{get; set;}

        [Required]
        [Display(Name = "대출 기간 (년)")]
        public int TermYears { get; set; } 

        [Required]
        [Display(Name = "월 상환금 ($)")]
        public double MonthlyPayment {get; set;}

        [Display(Name = "현재 평균 모기지 금리 (%)")]
        public double CurrentMortgageRate {get; set;}
    }
}