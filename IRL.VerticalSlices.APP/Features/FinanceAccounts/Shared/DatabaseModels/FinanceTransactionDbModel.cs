using IRL.VerticalSlices.APP.Common.Interfaces;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Enums;
using System;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels
{
    public class FinanceTransactionDbModel : IObjectWithState
    {
        public string Id { get; set; }
        public int AccountCode { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
    }
}