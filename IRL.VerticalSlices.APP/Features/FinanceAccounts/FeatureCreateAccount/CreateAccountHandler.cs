using IRL.VerticalSlices.APP.Common.Base;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureCreateAccount
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, RequestResult<CreateAccountResult>>
    {
        private readonly CreateAccountValidator _validator;
        private readonly string _connectionString;

        public CreateAccountHandler(CreateAccountValidator validator,
            IConfiguration config)
        {
            this._validator = validator;
            this._connectionString = config.GetConnectionString("AppContext");
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

            var account = FinanceAccount.FinaceAccountFactory.Create(Guid.NewGuid().ToString(), request.AccountCode, request.CustomerCode, 0, null);

            string commandSql =
                @"INSERT INTO [dbo].[FinanceAccounts]
                ([Id]
                ,[Balance]
                ,[AccountCode]
                ,[CustomerCode])
            VALUES
                (@Id
                ,0
                ,@AccountCode
                ,@CustomerCode)";

            using (SqlConnection connection =
                new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandSql, connection);
                command.Parameters.AddWithValue("@Id", account.Id.ToString());
                command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
                command.Parameters.AddWithValue("@CustomerCode", account.CustomerCode);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            result.Payload.AccountCode = account.AccountCode;

            return result;
        }
    }
}