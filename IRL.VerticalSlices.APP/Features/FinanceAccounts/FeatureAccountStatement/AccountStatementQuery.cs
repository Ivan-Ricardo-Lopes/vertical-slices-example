using IRL.VerticalSlices.APP.Common.Base;
using MediatR;
using System.Collections.Generic;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureAccountStatement
{
    public class AccountStatementQuery : IRequest<RequestResult<List<AccountStatementResult>>>
    {
        public string AccountCode { get; set; }
    }
}