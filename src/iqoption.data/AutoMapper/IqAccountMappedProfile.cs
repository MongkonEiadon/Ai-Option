using System.Linq;
using iqoption.data.IqOptionAccount;
using iqoption.domain.IqOption;
using Profile = AutoMapper.Profile;

namespace iqoption.data.AutoMapper {
    public class IqAccountMappedProfile : Profile {
        public IqAccountMappedProfile() {
            CreateMap<IqOptionAccountDto, IqAccount>()
                .ForMember(m=>m.Level, x => x.Ignore())
                .ForMember(m => m.IsSuccess, x => x.Ignore());
        }
    }

    public class IqOptionAccountDtoMappedProfile : Profile {
        public IqOptionAccountDtoMappedProfile() {
            CreateMap<IqAccount, IqOptionAccountDto>()
                .ForMember(m => m.User, x => x.Ignore())
                .ForMember(m => m.IsActive, x => x.Ignore())
                .ForMember(m => m.Id, x => x.Ignore())
                .ForMember(m => m.CreatedOn, x => x.Ignore())
                .ForMember(x => x.UpdatedOn, x => x.Ignore());
        }
    }
}