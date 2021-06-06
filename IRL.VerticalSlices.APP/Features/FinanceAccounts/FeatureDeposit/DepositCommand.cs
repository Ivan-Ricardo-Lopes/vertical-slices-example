using IRL.VerticalSlices.APP.Common.Base;
using MediatR;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureDeposit
{
    public class DepositCommand : IRequest<RequestResult<DepositResult>>
    {
        public int AccountCode { get; set; }
        public string CustomerCode { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}