using AutoMapper;
using Dapper;
using IRL.VerticalSlices.APP.Common.Base;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureAccountStatement
{
    public class AccountStatementHandler : IRequestHandler<AccountStatementQuery, RequestResult<List<AccountStatementResult>>>
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public AccountStatementHandler(IConfiguration config, IMapper mapper)
        {
            this._connectionString = config.GetConnectionString("AppContext");
            this._mapper = mapper;
        }

        public async Task<RequestResult<List<AccountStatementResult>>> Handle(AccountStatementQuery request, CancellationToken cancellationToken)
        {
            var result = new RequestResult<List<AccountStatementResult>>();
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                var transactions = await conexao.QueryAsync<FinanceTransactionDbModel>(
                    @"SELECT
                    [AccountCode]
                    ,[Amount]
                    ,[Type]
                    ,[CreatedDate]
                    ,[Description]
                FROM [dbo].[FinanceTransactions] WHERE ACCOUNTCODE = @AccountCode",
                    new
                    {
                        request.AccountCode
                    });

                var payload = _mapper.Map<List<FinanceTransactionDbModel>, List<AccountStatementResult>>(transactions.ToList());
                result.Payload = payload;
            }
            return result;
        }
    }
}