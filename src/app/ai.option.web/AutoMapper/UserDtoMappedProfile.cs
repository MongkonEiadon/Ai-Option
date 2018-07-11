using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.User;

namespace ai.option.web.AutoMapper {
    public class UserDtoMappedProfile : Profile {
        public UserDtoMappedProfile() {
            CreateMap<UserDto, LoginViewModel>()
                .ForMember(m => m.EmailAddress, c => c.MapFrom(s => s.UserName))
                .ForMember(m => m.ConfirmedPassword, c => c.Ignore())
                .ForMember(m => m.Password, c => c.Ignore())
                .ForMember(m => m.InvitationCode, c => c.MapFrom(s => s.InviationCode))
                .ForAllOtherMembers(c => c.Ignore());

            CreateMap<LoginViewModel, UserDto>()
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.EmailAddress))
                .ForMember(m => m.Email, c => c.MapFrom(s => s.EmailAddress))
                .ForMember(m => m.InviationCode, c => c.MapFrom(s => s.InvitationCode))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}