using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Enums;
using System;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureAccountStatement
{
    public class AccountStatementResult
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}