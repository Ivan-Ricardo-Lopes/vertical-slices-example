using IRL.VerticalSlices.APP.Common.Interfaces;
using IRL.VerticalSlices.APP.Common.ValueObjects;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Enums;
using System;
using Tactical.DDD;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Entities
{
    public class FinanceTransaction : Entity<GuidId>, IObjectWithState
    {
        public override GuidId Id { get; protected set; }
        public int AccountCode { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string Description { get; set; }
        public State State { get; set; }

        public static class FinanceTransactionFactory
        {
            public static FinanceTransaction Create(string id, int accountCode, decimal amount, string description, TransactionType type, DateTime? CreatedDate = null, State? state = null)
            {
                return new FinanceTransaction()
                {
                    Id = new GuidId(id),
                    AccountCode = accountCode,
                    Amount = amount,
                    Description = description,
                    Type = type,
                    CreatedDate = CreatedDate ?? DateTime.UtcNow,
                    State = state ?? State.Added
                };
            }
        }
    }
}