using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;

namespace ai.option.web.AutoMapper
{
    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile() {

            CreateMap<UserDto, LoginViewModel>();

            CreateMap<LoginViewModel, UserDto>()
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.EmailAddress))
                .ForMember(m => m.Email, c => c.MapFrom(s => s.EmailAddress));

        }
    }
}
