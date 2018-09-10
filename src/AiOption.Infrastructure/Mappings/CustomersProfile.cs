using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class CustomersProfile : Profile {

        public CustomersProfile() {
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.EmailAddress, d => d.MapFrom(dto => dto.UserName));


            CreateMap<CustomerDto, AuthorizedCustomer>()
                .ForMember(x => x.EmailAddress, c => c.MapFrom(x => x.UserName))
                .ForMember(x => x.Token, c => c.MapFrom(x => x.PasswordHash));

            CreateMap<NewCustomer, CustomerDto>()
                .ForMember(x => x.UserName, c => c.MapFrom(x => x.EmailAddress))
                .ForMember(x => x.Email, c => c.MapFrom(x => x.EmailAddress))
                .ForMember(x => x.InviationCode, c => c.MapFrom(x => x.InvitationCode))
                .ForMember(x => x.Id, c => c.MapFrom(x => x.Id));
        }

    }


}