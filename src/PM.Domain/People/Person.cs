using PM.Common.CommonModels;
using PM.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PM.Domain.People
{
    public class Person : BaseEntity, IAggregateRoot
    {
        private List<RelatedPerson> _relatedPeople;
        private GenderTypes _gender;
        private string _personalNumber;
        private DateTime _birthDate;
        private string _city;
        private int? _cityID;
        private string _firstName;
        private string _lastName;
        private string _imageUrl;
        private PhoneNumber _phoneNumber;

        private Dictionary<string, Func<Result>> _validators;


        public Person(string firstName,
                        string lastName,
                        GenderTypes gender,
                        string personalNumber,
                        DateTime birthDate
            )
        {
            InitValidators();

            FirstName = firstName;
            Gender = gender;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;
            LastName = lastName;

            _relatedPeople = new List<RelatedPerson>();
            Validate("FirstName");
        }

        public void AddRelatedPerson(Person rp, RelationTypes type)
        {
            var relPerson = new RelatedPerson
            {
                BirthDate = rp.BirthDate,
                City = rp.City,
                CityID = rp.CityID,
                FirstName = rp.FirstName,
                Gender = rp.Gender,
                ImageUrl = rp.ImageUrl,
                ID = rp.ID,
                LastName = rp.LastName,
                PersonalNumber = rp.PersonalNumber,
                PhoneNumber = rp.PhoneNumber.Number.Value,
                PhoneNumberType = rp.PhoneNumber.PhoneNumberType,
                RelationType = type
            };
            _relatedPeople.Add(relPerson);
            //TODO: VALIDATIOn
        }

        public void AddRelatedPerson(RelatedPerson relatedPerson)
        {
            _relatedPeople.Add(relatedPerson);
        }

        public void AddRelatedPeople(IEnumerable<RelatedPerson> relatedPeople)
        {
            foreach (var rp in relatedPeople)
                _relatedPeople.Add(rp);
        }

        private void Validate(params string[] fieldNames)
        {
            Result result;
            foreach (var fieldName in fieldNames)
            {
                result = _validators[fieldName]();
                if (!result.IsSuccess)
                    throw new Exception(result.Message);
            }
        }

        public IReadOnlyCollection<RelatedPerson> RelatedPeople { get => _relatedPeople; set => AddRelatedPeople(value); }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Validate("FirstName");
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                Validate();
            }
        }

        public GenderTypes Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                Validate();
            }
        }

        public string PersonalNumber
        {
            get => _personalNumber;
            set
            {
                _personalNumber = value;
                Validate();
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                Validate();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                Validate();
            }
        }

        public int? CityID
        {
            get => _cityID;
            set
            {
                _cityID = value;
                Validate();
            }
        }

        public PhoneNumber PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                Validate();
            }
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                Validate();
            }
        }


        private void InitValidators()
        {
            _validators = new Dictionary<string, Func<Result>>()
            {
                { "FirstName", () => {
                   var result = Result.GetSuccessInstance();
                    if(string.IsNullOrEmpty(_firstName))
                        result = new Result(-1, false, "სახელი სავალდებულო ველია");
                    if(_firstName.Length < 2 || _firstName.Length > 50)
                        result = new Result(-1, false, "სახელში უნდა შედგებოდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოსგან");
                    if(!Regex.IsMatch(_firstName, "(^[a-zA-Z]+$|^[ა-ჰ]+$)"))
                        result = new Result(-1, false, "უნდა შეიცავდეს მხოლოდ ქართული ან ლათინური ანბანის ასოებს, არ უნდა შეიცავდეს ერთდროულად ლათინურ და ქართულ ასოებს");
                    return result;
                } },
            };
        }

    }
}
