using IRL.VerticalSlices.APP.Common.Base;
using MediatR;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureWithdraw
{
    public class WithdrawCommand : IRequest<RequestResult<WithdrawResult>>
    {
        public int AccountCode { get; set; }
        public string CustomerCode { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}