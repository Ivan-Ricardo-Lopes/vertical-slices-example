using System.Collections.Generic;
using Tactical.DDD;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.ValueObjects
{
    public class Balance : ValueObject
    {
        public Balance(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; set; }

        public void Add(decimal amount)
        {
            this.Amount += amount;
        }

        public void Remove(decimal amount)
        {
            this.Amount -= amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }
    }
}