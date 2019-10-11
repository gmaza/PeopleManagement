using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PM.Common.CommonModels;
using PM.Domain.People;

namespace PM.Application.People
{
    public class PeopleApplication : IPeopleApplication
    {
        private readonly IPeopleDomainService _peopleDomainService;

        public PeopleApplication(IPeopleDomainService peopleDomainService)
        {
            this._peopleDomainService = peopleDomainService;
        }

        public async Task<Result<int>> CreatePerson(CreatePersonCommand cmd)
        {
            var person = new Person(cmd.FirstName,
                cmd.LastName,
                (GenderTypes)cmd.Gender,
                cmd.PersonalNumber,
                cmd.BirthDate);

            person.City = cmd.City;
            person.PhoneNumber = 
                new PhoneNumber(cmd.PhoneNumber, (PhoneNumberTypes)cmd.PhoneNumberType);

            return await _peopleDomainService.SavePerson(person);
        }
    }
}
