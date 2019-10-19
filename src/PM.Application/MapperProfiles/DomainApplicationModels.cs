using AutoMapper;
using PM.Application.People;
using PM.Domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Application.MapperProfiles
{
    public class DomainApplicationModels : Profile
    {
        public DomainApplicationModels()
        {
            CreateMap<PeopleFilter, FilterPeopleQuery>();
        }
    }
}
