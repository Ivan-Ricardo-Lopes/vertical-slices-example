using IRL.VerticalSlices.APP.Common.Interfaces;
using IRL.VerticalSlices.APP.Common.ValueObjects;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Enums;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using Tactical.DDD;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Entities
{
    public class FinanceAccount : AggregateRoot<GuidId>, IObjectWithState
    {
        public override GuidId Id { get; protected set; }
        public int AccountCode { get; private set; }
        public string CustomerCode { get; private set; }
        public Balance Balance { get; private set; }
        public ICollection<FinanceTransaction> FinanceTransactions { get; private set; }
        public State State { get; set; }

        public void Deposit(decimal amount, string description)
        {
            var transaction = FinanceTransaction.FinanceTransactionFactory.Create(Guid.NewGuid().ToString(), AccountCode, amount, description, TransactionType.Inbound);
            FinanceTransactions.Add(transaction);
            Balance.Add(amount);
            this.State = State.Modified;
        }

        public void Withdraw(decimal amount, string description)
        {
            var transaction = FinanceTransaction.FinanceTransactionFactory.Create(Guid.NewGuid().ToString(), AccountCode, amount, description, TransactionType.Outbound);
            FinanceTransactions.Add(transaction);
            Balance.Remove(amount);
            this.State = State.Modified;
        }

        public static class FinaceAccountFactory
        {
            public static FinanceAccount Create(string id, int accountCode, string customerCode, decimal balance,
                ICollection<FinanceTransaction> financeTransactions = null)
            {
                return new FinanceAccount()
                {
                    Id = new GuidId(id),
                    AccountCode = accountCode,
                    CustomerCode = customerCode,
                    Balance = new Balance(balance),
                    FinanceTransactions = financeTransactions ?? new List<FinanceTransaction>()
                };
            }
        }
    }
}