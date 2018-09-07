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
        }

    }


}