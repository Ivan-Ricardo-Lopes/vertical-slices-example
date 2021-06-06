using IRL.VerticalSlices.APP.Common.Interfaces;
using System.Collections.Generic;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels
{
    public class FinanceAccountDbModel : IObjectWithState
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public int AccountCode { get; set; }
        public string CustomerCode { get; set; }
        public ICollection<FinanceTransactionDbModel> FinanceTransactions { get; set; } = new List<FinanceTransactionDbModel>();
        public State State { get; set; }
    }
}