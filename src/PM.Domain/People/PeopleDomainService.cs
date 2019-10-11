using PM.Common.CommonModels;
using System.Threading.Tasks;

namespace PM.Domain.People
{
    public class PeopleDomainService : IPeopleDomainService
    {
        public async Task<Result<int>> SavePerson(Person person)
        {
            return null;
        }
    }
}
