using IRL.VerticalSlices.APP.Common.Base;
using MediatR;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureCreateAccount
{
    public class CreateAccountCommand : IRequest<RequestResult<CreateAccountResult>>
    {
        public string CustomerCode { get; set; }
        public int AccountCode { get; set; }
    }
}