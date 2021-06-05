using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.APP.Features.Deposit
{
    public class DepositHandler : IRequestHandler<DepositCommand, DepositResult>
    {
        public Task<DepositResult> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}