using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class CustomersProfile : Profile {

        public CustomersProfile() {
            CreateMap<CustomerDto, CustomerState>()
                .ForMember(c => c.EmailAddress, d => d.MapFrom(dto => dto.UserName))
                .ForMember(x => x.Token, c => c.MapFrom(x => x.PasswordHash))
                .ForMember(x => x.Password, c => c.Ignore())
                .ForMember(x => x.InvitationCode, c => c.MapFrom(x => x.InviationCode));

            CreateMap<CustomerState, CustomerDto>()
                .ForMember(x => x.UserName, c => c.MapFrom(x => x.EmailAddress))
                .ForMember(x => x.Email, c => c.MapFrom(x => x.EmailAddress))
                .ForMember(x => x.InviationCode, c => c.MapFrom(x => x.InvitationCode))
                .ForMember(x => x.Id, c => c.MapFrom(x => x.Id));
        }

    }
}