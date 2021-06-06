using AutoMapper;
using IRL.VerticalSlices.APP.Common.Base;
using IRL.VerticalSlices.APP.Common.Database.EntityFramework;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureWithdraw
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, RequestResult<WithdrawResult>>
    {
        private readonly WithdrawHandlerValidator _validator;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public WithdrawHandler(WithdrawHandlerValidator validator, AppDbContext appDbContext, IMapper mapper)
        {
            this._validator = validator;
            this._appDbContext = appDbContext;
            this._mapper = mapper;
        }

        public async Task<RequestResult<WithdrawResult>> Handle(WithdrawCommand request, CancellationToken token)
        {
            var result = new RequestResult<WithdrawResult>();

            var validationResults = _validator.Validate(request);

            if (!validationResults.IsValid)
            {
                validationResults.Errors.Select(x => x.ErrorMessage).ToList().ForEach(error => result.AddError(error));
                return result;
            }

            FinanceAccount account = GetAccount(request);

            account.Withdraw(request.Amount, request.Description);

            await UpdateAccount(account);

            result.Payload.Balance = account.Balance.Amount;
            result.Payload.AccountCode = account.AccountCode;
            result.Payload.CustomerCode = account.CustomerCode;

            return result;
        }

        private FinanceAccount GetAccount(WithdrawCommand request)
        {
            var model = _appDbContext.FinanceAccounts
                            .Include(x => x.FinanceTransactions)
                            .AsNoTracking()
                            .FirstOrDefault(x => x.AccountCode == request.AccountCode
                            && x.CustomerCode == request.CustomerCode);

            return _mapper.Map<FinanceAccountDbModel, FinanceAccount>(model);
        }

        private async Task UpdateAccount(FinanceAccount account)
        {
            var dbModel = _mapper.Map<FinanceAccount, FinanceAccountDbModel>(account);
            _appDbContext.FinanceAccounts.Update(dbModel);
            await _appDbContext.SaveChangesAsync();
        }
    }
}