using AutoMapper;
using IRL.VerticalSlices.APP.Common.ValueObjects;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureAccountStatement;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.Entities;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DomainModels.ValueObjects;

namespace IRL.VerticalSlices.API.Configs
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.CreateMap<FinanceAccount, FinanceAccountDbModel>()
                .ForMember(
                      dest => dest.Balance,
                      opt => opt.MapFrom(src => src.Balance.Amount)
                );

                cfg.CreateMap<FinanceAccountDbModel, FinanceAccount>()
                .ForMember(
                      dest => dest.Balance,
                      opt => opt.MapFrom(src => new Balance(src.Balance))
                )
                .ForMember(
                      dest => dest.Id,
                      opt => opt.MapFrom(src => new GuidId(src.Id))
                );

                cfg.CreateMap<FinanceTransactionDbModel, FinanceTransaction>()
                .ForMember(
                      dest => dest.Id,
                      opt => opt.MapFrom(src => new GuidId(src.Id))
                ).ReverseMap();

                cfg.CreateMap<FinanceTransactionDbModel, AccountStatementResult>();
            });

            return config;
        }
    }
}