using IRL.VerticalSlices.APP.Common.ValueObjects;
using IRL.VerticalSlices.APP.Features.Shared.DomainModels.Enums;
using IRL.VerticalSlices.APP.Features.Shared.DomainModels.ValueObjects;
using System.Collections.Generic;
using Tactical.DDD;

namespace IRL.VerticalSlices.APP.Features.Shared.DomainModels.Entities
{
    public class FinanceAccount : AggregateRoot<GuidId>
    {
        public override GuidId Id { get; protected set; }
        public Balance Balance { get; private set; }
        public ICollection<FinanceTransaction> FinanceTransactions { get; private set; }

        public void Deposit(decimal amount, string description)
        {
            var transaction = new FinanceTransaction(amount, description, TransactionType.Inbound);
            FinanceTransactions.Add(transaction);
            Balance.Add(amount);
        }

        public void Withdraw(decimal amount, string description)
        {
            var transaction = new FinanceTransaction(amount, description, TransactionType.Outbound);
            FinanceTransactions.Add(transaction);
            Balance.Remove(amount);
        }
    }
}