using MediatR;

namespace IRL.VerticalSlices.APP.Features.Deposit
{
    public class DepositCommand : IRequest<DepositResult>
    {
        public string CustomerCode { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}