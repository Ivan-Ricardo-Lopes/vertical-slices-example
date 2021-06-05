using IRL.VerticalSlices.APP.Common.ValueObjects;
using IRL.VerticalSlices.APP.TransactionAccounts.DomainModels.Enums;
using System;
using Tactical.DDD;

namespace IRL.VerticalSlices.APP.Features.Shared.DomainModels.Entities
{
    public class FinanceTransaction : Entity<GuidId>
    {
        public FinanceTransaction(decimal amount, string description, TransactionType type)
        {
            Id = new GuidId();
            Amount = amount;
            Description = description;
            Type = type;
            CreatedDate = DateTime.UtcNow;
        }

        public override GuidId Id { get; protected set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string Description { get; set; }
    }
}