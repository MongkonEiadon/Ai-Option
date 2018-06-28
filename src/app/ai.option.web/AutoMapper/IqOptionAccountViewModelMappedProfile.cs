using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;

namespace ai.option.web.AutoMapper {
    public class IqOptionAccountViewModelMappedProfile : Profile {
        public IqOptionAccountViewModelMappedProfile() {
            CreateMap<IqOptionAccountDto, IqOptionAccountViewModel>()
                .ForMember(m => m.EmailAddress, c => c.MapFrom(s => s.IqOptionUserName))
                .ForMember(m => m.UserId, c => c.MapFrom(s => s.IqOptionUserId))
                .ForMember(m => m.IqOptionAccountId, c => c.MapFrom(s => s.Id));
        }
    }
}