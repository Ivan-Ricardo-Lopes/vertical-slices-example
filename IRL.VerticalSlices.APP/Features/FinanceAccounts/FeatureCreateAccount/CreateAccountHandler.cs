using AutoMapper;
using IRL.VerticalSlices.APP.Common.Base;
using IRL.VerticalSlices.APP.Common.Database.EntityFramework;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureCreateAccount
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, RequestResult<CreateAccountResult>>
    {
        private readonly CreateAccountValidator _validator;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateAccountHandler(CreateAccountValidator validator, AppDbContext appDbContext, IMapper mapper)
        {
            this._validator = validator;
            this._appDbContext = appDbContext;
            this._mapper = mapper;
        }

        public async Task<RequestResult<CreateAccountResult>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var result = new RequestResult<CreateAccountResult>();

            var validationResults = _validator.Validate(request);

            if (!validationResults.IsValid)
            {
                validationResults.Errors.Select(x => x.ErrorMessage).ToList().ForEach(error => result.AddError(error));
                return result;
            }

            int accountCode = GenerateNewAccountCode();

            var account = FinanceAccount.FinaceAccountFactory.Create(Guid.NewGuid().ToString(), accountCode, request.CustomerCode, 0, null);

            await SaveAccount(account);

            result.Payload.AccountCode = account.AccountCode;

            return result;
        }

        private int GenerateNewAccountCode()
        {
            return _appDbContext.FinanceAccounts
                .OrderByDescending(x => x.AccountCode)
                .FirstOrDefault()?.AccountCode + 1 ?? 10000;
        }

        private async Task SaveAccount(FinanceAccount account)
        {
            var dbModel = _mapper.Map<FinanceAccount, FinanceAccountDbModel>(account);
            await _appDbContext.AddAsync(dbModel);
            await _appDbContext.SaveChangesAsync();
        }
    }
}