using AiOption.Domain.Accounts;
using AiOption.Domain.IqAccounts;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class IqAccountsProfile : Profile {

        public IqAccountsProfile() {

            CreateMap<Account, IqAccountDto>()
                .ForMember(a => a.SecuredToken, c => c.MapFrom(u => u.SecuredToken))
                .ForMember(a => a.Id, c => c.MapFrom(u => u.UserId))
                .ForMember(a => a.CustomerLevel, c => c.MapFrom(u => u.Level))
                .ForMember(a => a.CustomerId, c => c.Ignore())
                .ForMember(a => a.TradeMode, c => c.Ignore())
                .ForMember(a => a.CreatedDate, c => c.Ignore())
                .ForMember(a => a.UpdatedDate, c=> c.Ignore())
                .ForMember(a => a.SecuredUpdated, c => c.Ignore());

            CreateMap<IqAccountDto, Account>()
                .ForMember(a => a.UserId, c => c.MapFrom(u => u.Id))
                .ForMember(a => a.SecuredToken, c => c.MapFrom(u => u.SecuredToken))
                .ForMember(a => a.Level, c => c.MapFrom(u => u.CustomerLevel));
        }

    }


}