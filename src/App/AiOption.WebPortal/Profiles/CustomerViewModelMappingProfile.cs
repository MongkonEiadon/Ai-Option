using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.WebPortal.ViewModels;
using AutoMapper;

namespace AiOption.WebPortal.Profiles
{
    public class CustomerViewModelMappingProfile : Profile
    {
        public CustomerViewModelMappingProfile()
        {
            base.CreateMap<Customer, CustomerViewModel>()
                .ForMember(m => m.CustomerId, x => x.MapFrom(c => c.Id.Value))
                .ForMember(m => m.EmailAddress, x=> x.MapFrom(c => c.UserName.Value))
                .ForMember(m => m.Password, x => x.Ignore())
                .ForAllOtherMembers(c => c.Ignore());

            base.CreateMap<CustomerViewModel, Customer>()
                .ForMember(m => m.Id, x => x.MapFrom(c => CustomerId.With(c.CustomerId)))
                .ForMember(m => m.UserName, x=> x.MapFrom(c => Email.New(c.EmailAddress)))
                .ForMember(m => m.Password, x=> x.MapFrom(c => Password.New(c.Password)))
                .ForMember(m => m.InvitationCode, x=> x.MapFrom(c => c.InvitationCode))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
