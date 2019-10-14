using AutoMapper;
using PM.Common.CommonModels;
using PM.Domain.Interfaces.Repository;
using PM.Domain.People;
using PM.Infrastructure.EF.Context;
using PM.Infrastructure.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Infrastructure.Repository
{
    public class PeopreRepository : Repository<Person>, IPeopleRepository
    {
        public PeopreRepository(PMContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override Result Update(int id, Person newEntity)
        {
            var old = Context.Set<PersonEntity>().Find(id);
            old.BirthDate = newEntity.BirthDate;
            old.CityID = newEntity.CityID;
            old.FirstName = newEntity.FirstName;
            old.Gender = newEntity.Gender;
            old.ImageUrl = newEntity.ImageUrl;
            old.LastName = newEntity.LastName;
            old.PersonalNumber = newEntity.PersonalNumber;
            old.PhoneNumber = newEntity.PhoneNumber.Number.Value;
            old.PhoneNumberType = newEntity.PhoneNumber.PhoneNumberType;

            return Result.GetSuccessInstance();
        }
    }
}
