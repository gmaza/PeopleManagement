using PM.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Domain.People
{
    public class RelatedPerson : BaseEntity
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderTypes Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int? CityID { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneNumberTypes PhoneNumberType { get; set; }
        public string ImageUrl { get; set; }
        public RelationTypes RelationType { get; set; }
    }
}
