using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.IqOption;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class IqAccountsMappingProfile : Profile {

        public IqAccountsMappingProfile() {
            CreateMap<Account, IqAccountDto>();


            CreateMap<IqAccountDto, Account>()
                .ForMember(a => a.Level, c => c.Ignore());
        }

    }

}