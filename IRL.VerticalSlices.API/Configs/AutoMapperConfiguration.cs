using AutoMapper;
using IRL.VerticalSlices.API.RequestModels.FinanceAccount;
using IRL.VerticalSlices.APP.Features.Deposit;

namespace IRL.VerticalSlices.API.Configs
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.CreateMap<DepositInputModel, DepositCommand>();
            });

            return config;
        }
    }
}