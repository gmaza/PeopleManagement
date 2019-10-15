using AutoMapper;
using PM.Domain.People;
using PM.Infrastructure.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Infrastructure.Mapper
{
    public class EFProfile : Profile
    {
        public EFProfile()
        {
            CreateMap<Person, PersonEntity>().ForMember(
                dest => dest.City,
                opt => opt.Ignore());

            CreateMap<PersonEntity, Person>()
                        .ForMember(
                            dest => dest.PhoneNumber,
                            opt => opt.MapFrom(src => new PhoneNumber(src.PhoneNumber, src.PhoneNumberType)));

            CreateMap<PersonEntity, RelatedPerson>()
                        .ForMember(
                            dest => dest.PhoneNumber,
                            opt => opt.MapFrom(src => new PhoneNumber(src.PhoneNumber, src.PhoneNumberType)));
        }
    }
}
