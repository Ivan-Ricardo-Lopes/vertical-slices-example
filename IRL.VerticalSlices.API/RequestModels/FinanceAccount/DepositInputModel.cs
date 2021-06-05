using System;
using System.ComponentModel.DataAnnotations;

namespace IRL.VerticalSlices.API.RequestModels.FinanceAccount
{
    public class DepositInputModel
    {
        [Required]
        public string CustomerCode { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Amount must be greater than zero!")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}