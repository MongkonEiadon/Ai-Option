using ai.option.web.ViewModels.Account;
using AutoMapper;
using iqoption.data.User;

namespace ai.option.web.AutoMapper {
    public class UserViewModelMappedProfile : Profile {

        public UserViewModelMappedProfile() {
            CreateMap<UserDto, UserViewModel>()
                .ForMember(m => m.Id, c => c.MapFrom(x => x.Id))
                .ForMember(m => m.EmailAddress, c => c.MapFrom(x => x.UserName))
                .ForMember(m => m.RoleName, c => c.Ignore())
                .ForMember(m => m.NumberOfIqAccounts, c => c.MapFrom(x => x.IqOptionAccounts.Count));

        }
    }
}