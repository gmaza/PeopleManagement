using System;
using System.Collections.Generic;
using System.Linq;
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
            _peopleDomainService = peopleDomainService;
        }

        public async Task<Result<int>> CreatePerson(CreatePersonCommand cmd)
        {
            try
            {
                var person = new Person(cmd.FirstName,
                                    cmd.LastName,
                                    (GenderTypes)cmd.Gender,
                                    cmd.PersonalNumber,
                                    cmd.BirthDate);

                person.CityID = cmd.CityID;
                person.PhoneNumber =
                    new PhoneNumber(cmd.PhoneNumber, (PhoneNumberTypes)cmd.PhoneNumberType);

                return await _peopleDomainService.CreatePerson(person);
            }
            catch (Exception ex)
            {
                return new Result<int>(-1, false, ex.Message, 0);
            }

        }

        public async Task<FilterResponse<IEnumerable<PeopleListItem>>> Filter(FilterModel<string> f)
        {
            var searchResult = await _peopleDomainService.FastSearch(f.Filter,
                f.PageRequest.Index,
                f.PageRequest.ShowPerPage,
                f.PageRequest.SortingColumn);

            var items = searchResult.Item1.Select(i => new PeopleListItem
            {
                ID = i.ID,
                BirthDate = i.BirthDate,
                City = i.City,
                CityID = i.CityID,
                FirstName = i.FirstName,
                Gender = (int)i.Gender,
                ImageUrl = i.ImageUrl,
                LastName = i.LastName,
                PersonalNumber = i.PersonalNumber,
                PhoneNumber = i.PhoneNumber.Number.Value,
            });

            return new FilterResponse<IEnumerable<PeopleListItem>>(items, searchResult.Item2);
        }

        public async Task<Result> Update(int id, UpdatePersonCommand cmd)
        {
            try
            {
                var person = new Person(cmd.FirstName,
                                    cmd.LastName,
                                    (GenderTypes)cmd.Gender,
                                    cmd.PersonalNumber,
                                    cmd.BirthDate);

                person.ID = id;
                person.CityID = cmd.CityID;
                person.PhoneNumber =
                    new PhoneNumber(cmd.PhoneNumber, (PhoneNumberTypes)cmd.PhoneNumberType);

                return await _peopleDomainService.UpdatePerson(id, person);
            }
            catch (Exception ex)
            {
                return new Result<int>(-1, false, ex.Message, 0);
            }
        }

        public async Task<PersonDetails> Get(int id)
        {
            var person = await _peopleDomainService.GetPerson(id);

            if (person == null)
                return null;

            return new PersonDetails
            {
                ID = person.ID,
                BirthDate = person.BirthDate,
                City = person.City,
                CityID = person.CityID,
                FirstName = person.FirstName,
                Gender = (int)person.Gender,
                ImageUrl = person.ImageUrl,
                LastName = person.LastName,
                PersonalNumber = person.PersonalNumber,
                PhoneNumber = person.PhoneNumber.Number.Value,
                PhoneNumberType = (int)person.PhoneNumber.PhoneNumberType,
                RelatedPeople = person.RelatedPeople?.Select(rp => new PeopleListItem
                {
                    ID = rp.ID,
                    BirthDate = rp.BirthDate,
                    City = rp.City,
                    CityID = rp.CityID,
                    FirstName = rp.FirstName,
                    Gender = (int)rp.Gender,
                    ImageUrl = rp.ImageUrl,
                    LastName = rp.LastName,
                    PersonalNumber = rp.PersonalNumber,
                    PhoneNumber = rp.PhoneNumber,
                })

            };
        }

        public async Task<Result> Delete(int id)
        {
            return await _peopleDomainService.Delete(id);
        }

        public async Task<Result> MakeRelation(int personID, int targetID, RelationTypes type)
        {
            try
            {
                var person = await _peopleDomainService.GetPerson(personID);
                var relativePerson = await _peopleDomainService.GetPerson(targetID);
                person.AddRelatedPerson(relativePerson, type);
                return await _peopleDomainService.UpdatePerson(personID, person);
            }
            catch (Exception ex)
            {
                return new Result(-1, false, ex.Message);
            }
        }
    }
}
