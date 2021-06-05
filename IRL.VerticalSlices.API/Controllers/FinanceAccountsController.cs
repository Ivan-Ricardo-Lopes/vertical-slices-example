using AutoMapper;
using IRL.VerticalSlices.API.RequestModels.FinanceAccount;
using IRL.VerticalSlices.APP.Features.Deposit;
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
        private readonly IMapper _mapper;

        public FinanceAccountsController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [Route("~/finance-accounts/deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] DepositInputModel model)
        {
            var result = await _mediator.Send(_mapper.Map<DepositInputModel, DepositCommand>(model));
            if (result.IsSuccess)
                Ok(result.Payload);

            return StatusCode(422, result.Error);
        }
    }
}