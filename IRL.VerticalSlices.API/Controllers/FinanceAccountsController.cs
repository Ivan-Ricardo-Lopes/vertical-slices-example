using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureCreateAccount;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureDeposit;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureWithdraw;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceAccountsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("~/finance-accounts/")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Payload);

            return BadRequest(result.Errors);
        }

        [Route("~/finance-accounts/{accountCode}/deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Payload);

            return BadRequest(result.Errors);
        }

        [Route("~/finance-accounts/{accountCode}/withdraw")]
        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] WithdrawCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Payload);

            return BadRequest(result.Errors);
        }
    }
}