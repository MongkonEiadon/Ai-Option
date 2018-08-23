﻿using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.IqOption;
using AiOption.Infrastructure.DataAccess;

using AutoMapper;

namespace AiOption.Infrastructure.Mappings {

    public class IqAccountsMappingProfile : Profile {

        public IqAccountsMappingProfile() {
            CreateMap<Account, IqAccountDto>()
                .ForMember(a => a.SecuredToken, c => c.MapFrom(u => u.SecuredToken))
                .ForMember(a => a.Id, c => c.MapFrom(u => u.UserId))
                .ForMember(a => a.CustomerLevel, c => c.MapFrom(u => u.Level))
                .ForMember(a => a.SecuredUpdated, c => c.Ignore());


            CreateMap<IqAccountDto, Account>()
                .ForMember(a => a.UserId, c => c.MapFrom(u => u.Id))
                .ForMember(a => a.SecuredToken, c => c.MapFrom(u => u.SecuredToken))
                .ForMember(a => a.Level, c => c.MapFrom(u => u.CustomerLevel));
        }

    }

}