using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;

namespace ai.option.web.AutoMapper {
    public class IqOptionAccountViewModelMappedProfile : Profile {
        public IqOptionAccountViewModelMappedProfile() {
            CreateMap<IqOptionAccountDto, IqOptionAccountViewModel>();
        }
    }
}