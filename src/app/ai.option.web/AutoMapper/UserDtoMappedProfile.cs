using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;

namespace ai.option.web.AutoMapper {
    public class UserDtoMappedProfile : Profile {
        public UserDtoMappedProfile() {
            CreateMap<UserDto, LoginViewModel>();

            CreateMap<LoginViewModel, UserDto>()
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.EmailAddress))
                .ForMember(m => m.Email, c => c.MapFrom(s => s.EmailAddress));
        }
    }
}