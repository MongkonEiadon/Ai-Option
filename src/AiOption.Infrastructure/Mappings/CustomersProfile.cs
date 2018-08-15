using AiOption.Domain.Customer;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class CustomersProfile : Profile {

        public CustomersProfile() {
            CreateMap<CustomerDto, Customer>();
        }

    }

}