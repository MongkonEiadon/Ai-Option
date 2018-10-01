using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.ReadStores.ReadModels;
using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class CustomersProfile : Profile {

        public CustomersProfile()
        {
            CreateMap<Customer, CustomerReadModel>()
                .ForMember(rm => rm.Password, o => o.MapFrom(c => c.Password))
                .ForMember(rm => rm.Level, o => o.MapFrom(c => c.Level))
                .ForMember(rm => rm.UserName, o => o.MapFrom(c => c.UserName))
                .ForMember(rm => rm.InvitationCode, o => o.Ignore());
        }

    }

}