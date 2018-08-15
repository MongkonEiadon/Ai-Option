using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Domain.IqOptionAccount;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {
    public class IqAccountsMappingProfile : Profile {

        public IqAccountsMappingProfile() {
            CreateMap<IqOptionAccount, IqAccountDto>();


            CreateMap<IqAccountDto, IqOptionAccount>();
        }
    }

}
