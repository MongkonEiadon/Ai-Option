using System;
using System.Collections.Generic;
using System.Text;
using iqoption.data.Model;
using iqoption.domain.IqOption;
using Profile = AutoMapper.Profile;

namespace iqoption.data.AutoMapper
{
    public class IqAccountMappedProfile : Profile
    {
        public IqAccountMappedProfile() {
            CreateMap<IqOptionAccountDto, IqAccount>()
                .ForMember(m => m.IsSuccess, x => x.Ignore());
        }
    }

    public class IqOptionAccountDtoMappedProfile : Profile {
        public IqOptionAccountDtoMappedProfile() {

            CreateMap<IqAccount, IqOptionAccountDto>()
                .ForMember(m => m.Id, x => x.Ignore())
                .ForMember(m => m.CreatedOn, x => x.Ignore())
                .ForMember(x => x.UpdatedOn, x => x.Ignore());

        }
    }
}
