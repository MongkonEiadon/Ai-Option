using ai.option.web.ViewModels;
using AutoMapper;

namespace ai.option.web.AutoMapper {
    public class IqOptionProfileResponseMappedProfile : Profile {
        public IqOptionProfileResponseMappedProfile() {
            CreateMap<iqoptionapi.models.Profile, IqOptionProfileResponseViewModel>();
        }
    }
}