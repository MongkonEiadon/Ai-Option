using AutoMapper;
using iqoption.data.User;

namespace iqoption.data.AutoMapper {
    public class UserMappedProfile : Profile {
        public UserMappedProfile() {
            CreateMap<UserDto, domain.Users.User>()
                .ForMember(m => m.Email, u => u.MapFrom(x => x.UserName))
                .ForMember(m => m.Level, s => s.Ignore())
                .ForMember(m => m.InvitationCode, s => s.MapFrom(x => x.InviationCode))
                .ForMember(m => m.Id, s=> s.MapFrom(x => x.Id))
                .ForMember(m => m.Password, s => s.Ignore());
        }
    }
}